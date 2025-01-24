variable "vpc_id" {
  default = "vpc-09341858539bcd7ff"
}

variable "subnet_ids" {
  default = [
  "subnet-0ce120a85b257f63e",
  "subnet-0e3949e9b867315d7",
]
}

variable "restaurants_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:923333015021:listener/net/a2931bebffb2b413f86b0a19e9650d3f/5f710eef1ce6d29f/50595962e8f65694"
}

variable "reservations_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:923333015021:listener/net/a28a56c05f23f4a95967d2387ef4d7e2/9c590e3e41c5e2ca/2c4edc06fd23b1e1"
}