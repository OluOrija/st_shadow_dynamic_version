data "azurerm_resource_group" "rg" {
  name     = var.core_rg_name
}

resource "azurerm_key_vault" "keyvault" {
  name                        = var.core_keyvault_name
  location                    = data.azurerm_resource_group.rg.location
  resource_group_name         = data.azurerm_resource_group.rg.name
  sku_name                    = "standard"
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  purge_protection_enabled    = true
  soft_delete_retention_days  = 90
}

resource "azurerm_key_vault_secret" "jwt_key" {
  name         = "Jwt-Key"
  value        = "ThisIsA32CharacterMinimumKeyForHS256"
  key_vault_id = azurerm_key_vault.keyvault.id
}

resource "azurerm_key_vault_secret" "jwt_issuer" {
  name         = "Jwt-Issuer"
  value        = "YourIssuer"
  key_vault_id = azurerm_key_vault.keyvault.id
}

resource "azurerm_key_vault_secret" "jwt_audience" {
  name         = "Jwt-Audience"
  value        = "YourAudience"
  key_vault_id = azurerm_key_vault.keyvault.id
}

resource "azurerm_key_vault_secret" "default_connection" {
  name         = "DefaultConnection"
  value        = "Server=localhost;Database=st_shadow;Trusted_Connection=True;;TrustServerCertificate=True"
  key_vault_id = azurerm_key_vault.keyvault.id
}

data "azurerm_client_config" "current" {}
