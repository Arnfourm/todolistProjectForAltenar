variable "vagrantfile_path" {
  type = string
  description = "vagrantfile path"
  default = "./modules/vagrant_create_vms"
}

variable "bridge_interface" {
  type = string
  description = "network bridge interface"
  default = "wlp1s0"
}

variable "diskstorage_size" {
  type = string
  description = "disk storage for single VM"
  default = "20GB"
}