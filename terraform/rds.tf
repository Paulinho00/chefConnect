# module "chat_app_rds" {
#   source = "./modules/rds"
#
#   db_name               = "chat_app_db"
#   identifier            = "chat-app-db"
#   username              = "postgres"
#   password              = "password"
#   vpc_security_group_ids = [aws_security_group.chat_app_db_sg.id]
#   subnet_ids            = [aws_subnet.public_subnet_az1.id, aws_subnet.public_subnet_az2.id]
# }