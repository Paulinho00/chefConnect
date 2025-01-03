resource "aws_eks_cluster" "main" {
  name     = "microservices-cluster"
  role_arn = data.aws_iam_role.lab_role.arn

  vpc_config {
    subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
  }
}

output "cluster_name" {
  value = aws_eks_cluster.main.name
}
