variable "vm_number_to_create" {
  type = number
  default = 1
  description = "number of vms to create"
}

variable "vm_image_path" {
  type = string
  description = "http or local path to image"
}