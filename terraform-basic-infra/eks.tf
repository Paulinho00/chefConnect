resource "aws_eks_cluster" "main" {
  name     = "microservices-cluster"
  role_arn = data.aws_iam_role.lab_role.arn

  vpc_config {
    subnet_ids = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]
  }
}

resource "aws_eks_node_group" "main" {
  cluster_name    = aws_eks_cluster.main.name
  node_group_name = "main-node-group"
  node_role_arn   = data.aws_iam_role.lab_role.arn
  subnet_ids      = [aws_subnet.microservices_private_1.id, aws_subnet.microservices_private_2.id]

  scaling_config {
    desired_size = 3
    max_size     = 5
    min_size     = 1
  }

  update_config {
    max_unavailable = 1
  }
}

output "cluster_name" {
  value = aws_eks_cluster.main.name
}
