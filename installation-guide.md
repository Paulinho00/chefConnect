# Infrastructure 
### Prerequisites
- Terraform installed
- AWS CLI installed
- Kubernetes installed
### Steps
1. Run `terraform apply` in `terraform-basic-infra` directory
1. Replace all `<PLACEHOLDER>` with values from terraform output in configs in `kubernetes` directory
1. Run  `aws eks update-kubeconfig --name microservices-cluster --region us-east-1` in your local command shell
1. Run `kubectl apply -f <path_to_config>` for each config in `kubernetes` directory. E.g. `kubectl apply -f ./restaurants-service.yml`. Make sure that restaurant service is deployed as first.
1. Check if all pods are in `Running` state with `kubectl get pod` command
1. Replace all values in file `terraform-gateway/input_variables.tf`. Take VPC id and subnet ids from terraform output. To replace load balancer ARN, you have to go to AWS site and copy it from there
1. Run `terraform apply` in `terraform-gateway` directory
1. You should see URLs for each microservice in terraform output
