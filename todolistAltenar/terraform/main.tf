terraform {
  required_providers {
    virtualbox = {
      source = "terra-farm/virtualbox"
      version = "0.2.2-alpha.1"
    }
    vagrant = {
      source = "bmatcuk/vagrant"
    }
  }
}

provider "vagrant" {}

module "Vms-create-via-Vagrant" {
  source = "./modules/vagrant_create_vms"
}

#provider "virtualbox" {}

# module "Vms-creation" {
#  source = "./modules/create_vms"
#  vm_number_to_create = 2
#  vm_image_path = "/home/luver/Downloads/virtualbox.box"
#  vm_ssh_name = var.ssh_name
#  vm_ssh_pass = var.ssh_pass
#}