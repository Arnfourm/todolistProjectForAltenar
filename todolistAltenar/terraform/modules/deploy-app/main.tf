# resource "kubernetes_manifest" "metalLB-config-p1" {
#   manifest = {
#     "apiVersion" = "metallb.io/v1beta1"
#     "kind" = "IPAddressPool"
#     "metadata" = {
#         "name" = "metallb-configure"
#         "namespace" = "metallb-system"
#     }
#     "spec" = {
#         "addresses" = [
#             "192.168.0.240-192.168.0.250"
#         ]
#         "autoAssign" = "true"
#     }
#   }
# }

# resource "kubernetes_manifest" "metalLB-config-p2" {
#   manifest = {
#     "apiVersion" = "metallb.io/v1beta1"
#     "kind" = "L2Advertisement"
#     "metadata" = {
#         "name" = "metallb-configure"
#         "namespace" = "metallb-system"
#     }
#     "spec" = {
#         "ipAddressPools" = [
#             "metallb-configure"  
#         ]
#     }
#   }
# }

resource "kubectl_manifest" "metalLB-config-p1" {
  yaml_body = <<YAML
apiVersion: metallb.io/v1beta1
kind: IPAddressPool
metadata:
  name: metallb-configure
  namespace: metallb-system
spec:
  addresses:
  - 192.168.0.240-192.168.0.250
  autoAssign: true
  YAML
}

resource "kubectl_manifest" "metalLB-config-p2" {
    yaml_body = <<YAML
apiVersion: metallb.io/v1beta1
kind: L2Advertisement
metadata:
  name: metallb-configure
  namespace: metallb-system
spec:
  ipAddressPools:
  - metallb-configure
    YAML
}

resource "helm_release" "deploy-nginx-ingress-controller" {
  name = "deploy-ingress-nginx"
  repository = "https://kubernetes.github.io/ingress-nginx"
  chart = "ingress-nginx"
  namespace = "ingress-nginx"
  create_namespace = "true"
}

resource "kubectl_manifest" "image-pull-secrets" {
  yaml_body = <<YAML
apiVersion: v1
kind: Secret
metadata:
  name: ${var.docker-config-key-name}
data:
  .dockerconfigjson: ${base64encode("${file("${var.docker-config-key-path}")}")}
type: kubernetes.io/dockerconfigjson
  YAML
}

resource "kubectl_manifest" "db-connection-string" {
  yaml_body = <<YAML
apiVersion: v1
kind: Secret
metadata:
  name: ${var.connection-to-db-name}
data:
  connection-string: ${var.connection-to-db-string}
type: opaque
  YAML
}

# resource "kubernetes_secret" "image-pull-secrets" {
#   metadata {
#     name = "${var.docker-config-key-name}"
#   }

#   data = {
#     ".dockerconfigjson" = "${file("${var.docker-config-key-path}")}"
#   }

#   type = "kubernetes.io/dockerconfigjson"
# }

# resource "kubernetes_secret" "name" {
#   metadata {
#     name = "${var.connection-to-db-name}"
#   }

#   data = {
#     connection-string: "${var.connection-to-db-string}"
#   }

#   type = "opaque"
# }

resource "helm_release" "create-deploy-frontend" {
  name = "deploy-frontend"
  chart = "../helm-charts/deploy-app/"

  values = [
    file("../helm-charts/deploy-app/values-frontend.yaml")
  ]
}

resource "helm_release" "create-deploy-backend" {
  name = "deploy-backend"
  chart = "../helm-charts/deploy-app/"

  values = [
    file("../helm-charts/deploy-app/values-backend.yaml")
  ]
}

resource "helm_release" "create-deploy-ingress" {
  name = "deploy-ingress"
  chart = "../helm-charts/deploy-app/"

  values = [
    file("../helm-charts/deploy-app/values-ingress.yaml")
  ]
}