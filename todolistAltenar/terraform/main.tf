provider "helm" {
  kubernetes = {
    config_path = "~/.kube/config"
  }
}

provider "kubectl" {
  config_path = "/home/luver/.kube/config"
}

module "Vms-create-via-Vagrant" {
  source = "./modules/vagrant_create_vms"
  vagrantfile_dir_path = "./modules/vagrant_create_vms"
  vagrantfile_os_image = "rockylinux/9"
  vagrantfile_os_image_version = "6.0.0"
  vagrantfile_count_masters = var.count_masters
  vagrantfile_count_workers = var.count_workers
  vagrantfile_count_dbs = var.count_dbs
  vagrantfile_network_interface = var.network_interface
  vagrantfile_cpus_in_stock = var.cpus_count
  vagrantfile_ram_in_stock = var.ram_volume
  vagrantfile_diskstorage_in_stock = var.diskstorage_size
}

resource "local_sensitive_file" "inventory-create" {
  content = yamlencode({
  "all" = {
    "hosts" = merge(
      {
        for master_index in range(1, var.count_masters + 1) : "master-node-${master_index}" => {
          "ansible_host": "192.168.0.17${master_index}"
          "ip": "192.168.0.17${master_index}"
          "access_ip": "192.168.0.17${master_index}"
        }
      },
      {
        for worker_index in range(1, var.count_workers + 1) : "worker-node-${worker_index}" => {
          "ansible_host": "192.168.0.18${worker_index}"
          "ip": "192.168.0.18${worker_index}"
          "access_ip": "192.168.0.18${worker_index}"
        }
      },
      {
        for db_index in range(1, var.count_dbs + 1) : "database-node-${db_index}" => {
          "ansible_host": "192.168.0.19${db_index}"
          "ip": "192.168.0.19${db_index}"
          "access_ip": "192.168.0.19${db_index}"
        }
      }
    )

    "children" = {
      "kube_control_plane" = {
        "hosts" = {
          for master_index in range(1, var.count_masters + 1) : "master-node-${master_index}" => {}
        }
      },
      "kube_node" = {
        "hosts" = {
          for worker_index in range(1, var.count_workers + 1) : "worker-node-${worker_index}" => {}
        }
      },
      "etcd" = {
        "hosts" = {
          for master_index in range(1, var.count_masters + 1) : "master-node-${master_index}" => {}
        }
      },
      "k8s_cluster" = {
        hosts = merge(
          {
            for master_index in range(1, var.count_masters + 1) : "master-node-${master_index}" => {}
          },
          {
            for worker_index in range(1, var.count_workers + 1) : "worker-node-${worker_index}" => {}
          }
        ),
      },
      "calico_rr:" = {
        "hosts:" = {}
      },
      "postgres-hosts" = {
        hosts = {
          for db_index in range(1, var.count_dbs + 1) : "database-node-${db_index}" => {}
        }
      }
    }
    }
  })

  filename = "../ansible/inventories/kubespray/inventory.yml"
}

resource "terraform_data" "start-server-playbook-exec" {
  provisioner "local-exec" {
    command = "ansible-playbook ../ansible/playbooks/startServer.yml --vault-password-file ./pass -i ../ansible/inventories/kubespray/inventory.yml "
  }

  depends_on = [ 
    module.Vms-create-via-Vagrant,
    local_sensitive_file.inventory-create
    # local_sensitive_file.infra-inventory-create
  ]
}

resource "terraform_data" "db_creation" {
  provisioner "local-exec" {
    command = "ansible-playbook ../ansible/playbooks/db-deploy.yml --vault-password-file ./pass -i ../ansible/inventories/kubespray/inventory.yml"
  }  

  depends_on = [ 
    terraform_data.start-server-playbook-exec,
    local_sensitive_file.inventory-create
    # local_sensitive_file.infra-inventory-create
  ]
}

resource "terraform_data" "kubespray-creation-cluster" {
  provisioner "local-exec" {
    command = "ansible-playbook ~/kubespray/cluster.yml -i ../ansible/inventories/kubespray/inventory.yml --become --vault-password-file ./pass"
  }

  depends_on = [ 
    terraform_data.start-server-playbook-exec,
    local_sensitive_file.inventory-create
  ]
}

resource "terraform_data" "post-installation-settings" {
  provisioner "local-exec" {
    command = "ansible-playbook ../ansible/playbooks/post-installation-configure.yml --vault-password-file ./pass -i ../ansible/inventories/kubespray/inventory.yml"
  }

  depends_on = [ 
    terraform_data.kubespray-creation-cluster
  ]
}

module "deploy-app" {
  source = "./modules/deploy-app"
  docker-config-key-path = var.docker-config-path
  docker-config-key-name = var.docker-config-name
  connection-to-db-name = var.db-connection-name
  connection-to-db-string = var.db-connection-string

  depends_on = [ 
    terraform_data.post-installation-settings
  ]
}