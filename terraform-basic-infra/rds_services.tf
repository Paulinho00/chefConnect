# Restaurant Service Db
module "restaurants_service_db" {
  source = "./rds_module"
  username = "postgres"
  password = "postgres"
  db_name = "restaurantsDb"
  identifier = "restaurants-db"
  vpc_id = aws_vpc.microservice_main.id
  subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
}

output "restaurants_ednpoint" {
  value = module.restaurants_service_db.rds_instance_endpoint
}