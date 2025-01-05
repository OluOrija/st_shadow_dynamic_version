# Create KeyVault and update Access policy through ClickOps

# Set variables for Key Vault details
$keyVaultName = "stshadowKeyVault"  # Replace with your Key Vault name

# Define the secrets to be added to the Key Vault
$secrets = @{
    "tfbackend-sa-access-key" = "" 
}

# Loop through each secret and set it in the Key Vault
foreach ($secretName in $secrets.Keys) {
    $secretValue = $secrets[$secretName]
    $command = "az keyvault secret set --vault-name $keyVaultName --name $secretName --value $secretValue"
    
    try {
        $result = Invoke-Expression $command
        Write-Host "Secret successfully set in Key Vault '$keyVaultName': $secretName"
    } catch {
        Write-Host "Failed to set secret in Key Vault. Error: $_"
    }
}
