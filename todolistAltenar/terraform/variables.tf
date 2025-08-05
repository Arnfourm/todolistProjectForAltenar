variable "vm_number_to_create" {
  type = number
  default = 2
}

variable "vm_image_path" {
  type = string
  default = "/home/luver/Downloads/virtualbox.box"
  description = "Now using ubuntu image from example of this provider, because another images is unusable"
}