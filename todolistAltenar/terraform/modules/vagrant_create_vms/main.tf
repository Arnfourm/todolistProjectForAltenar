resource "vagrant_vm" "createVm" {
  name = "vagrantCreation"
  vagrantfile_dir = var.vagrantfile_dir_path

  env = {
    vm_cpus = var.vagrantfile_cpus_in_stock
    vm_memory = var.vagrantfile_ram_in_stock
    vm_network_interface = var.vagrantfile_network_interface
    vm_diskstorage_size = var.vagrantfile_diskstorage_in_stock
    vm_os_image = var.vagrantfile_os_image
    vm_os_image_version = var.vagrantfile_os_image_version
    vm_masters_count = var.vagrantfile_count_masters
    vm_workers_count = var.vagrantfile_count_workers
    vm_dbs_count = var.vagrantfile_count_dbs
  }
}