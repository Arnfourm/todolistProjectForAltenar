terraform {
  required_providers {
    virtualbox = {
      source = "shekeriev/virtualbox"
      version = "0.0.4"
    }
  }
}

provider "virtualbox" {}

resource "virtualbox_vm" "vm1" {
  name = "test-vm"
  image = "/home/luver/Rocky-9.6-x86_64-minimal.iso"
  cpus = 2
  memory = "2048 mib"
  user_data = file("${path.module}/user_data")
  
  network_adapter {
    type = "bridged"
  }
}
