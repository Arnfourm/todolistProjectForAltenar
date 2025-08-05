output "IpAddr-1" {
  value = element(virtualbox_vm.vm_create.*.network_adapter.0.ipv4_address, 1)
}

output "IpAddr-2" {
  value = element(virtualbox_vm.vm_create.*.network_adapter.0.ipv4_address, 2)
}