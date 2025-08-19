# ========= vms variables =========== #
variable "count_masters" {
  type = number
  default = 1
}

variable "count_workers" {
  type = number
  default = 2
}

variable "count_dbs" {
  type = number
  default = 1
}

variable "network_interface" {
  type = string
  description = "network bridge interface"
  default = "wlp1s0"
}

variable "diskstorage_size" {
  type = string
  description = "disk storage for single VM"
  default = "35GB"
}

variable "cpus_count" {
  type = number
  default = 2
}

variable "ram_volume" {
  type = number
  default = 2048
}

# ========== k8s variables =========== #
variable "docker-config-name" {
  type = string
  sensitive = true
}

variable "docker-config-path" {
  type = string
  sensitive = true
}

variable "db-connection-name" {
  type = string
  sensitive = true
}

variable "db-connection-string" {
  type = string
  sensitive = true
}