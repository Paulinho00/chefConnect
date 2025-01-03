resource "aws_security_group" "service_security_group" {
  name = "allow_access_to_${var.service_name}_service"
  description = "Allow access to service"
  vpc_id = var.vpc_id

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_apigatewayv2_vpc_link" "eks" {
  name = "${var.service_name}-vpc-link"
  security_group_ids = [aws_security_group.service_security_group.id]
  subnet_ids = var.subnet_ids
}

resource "aws_apigatewayv2_integration" "eks" {
  api_id = var.api_id
  integration_uri = var.load_balancer_listener_arn
  integration_type = "HTTP_PROXY"
  integration_method = "ANY"
  connection_type = "VPC_LINK"
  connection_id = aws_apigatewayv2_vpc_link.eks.id
}

resource "aws_apigatewayv2_route" "main" {
  api_id = var.api_id
  route_key = "ANY /restaurants/"
  target = "integreations/${aws_apigatewayv2_integration.eks.id}"
}