module "Vms-creation" {
  source = "./modules/create_vms"
  vm_number_to_create = 2
  vm_image_path = "/home/luver/Downloads/virtualbox.box"
}