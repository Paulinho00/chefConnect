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
  default = "arn:aws:elasticloadbalancing:us-east-1:606529675772:listener/net/a50102552a0b04ac29f602491788d768/091e10c2f567ab51/e614f897be961377"
}