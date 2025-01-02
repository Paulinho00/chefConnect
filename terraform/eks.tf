resource "aws_eks_cluster" "main" {
  name     = "microservices-cluster"
  role_arn = data.aws_iam_role.lab_role.arn

  vpc_config {
    subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
  }
}

resource "aws_api_gateway_rest_api" "microservices" {
  name = "microservices-api"
}

resource "aws_apigatewayv2_vpc_link" "main" {
  name               = "microservices-vpc-link"
  security_group_ids = [aws_security_group.vpc_link.id]
  subnet_ids         = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_1.id]
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
    cidr_blocks = [aws_vpc.microservice_main.cidr_block]
  }

  tags = {
    Name = "api-gateway-vpc-link-sg"
  }
}

# Add services here
# Restaurant Service
module "restaurant_service" {
  source = "./microservice_module"
  api_gateway_rest_api_id = aws_api_gateway_rest_api.microservices.id
  api_gateway_rest_api_root_resource_id = aws_api_gateway_rest_api.microservices.root_resource_id
  service_name = "restaurant-service"
  apigatewayv2_vpc_link_id = aws_apigatewayv2_vpc_link.main.id
  api_gateway_rest_api_execution_arn = aws_api_gateway_rest_api.microservices.execution_arn
}
