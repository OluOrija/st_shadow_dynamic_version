# Ensure Azure environment variables are set
# The Set-AzureEnvVariables.ps1 script is never committed to the repo so create and populate if needed.
# Set-AzureEnvVariables.ps1 TEMPLATE:
# $env:ARM_SUBSCRIPTION_ID = "" 
# $env:ARM_CLIENT_ID = "" 
# $env:ARM_CLIENT_SECRET = "" 
# $env:ARM_TENANT_ID = "" 
$scriptDirectory = $PSScriptRoot
$envVarsScriptPath = Join-Path -Path $scriptDirectory -ChildPath "Set-AzureEnvVariables.ps1"
& $envVarsScriptPath
Write-Host "Ensuring Azure environment variables are set.. "

# Define the directory containing the Terraform files
$terraformDir = Get-Location

# Navigate to the Terraform directory
Set-Location -Path $terraformDir
Write-Host "Changed directory to: $terraformDir"

# Fetch the access key from Azure Key Vault
$keyVaultName = "stshadowKeyVault"  # Replace with your Key Vault name
$secretName = "tfbackend-sa-access-key"  # Replace with your secret name
try {
    $accessKey = az keyvault secret show --vault-name $keyVaultName --name $secretName --query value -o tsv
    if (-not $accessKey) {
        throw "Access key could not be retrieved from Key Vault."
    }
    Write-Host "Access key successfully fetched from Azure Key Vault."
} catch {
    Write-Host "Failed to fetch access key. Error: $_"
    exit 1
}

# Set the access key as a Terraform variable (environment variable)
$env:TF_VAR_storage_access_key = $accessKey

# Initialize Terraform
Write-Host "Initializing Terraform..."
try {
    #terraform init -reconfigure
    terraform init
    Write-Host "Terraform successfully initialized."
} catch {
    Write-Host "Terraform initialization failed. Error: $_"
    exit 1
}

# Apply Terraform configuration
Write-Host "Applying Terraform configuration..."
try {
    #terraform apply -auto-approve
    terraform apply
    Write-Host "Terraform configuration successfully applied."
} catch {
    Write-Host "Terraform apply failed. Error: $_"
    exit 1
}