resource "aws_apigatewayv2_vpc_link" "service" {
  name               = "${var.service_name}-vpclink"
  security_group_ids = var.security_group_ids
  subnet_ids         = var.subnet_ids
}

resource "aws_eks_node_group" "node_group" {
  cluster_name    = var.cluster_name
  node_group_name = "${var.service_name}-node-group"
  node_role_arn   = var.role_arn
  subnet_ids      = var.subnet_ids
  instance_types  = ["t3.medium"]

  scaling_config {
    desired_size = 2
    min_size     = 1
    max_size     = 3
  }
}

resource "aws_apigatewayv2_integration" "service" {
  api_id           = var.api_gateway_id
  integration_type = "HTTP_PROXY"
  integration_uri  = data.aws_lb_listener.k8s_nlb_listener.arn
  
  integration_method = "ANY"
  connection_type   = "VPC_LINK"
  connection_id     = aws_apigatewayv2_vpc_link.service.id
}

resource "aws_apigatewayv2_route" "service" {
  api_id    = var.api_gateway_id
  route_key = "ANY /author/{proxy+}"
  target    = "integrations/${aws_apigatewayv2_integration.service.id}"
}