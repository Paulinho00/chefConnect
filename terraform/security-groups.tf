resource "aws_security_group" "restaurant_db_security_group" {
  name = "allow_access_to_restaurant_db"
  description = "Allow access to DB from my IP and backend"
  vpc_id = aws_vpc.main_vpc.id

  ingress {
    from_port = 5432
    to_port = 5432
    protocol = "tcp"
    cidr_blocks = ["${var.my_public_ip}/32"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}