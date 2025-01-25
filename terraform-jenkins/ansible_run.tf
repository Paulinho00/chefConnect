resource "terraform_data" "ansible_provisioner_initialize" {
  provisioner "local-exec" {
    working_dir = "ansible/"
    command     = "ANSIBLE_CONFIG=./ansible.cfg ansible-playbook initialize.yml"
  }
  depends_on = [
    terraform_data.ssh_connection_build,
    terraform_data.ssh_connection_jenkins,
  ]
}

resource "terraform_data" "ansible_provisioner_initialize_jenkins" {
  provisioner "local-exec" {
    working_dir = "ansible/"
    command     = "ANSIBLE_CONFIG=./ansible.cfg ansible-playbook initialize_jenkins.yml"
  }
  depends_on = [
    terraform_data.ansible_provisioner_initialize
  ]
}

resource "terraform_data" "ansible_provisioner_agents" {
  provisioner "local-exec" {
    working_dir = "ansible/"
    command     = "ANSIBLE_CONFIG=./ansible.cfg ansible-playbook initialize_agents.yml"
  }
  depends_on = [
    local_file.ip,
    terraform_data.ansible_provisioner_initialize_jenkins,
  ]
}