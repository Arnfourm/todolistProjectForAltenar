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

provider "vagrant" {}

module "Vms-create-via-Vagrant" {
  source = "./modules/vagrant_create_vms"
  vagrantfile_dir_path = var.vagrantfile_path
  vagrantfile_bridge_interface = var.bridge_interface
  vagrant_diskstorage = var.diskstorage_size
}

resource "terraform_data" "start-server-playbook-exec" {
  provisioner "local-exec" {
    command = "ansible-playbook ../ansible/playbooks/startServer.yml --vault-password-file ./pass"
  }

  depends_on = [ 
    module.Vms-create-via-Vagrant
  ]
}

# resource "ansible_vault" "secrets" {
#  vault_file = "../ansible/inventories/group_vars/backend-hosts.yml"
#  vault_password_file = "./pass"
# }