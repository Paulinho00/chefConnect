resource "aws_cognito_user_pool" "main_user_pool" {
  name = "main-user-pool"

  password_policy {
    minimum_length    = 8
    require_lowercase = true
    require_numbers   = true
    require_symbols   = true
    require_uppercase = true
  }

  username_attributes = ["email"]
  
  schema {
    name                = "email"
    attribute_data_type = "String"
    required            = true
    mutable            = true
    
    string_attribute_constraints {
      min_length = 1
      max_length = 256
    }
  }
  
  schema {
    name                = "given_name"
    attribute_data_type = "String"
    required            = true
    mutable            = true
    
    string_attribute_constraints {
      min_length = 1
      max_length = 256
    }
  }
  
  schema {
    name                = "family_name"
    attribute_data_type = "String"
    required            = true
    mutable            = true
    
    string_attribute_constraints {
      min_length = 1
      max_length = 256
    }
  }

  auto_verified_attributes = ["email"]
  
  verification_message_template {
    default_email_option = "CONFIRM_WITH_CODE"
    email_subject = "ChefConnect: Potwierdź konto"
    email_message = "Twój kod potwierdzenia: {####}"
  }
}

resource "aws_cognito_user_pool_client" "user_pool_client" {
  name = "my-app-client"
  
  user_pool_id = aws_cognito_user_pool.main_user_pool.id

  generate_secret = false

  explicit_auth_flows = [
    "ALLOW_USER_SRP_AUTH",
    "ALLOW_REFRESH_TOKEN_AUTH",
    "ALLOW_USER_PASSWORD_AUTH"
  ]
}

# # Cognito groups
# resource "aws_cognito_user_group" "admin_group" {
#   user_pool_id = aws_cognito_user_pool.main_user_pool.id
#   name         = "admin"
#   description  = "Administratorzy"
# }

# resource "aws_cognito_user_group" "manager_group" {
#   user_pool_id = aws_cognito_user_pool.main_user_pool.id
#   name         = "manager"
#   description  = "Manadżerowie restauracji"
# }

# resource "aws_cognito_user_group" "pracownik_restauracji_group" {
#   user_pool_id = aws_cognito_user_pool.main_user_pool.id
#   name         = "pracownik-restauracji"
#   description  = "Pracownicy restauracji"
# }

# Output values
output "user_pool_id" {
  value = aws_cognito_user_pool.main_user_pool.id
}

output "client_id" {
  value = aws_cognito_user_pool_client.user_pool_client.id
}

output "issuer_uri" {
  value = "https://cognito-idp.${var.region}.amazonaws.com/${aws_cognito_user_pool.main_user_pool.id}"
}