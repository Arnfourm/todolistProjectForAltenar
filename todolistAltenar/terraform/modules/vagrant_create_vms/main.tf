terraform {
  required_providers {
    vagrant = {
        source = "bmatcuk/vagrant"
    }
  }
}

resource "vagrant_vm" "createVm" {
  name = "vagrantCreation"
  vagrantfile_dir = var.vagrantfile_dir_path

  env = {
    vm_cpus = 4
    vm_memory = 4096
    vm_bridge_interface = var.vagrantfile_bridge_interface
    vm_diskstorage_size = var.vagrant_diskstorage
  }
}