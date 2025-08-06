terraform {
  required_providers {
    vagrant = {
        source = "bmatcuk/vagrant"
    }
  }
}

resource "vagrant_vm" "createVm" {
  name = "vagrantCreation"
  vagrantfile_dir = "/home/luver/todolistProjectForAltenar/todolistAltenar/terraform/modules/vagrant_create_vms/"
}