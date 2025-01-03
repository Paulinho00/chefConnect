# Restaurant Service Db
module "restaurant_service_db" {
  source = "./rds_module"
  username = "postgres"
  password = "postgres"
  db_name = "restaurantDb"
  identifier = "restaurant-db"
  vpc_security_group_ids = [aws_security_group.restaurant_db_security_group.id]
  subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
}

output "restaurant_ednpoint" {
  value = module.restaurant_service_db.rds_instance_endpoint
}