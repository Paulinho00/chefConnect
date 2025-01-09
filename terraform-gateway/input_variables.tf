variable "vpc_id" {
  default = "vpc-04f25b68e9ef26379"
}

variable "subnet_ids" {
  default = [  "subnet-0a1e2e6af9c3ed5dd", "subnet-028e1d9c94c9ea207"]
}

variable "restaurants_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:606529675772:listener/net/a9bdf2762fec24708aa72b7fd8fb81f1/7f6162d396336d8f/d98eedffbac0af66"
}

variable "reservations_service_load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:606529675772:listener/net/a404504eff26b434cb24d6351b1cf36e/cd32ab333679dbef/380fac7df7e6549d"
}