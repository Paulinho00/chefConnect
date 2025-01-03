variable "vpc_id" {
  default = "vpc-0496466555a3437eb"
}

variable "subnet_ids" {
  default = [  "subnet-03db230e8f8bcfb75", "subnet-0539a4533509f281c"]
}

variable "load_balancer_listener_arn" {
  default = "arn:aws:elasticloadbalancing:us-east-1:935427774165:listener/net/a65b12459b05f41f78ffc85b15881b4d/5ce36f39e0262e93/0a037988daeb2536"
}