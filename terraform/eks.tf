resource "aws_eks_cluster" "main" {
  name     = "microservices-cluster"
  role_arn = data.aws_iam_role.lab_role.arn

  vpc_config {
    subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
  }
}

resource "aws_apigatewayv2_api" "main" {
  name          = "microservices-api"
  protocol_type = "HTTP"
}

resource "aws_security_group" "vpc_link" {
  name        = "api-gateway-vpc-link"
  description = "Security group for API Gateway VPC Link"
  vpc_id      = aws_vpc.microservice_main.id

  ingress {
    from_port   = 8080
    to_port     = 8080
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 8080
    to_port     = 8080
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "api-gateway-vpc-link-sg"
  }
}

output "cluster_name" {
  value = aws_eks_cluster.main.name
}

output "api_gateway_id" {
  value = aws_apigatewayv2_api.main.id
}

output "security_group_id" {
  value = aws_security_group.vpc_link.id
}