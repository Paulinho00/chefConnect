# BUILD EC2
resource "aws_instance" "build" {
  ami                    = "ami-0866a3c8686eaeeba"
  instance_type          = "t2.micro"
  vpc_security_group_ids = [aws_security_group.web-server-sg.id]
  key_name = "vockey"
  tags = {
    Name = "BUILD"
  }
}

# Tymczasowe połączenia, do sprawdzenia czy maszyna jest dostępna
resource "terraform_data" "ssh_connection_build" {
  provisioner "remote-exec" {
    inline = [
      "echo \"$HOSTNAME connected...\""
    ]
  }

  connection {
    type        = "ssh"
    user        = "ubuntu"
    private_key = file(pathexpand("~/.ssh/vockey.pem"))
    host        = aws_instance.build.public_ip
  }
}

output "build_public_ip" {
  value = aws_instance.build.public_ip
}
