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
module "restaurants_gateway_entry" {
  source = "./microservice_module"
  vpc_id = var.vpc_id
  subnet_ids = var.subnet_ids
  service_name = "restaurants"
  api_id = aws_apigatewayv2_api.main.id
  load_balancer_listener_arn = var.restaurants_service_load_balancer_listener_arn
}

output "restaurants_microservice_url" {
  value = "${aws_apigatewayv2_stage.prod.invoke_url}/${module.restaurants_gateway_entry.service_route_key}"
}

# Route to Reservations Service
module "reservations_gateway_entry" {
  source = "./microservice_module"
  vpc_id = var.vpc_id
  subnet_ids = var.subnet_ids
  service_name = "reservations"
  api_id = aws_apigatewayv2_api.main.id
  load_balancer_listener_arn = var.reservations_service_load_balancer_listener_arn
}

output "reservations_microservice_url" {
  value = "${aws_apigatewayv2_stage.prod.invoke_url}/${module.reservations_gateway_entry.service_route_key}"
}
