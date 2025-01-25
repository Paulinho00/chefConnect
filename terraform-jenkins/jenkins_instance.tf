# JENKINS EC2
resource "aws_instance" "jenkins" {
  ami                    = "ami-0866a3c8686eaeeba"
  instance_type          = "t3.medium"
  vpc_security_group_ids = [aws_security_group.web-server-sg.id]
  key_name = "vockey"
  tags = {
    Name = "JENKINS"
  }
}

# Tymczasowe połączenia, do sprawdzenia czy maszyna jest dostępna
resource "terraform_data" "ssh_connection_jenkins" {
  provisioner "remote-exec" {
    inline = [
      "echo \"$HOSTNAME connected...\""
    ]
  }

  connection {
    type        = "ssh"
    user        = "ubuntu"
    private_key = file(pathexpand("~/.ssh/vockey.pem"))
    host        = aws_instance.jenkins.public_ip
  }
}

output "jenkins_public_ip" {
  value = aws_instance.jenkins.public_ip
}
