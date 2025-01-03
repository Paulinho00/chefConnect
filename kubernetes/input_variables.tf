variable "vpc_id" {
  default = "vpc-0a6b7465e538dbb61"
}

variable "subnet_ids" {
  default = [  "subnet-062a229b7abdd49c3", "subnet-0d18388c650237d7b"]
}

variable "load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:935427774165:listener/net/a4a032f2d8a614954bfe99e64a21af2e/5762cd50f03151f1/39f3b3157ab3c749"
}