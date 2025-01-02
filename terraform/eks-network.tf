# VPC
resource "aws_vpc" "microservice_main" {
  cidr_block           = "10.0.0.0/16"
  enable_dns_hostnames = true
  enable_dns_support   = true

  tags = {
    Name = "microservices-vpc"
  }
}

resource "aws_subnet" "microservices_private_1" {
  vpc_id            = aws_vpc.microservice_main.id
  cidr_block        = "10.0.1.0/24"
  tags = {
    Name = "microservices-private-subnet-1"
    "kubernetes.io/role/internal-elb" = "1"
  }
}

resource "aws_subnet" "microservices_private_2" {
  vpc_id            = aws_vpc.microservice_main.id
  cidr_block        = "10.0.2.0/24"

  tags = {
    Name = "microservices-private-subnet-2"
    "kubernetes.io/role/internal-elb" = "1"
  }
}

resource "aws_eip" "microservices_nat" {
  domain = "vpc"
}

resource "aws_nat_gateway" "microservices_main" {
  allocation_id = aws_eip.microservices_nat.id
  subnet_id     = aws_subnet.microservices_public_1.id

  tags = {
    Name = "microservices-nat"
  }
}

resource "aws_route_table" "microservices_private" {
  vpc_id = aws_vpc.microservice_main.id

  route {
    cidr_block     = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.microservices_main.id
  }

  tags = {
    Name = "microservices-private-route-table"
  }
}

resource "aws_route_table_association" "microservices_private_1" {
  subnet_id      = aws_subnet.microservices_private_1.id
  route_table_id = aws_route_table.microservices_private.id
}

resource "aws_route_table_association" "microservices_private_2" {
  subnet_id      = aws_subnet.microservices_private_2.id
  route_table_id = aws_route_table.microservices_private.id
}

resource "aws_subnet" "microservices_public_1" {
  vpc_id            = aws_vpc.microservice_main.id
  cidr_block        = "10.0.101.0/24"
  map_public_ip_on_launch = true

  tags = {
    Name = "microservices-public-subnet-1"
  }
}

resource "aws_internet_gateway" "microservices_main" {
  vpc_id = aws_vpc.microservice_main.id

  tags = {
    Name = "microservices-igw"
  }
}

resource "aws_route_table" "microservices_public" {
  vpc_id = aws_vpc.microservice_main.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.microservices_main.id
  }

  tags = {
    Name = "microservices-public-route-table"
  }
}

resource "aws_route_table_association" "microservices_public_1" {
  subnet_id      = aws_subnet.microservices_public_1.id
  route_table_id = aws_route_table.microservices_public.id
}