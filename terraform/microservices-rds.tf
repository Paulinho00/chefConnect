module "restaurant_service_db" {
  source = "./rds_module"
  username = "postgres"
  password = "postgres"
  db_name = "restaurantDb"
  identifier = "restaurant-db"
  vpc_security_group_ids = [aws_security_group.restaurant_db_security_group.id]
  subnet_ids = [aws_subnet.main_first_subnet.id, aws_subnet.main_second_subnet.id]
}

output "restaurant_ednpoint" {
  value = module.restaurant_service_db.rds_instance_endpoint
}