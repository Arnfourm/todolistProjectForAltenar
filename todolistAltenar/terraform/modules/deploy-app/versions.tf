terraform {
  required_providers {
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
