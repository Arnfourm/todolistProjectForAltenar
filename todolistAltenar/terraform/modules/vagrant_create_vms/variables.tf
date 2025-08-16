# ========== main vars =============== #

variable "vagrantfile_dir_path" {
  type = string
  description = "path of vagrantfile dir"
}

variable "vagrantfile_os_image" {
  type = string
  description = "os image need to use"
}

variable "vagrantfile_os_image_version" {
  type = string
  description = "version of os image"
}

# ========== nodes count ============ #

variable "vagrantfile_count_masters" {
  type = number
  description = "number of master node need to create"
}

variable "vagrantfile_count_workers" {
  type = number
  description = "number of worker node need to create"
}

variable "vagrantfile_count_dbs" {
  type = number
  description = "number of dbs node need to create"
}

# ========== network settings ========== #

variable "vagrantfile_network_interface" {
  type = string
  description = "type of netowkr interface"
}

# ========== resource settings ======= #
variable "vagrantfile_cpus_in_stock" {
  type = number
  description = "cpus count in default node"
}

variable "vagrantfile_ram_in_stock" {
  type = number
  description = "RAM valume in default node"
}

variable "vagrantfile_diskstorage_in_stock" {
  type = string
  description = "diskstorage size in default node"
}