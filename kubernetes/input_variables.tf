variable "image_name" {
  default="259136/restaurant_service:latest"
}

variable "db_password" {
  default = "postgres"
}

variable "db_identifier" {
  default = "restaurant-db"
}

variable "cluster_name" {
  default = "microservices-cluster"
}

variable "subnet_ids" {
  default = [  "subnet-062a229b7abdd49c3", "subnet-0d18388c650237d7b"]
}

variable "security_group_ids" {
  default = ["sg-0857266c566219bf4"]
}

variable "vpc_id" {
  default = "vpc-088e4e3b646f170ff"
}

variable "gateway_id" {
  default = "2esx606g9c"
}
