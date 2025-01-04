variable "db_name" {
  description = "The name of the database"
  type        = string
}

variable "identifier" {
  description = "The unique identifier for the RDS instance"
  type        = string
}

variable "username" {
  description = "Master username for the database"
  type        = string
}

variable "password" {
  description = "Master password for the database"
  type        = string
  sensitive   = true
}

variable "vpc_id" {
  type = string
}

variable "subnet_ids" {
  description = "List of subnet IDs for the DB subnet group"
  type        = list(string)
}