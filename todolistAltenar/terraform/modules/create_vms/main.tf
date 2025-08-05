terraform {
  required_providers {
    virtualbox = {
      source = "terra-farm/virtualbox"
      version = "0.2.2-alpha.1"
    }
  }
}

provider "virtualbox" {}

resource "virtualbox_vm" "vm_create" {
  count = var.vm_number_to_create
  name = "vm-${count.index}"
  image = var.vm_image_path
  cpus = 2
  memory = "2048 mib"
  # user_data = file("${path.module}/user_data")
  
  network_adapter {
    type = "bridged"
    host_interface = "wlp1s0"
    device = "IntelPro1000MTServer"
  }
}