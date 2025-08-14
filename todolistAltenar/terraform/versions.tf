terraform {
  required_providers {
    virtualbox = {
      source = "terra-farm/virtualbox"
      version = "0.2.2-alpha.1"
    }
    vagrant = {
      source = "bmatcuk/vagrant"
    }
    ansible = {
      source = "ansible/ansible"
      version = "~> 1.3.0"
    }
    helm = {
      version = "3.0.2"
    }
    kubernetes = {
      version = "2.38.0"
    }
    kubectl = {
      source = "gavinbunney/kubectl"
      version = "1.19.0"
    }
  }
}