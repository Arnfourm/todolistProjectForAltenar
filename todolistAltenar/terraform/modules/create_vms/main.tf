resource "virtualbox_vm" "vm_create" {
  count = var.vm_number_to_create
  name = "vm-${count.index}"
  image = var.vm_image_path
  cpus = 2
  memory = "2048 mib"
  
  network_adapter {
    type = "bridged"
    host_interface = "wlp1s0"
    device = "IntelPro1000MTServer"
  }

  provisioner "local-exec" {
    command = "sshpass -p ${var.vm_ssh_pass} ssh-copy-id ${var.vm_ssh_name}@${self.network_adapter.0.ipv4_address}"
  }
}