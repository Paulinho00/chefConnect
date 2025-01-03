provider "kubernetes" {
  config_path = "C:\\Users\\Paweł\\.kube\\config"
}

provider "aws" {
  region = "us-east-1"
}

data "aws_iam_role" "lab_role" {
  name = "LabRole"
}

data  "aws_apigatewayv2_api" "api_gateway" {
  api_id = var.gateway_id
}

module "restaurant_service" {
  source = "./microservice_module"
  image_name = var.image_name
  db_password = var.db_password
  db_instance_identifier = var.db_identifier
  service_name = "restaurant"
  security_group_ids = var.security_group_ids
  cluster_name = var.cluster_name
  subnet_ids = var.subnet_ids
  role_arn = data.aws_iam_role.lab_role.arn
  vpc_id = var.vpc_id
  api_gateway_id = data.aws_apigatewayv2_api.api_gateway.api_id
}


# Deployment
resource "aws_apigatewayv2_deployment" "main" {
  api_id = data.aws_apigatewayv2_api.api_gateway.api_id

  depends_on = [
    module.restaurant_service,
  ]
}

# Stage
resource "aws_apigatewayv2_stage" "main" {
  api_id        = data.aws_apigatewayv2_api.api_gateway.api_id
  name          = "prod"
  deployment_id = aws_apigatewayv2_deployment.main.id

  auto_deploy = true  
}

# Output the API Gateway URL
output "api_gateway_url" {
  value = "${data.aws_apigatewayv2_api.api_gateway.api_endpoint}/${aws_apigatewayv2_stage.main.name}"
}