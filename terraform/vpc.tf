# resource "aws_vpc" "main_vpc" {
#   cidr_block = "10.0.0.0/16"
#   enable_dns_support = true
#   enable_dns_hostnames = true
#   tags = {
#     Name = "main-vpc"
#   }
# }

# resource "aws_subnet" "main_first_subnet" {
#   vpc_id     = aws_vpc.main_vpc.id
#   availability_zone = "us-east-1a"
#   cidr_block = "10.0.1.0/24"
#   map_public_ip_on_launch = true

#   tags = {
#     Name = "main-first-subnet"
#   }
# }

# resource "aws_subnet" "main_second_subnet" {
#   vpc_id     = aws_vpc.main_vpc.id
#   availability_zone = "us-east-1b"
#   cidr_block = "10.0.2.0/24"
#   map_public_ip_on_launch = true

#   tags = {
#     Name = "main-second-subnet"
#   }
# }

# resource "aws_internet_gateway" "internet_gateway" {
#   vpc_id = aws_vpc.main_vpc.id
#   tags = {
#     Name = "main-igw"
#   }
# }

# resource "aws_route_table" "main_route_table" {
#   vpc_id = aws_vpc.main_vpc.id

#   tags = {
#     Name = "main-route-table"
#   }
# }

# resource "aws_route_table_association" "gateway_route_association_first_subnet" {
#   subnet_id     = aws_subnet.main_first_subnet.id
#   route_table_id = aws_route_table.main_route_table.id
# }

# resource "aws_route_table_association" "gateway_route_association_second_subnet" {
#   subnet_id     = aws_subnet.main_second_subnet.id
#   route_table_id = aws_route_table.main_route_table.id
# }

# resource "aws_route" "main" {
#   route_table_id          = aws_route_table.main_route_table.id
#   destination_cidr_block = "0.0.0.0/0"
#   gateway_id              = aws_internet_gateway.internet_gateway.id 
# }