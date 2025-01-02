variable "api_gateway_rest_api_id" {
  type        = string
}

variable "api_gateway_rest_api_root_resource_id" {
  type        = string
}

variable "service_name" {
  type        = string
}

variable "apigatewayv2_vpc_link_id"{
    type      = string
}

variable "api_gateway_rest_api_execution_arn" {
  type      = string
}

variable "cluster_name" {
  type      = string
}

variable "subnet_ids" {
  type        = list(string)
}

variable "role_arn" {
  type = string
}