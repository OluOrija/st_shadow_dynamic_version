#################################################################################################################
# Configure the Azure provider
### Terraform Global Config
# Variables
variable "tenant_id" {
  type = string
}

variable "subscription_id" {
  type = string
}

variable "client_id" {
  type = string
}

variable "client_secret" {
  type = string
}

terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.116.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = ">= 2.0.0"
    }
  }
  required_version = ">= 0.14.9"
  backend "azurerm" {
    resource_group_name = "stshadow-core-rg"
    storage_account_name = "tfbackendsa8091"
    container_name       = "tfbackend-states"
    key                  = "fromlocalruns/stshadow-iac.tfstate"
    use_msi = true
  }  
}
provider "azurerm" {
  features {}
  subscription_id = var.subscription_id
  client_id       = var.client_id
  client_secret   = var.client_secret
  tenant_id       = var.tenant_id
}
### Terraform Config
#################################################################################################################