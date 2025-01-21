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
  value="jdbc:postgresql://${module.restaurants_service_db.rds_instance_endpoint}/${module.restaurants_service_db.rds_db_name}"
}

# Reservation Service Db
module "reservations_service_db" {
  source = "./rds_module"
  username = "postgres"
  password = "postgres"
  db_name = "reservationsDb"
  identifier = "reservations-db"
  vpc_id = aws_vpc.microservice_main.id
  subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
}

module "reservations_service_db" {
  source = "./rds_module"
  username = "postgres"
  password = "postgres"
  db_name = "analyticsDb"
  identifier = "analytics-db"
  vpc_id = aws_vpc.microservice_main.id
  subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
}

output "reservations_ednpoint" {
  value="jdbc:postgresql://${module.reservations_service_db.rds_instance_endpoint}/${module.reservations_service_db.rds_db_name}"
}