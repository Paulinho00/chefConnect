output "rds_instance_endpoint" {
  value = aws_db_instance.rds_instance.endpoint
}

output "rds_instance_id" {
  value = aws_db_instance.rds_instance.id
}

output "rds_username" {
  value = aws_db_instance.rds_instance.username
}

output "rds_password" {
  value = aws_db_instance.rds_instance.password
}