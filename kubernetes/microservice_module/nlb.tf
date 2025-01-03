# resource "aws_lb" "service" {
#   name               = "${var.service_name}-nlb"
#   internal           = true
#   load_balancer_type = "network"
#   subnets            = var.subnet_ids
# }

# resource "aws_lb_target_group" "service" {
#   name        = "${var.service_name}-tg"
#   port        = 8080
#   protocol    = "TCP"
#   vpc_id      = var.vpc_id
#   target_type = "ip"

#   health_check {
#     enabled             = false
#   }
# }

# resource "aws_lb_listener" "service" {
#   load_balancer_arn = aws_lb.service.arn
#   port              = 8080
#   protocol          = "TCP"

#   default_action {
#     type             = "forward"
#     target_group_arn = aws_lb_target_group.service.arn
#   }
# }