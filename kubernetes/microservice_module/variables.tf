variable "api_gateway_id" {
  type        = string
}

variable "service_name" {
  type        = string
}

variable "cluster_name" {
  type      = string
}

variable "subnet_ids" {
  type        = list(string)
}

variable "security_group_ids" {
  type        = list(string)
}

variable "vpc_id" {
  type = string
}

variable "role_arn" {
  type = string
}

variable "db_instance_identifier" {
  type = string
}

variable "db_password" {
  type = string
}

variable "image_name" {
  type = string
}