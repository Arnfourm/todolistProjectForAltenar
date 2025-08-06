variable "vm_number_to_create" {
  type = number
  default = 1
  description = "number of vms to create"
}

variable "vm_image_path" {
  type = string
  description = "http or local path to image"
}

variable "vm_ssh_name" {
  type = string
  description = "name to acces via ssh connection"
  #sensitive = true
}

variable "vm_ssh_pass" {
  type = string
  description = "password to access via ssh connection"
  #sensitive = true
}