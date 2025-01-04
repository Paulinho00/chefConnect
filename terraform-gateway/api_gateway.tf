terraform {
  required_providers {
    aws = {
      source = "hashicorp/aws"
    }
  }
}

resource "aws_apigatewayv2_api" "main" {
  name = "main"
  protocol_type = "HTTP"
}

resource "aws_apigatewayv2_stage" "prod" {
  api_id = aws_apigatewayv2_api.main.id

  name="prod"
  auto_deploy = true
}


# Route to Restaurant Service
module "restaurant_gateway_entry" {
  source = "./microservice_module"
  vpc_id = var.vpc_id
  subnet_ids = var.subnet_ids
  service_name = "restaurant"
  api_id = aws_apigatewayv2_api.main.id
  load_balancer_listener_arn = var.load_balancer_listener_arn
}

output "restaurant_url" {
  value = "${aws_apigatewayv2_stage.prod.invoke_url}/restaurant"
}

