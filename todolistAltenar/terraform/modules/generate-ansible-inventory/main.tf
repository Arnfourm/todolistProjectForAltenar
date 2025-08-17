resource "ansible_vault" "inventory_file" {
  vault_file = "../ansible-test/inventory-test.yml"
  vault_password_file = var.vault-password-path
}

locals {
  inventory_decode = yamldecode(ansible_vault.inventory_file.yaml)
}

resource "ansible_group" "master-node" {
  name = "master-node"
}

resource "ansible_group" "kube-control-plan" {
  name = "kube-control-plane"
  children = [ "master-node" ]
}