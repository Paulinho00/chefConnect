output "service_endpoint" {
  value = "${var.api_gateway_rest_api_execution_arn}/${var.service_name}"
}