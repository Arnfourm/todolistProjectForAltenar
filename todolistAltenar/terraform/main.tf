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
  name = "test-vm"
  image = "https://app.vagrantup.com/ubuntu/boxes/bionic64/versions/20180903.0.0/providers/virtualbox.box"
  cpus = 2
  memory = "2048 mib"
  user_data = file("${path.module}/user_data")
  
  network_adapter {
    type = "bridged"
    host_interface = "wlp1s0"
  }
}