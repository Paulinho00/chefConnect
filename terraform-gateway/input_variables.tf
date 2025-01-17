variable "vpc_id" {
  default = "vpc-0585eb2b8226bb0de"
}

variable "subnet_ids" {
  default = [  "subnet-06e942ce7554bc098", "subnet-0f9ace36b3121ef13"]
}

variable "restaurants_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:606529675772:listener/net/a78996514c98c45d78333fb00a12af82/7a343c66a37b937b/d6780747f1c50ec2"
}

variable "reservations_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:606529675772:listener/net/ab4948cdc17034c78b1247e661e78f48/a0bb02ea4713c3ec/a5ad5f4118bbff5e"
}