variable "vpc_id" {
  type = string
}

variable "subnet_ids" {
  type = list(string)
}

variable "service_name" {
  type = string
}

variable "api_id" {
  type = string
}

variable "load_balancer_listener_arn" {
  type = string
}