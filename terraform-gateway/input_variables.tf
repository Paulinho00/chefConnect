variable "vpc_id" {
  default = "vpc-00e3810aab7b848bd"
}

variable "subnet_ids" {
  default = [  "subnet-0a4631cb9e0d37451", "subnet-0ccbf2bfe5b26920e"]
}

variable "restaurants_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:935427774165:listener/net/a5ecd254d32a94ab1836a9b3e50a4547/94085b75ffe21b75/85d15c2dce7af0ae"
}