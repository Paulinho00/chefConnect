variable "vpc_id" {
  default = "vpc-0a2b540e3f85e3d52"
}

variable "subnet_ids" {
  default = [  "subnet-08fbc3622ca1c4ea6", "subnet-0a26826e724ab9665"]
}

variable "restaurants_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:606529675772:listener/net/ab8d6b0205a674a6b9d1719463005170/6bc41956c94a826d/2a6eda70b0725d5d"
}

variable "reservations_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:606529675772:listener/net/a1072554f9da3440ba1e63b3acaf7f3b/ca4daf645f670ff1/48791a432bd6a22b"
}