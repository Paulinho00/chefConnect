data "aws_db_instance" "database" {
  db_instance_identifier = var.db_instance_identifier
}

resource "kubernetes_deployment" "restaurant_service" {
  metadata {
    name = "${var.service_name}-service"
  }

  spec {
    replicas = 2

    selector {
      match_labels = {
        app = "${var.service_name}-service"
      }
    }

    template {
      metadata {
        labels = {
          app = "${var.service_name}-service"
        }
      }

      spec {
        container {
          name  = "restaurant-service"
          image = var.image_name

          env {
            name  = "SPRING_DATASOURCE_URL"
            value = "jdbc:postgresql://${data.aws_db_instance.database.endpoint}:${data.aws_db_instance.database.port}/${data.aws_db_instance.database.db_name}"
          }

          env {
            name  = "SPRING_DATASOURCE_USERNAME"
            value = data.aws_db_instance.database.master_username
          }

          env {
            name  = "SPRING_DATASOURCE_PASSWORD"
            value = var.db_password
          }

          env {
            name  = "SPRING_SECURITY_OAUTH2_RESOURCESERVER_JWT_ISSUER_URI"
            value = "value2"
          }

          env {
            name  = "EVENT_QUEUE_URL"
            value = "value2"
          }

          port {
            container_port = 8080
          }
        }
      }
    }
  }
}

# ClusterIP service for restaurant-service
resource "kubernetes_service" "restaurant_service_clusterip" {
  metadata {
    name = "${var.service_name}-service-clusterip"
  }

  spec {
    selector = {
      app = "${var.service_name}-service"
    }

    port {
      port        = 8080
      target_port = 8080
    }

    type = "ClusterIP"
  }
}

# LoadBalancer service for restaurant-service
resource "kubernetes_service" "restaurant_service_loadbalancer" {
  metadata {
    name = "${var.service_name}-service-loadbalancer"
  }

  spec {
    selector = {
      app = "${var.service_name}-service"
    }

    port {
      port        = 8080
      target_port = 8080
    }

    type = "LoadBalancer"
  }
}

# Output the LoadBalancer External IP
resource "kubernetes_service" "restaurant_service_external" {
  metadata {
    name = "${var.service_name}-service-external"
    annotations = {
      "service.beta.kubernetes.io/aws-load-balancer-type" = "nlb-ip"
      "service.beta.kubernetes.io/aws-load-balancer-internal" = "true"
      "service.beta.kubernetes.io/aws-load-balancer-nlb-target-type" = "ip"
      "service.beta.kubernetes.io/aws-load-balancer-scheme" = "internal"
    }
  }

  spec {
    selector = {
      app = "${var.service_name}-service"
    }

    port {
      name        = "http"
      port        = 8080
      target_port = 8080
      protocol    = "TCP"
    }

    type = "LoadBalancer"
  }
}


# Output the ClusterIP
data "kubernetes_service" "restaurant_service_clusterip" {
  metadata {
    name = kubernetes_service.restaurant_service_clusterip.metadata[0].name
  }
}
data "aws_lb" "k8s_nlb" {
  name = "${var.service_name}-service-external"
  
  depends_on = [
    kubernetes_service.restaurant_service_external
  ]
}

data "aws_lb_listener" "k8s_nlb_listener" {
  load_balancer_arn = data.aws_lb.k8s_nlb.arn
  port              = 8080
}
