resource "aws_api_gateway_resource" "service" {
  rest_api_id = var.api_gateway_rest_api_id
  parent_id   = var.api_gateway_rest_api_root_resource_id
  path_part   = "${var.service_name}-service"
}

resource "aws_api_gateway_method" "service" {
  rest_api_id   = var.api_gateway_rest_api_id
  resource_id   = aws_api_gateway_resource.service.id
  http_method   = "ANY"
  authorization = "NONE"
}

# Update API Gateway integration
resource "aws_api_gateway_integration" "service" {
  rest_api_id = var.api_gateway_rest_api_id
  resource_id = aws_api_gateway_resource.service.id
  http_method = aws_api_gateway_method.service.http_method
  
  type                    = "HTTP_PROXY"
  uri                     = "http://${var.service_name}.default.svc.cluster.local"
  integration_http_method = "ANY"
  
  connection_type = "VPC_LINK"
  connection_id   = var.apigatewayv2_vpc_link_id
}