terraform {
  required_providers {
    virtualbox = {
      source = "terra-farm/virtualbox"
      version = "0.2.2-alpha.1"
    }
    vagrant = {
      source = "bmatcuk/vagrant"
    }
    ansible = {
      source = "ansible/ansible"
      version = "~> 1.3.0"
    }
  }
}

module "Vms-create-via-Vagrant" {
  source = "./modules/vagrant_create_vms"
  vagrantfile_dir_path = var.vagrantfile_path
  vagrantfile_bridge_interface = var.bridge_interface
  vagrant_diskstorage = var.diskstorage_size
}

resource "terraform_data" "start-server-playbook-exec" {
  provisioner "local-exec" {
    command = "ansible-playbook ../ansible/playbooks/startServer.yml --vault-password-file ./pass -i ../ansible/inventories/infra/hosts.yml"
  }

  depends_on = [ 
    module.Vms-create-via-Vagrant
  ]
}

resource "terraform_data" "db_creation" {
  provisioner "local-exec" {
    command = "ansible-playbook ../ansible/playbooks/db-deploy.yml --vault-password-file ./pass -i ../ansible/inventories/infra/hosts.yml"
  }  

  depends_on = [ 
    terraform_data.start-server-playbook-exec
  ]
}

resource "terraform_data" "kubespray-creation-cluster" {
  provisioner "local-exec" {
    command = "ansible-playbook ~/kubespray/cluster.yml -i ../ansible/inventories/kubespray/inventory.yml --user=vagrant --become  --private-key=~/.ssh/id_rsa --vault-password-file ./pass"
  }

  depends_on = [ 
    terraform_data.start-server-playbook-exec
  ]
}

resource "terraform_data" "post-installation-settings" {
  provisioner "local-exec" {
    command = "ansible-playbook ../ansible/playbooks/post-installation-configure.yml --vault-password-file ./pass -i ../ansible/inventories/infra/hosts.yml"
  }

  depends_on = [ 
    terraform_data.kubespray-creation-cluster
  ]
}