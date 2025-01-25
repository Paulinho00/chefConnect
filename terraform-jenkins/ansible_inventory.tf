# Konfiguracja pliku inventory dla Ansible
resource "local_file" "ip" {
  content  = <<EOT
  [agents]
  ${aws_instance.build.public_ip}
  [jenkins]
  ${aws_instance.jenkins.public_ip}
  EOT
  filename = "ansible/inventory.ini"
}