# Ensure you are logged in to Azure
Write-Host "Checking Azure authentication..."
$context = Get-AzContext
if (-not $context) {
    Write-Host "Not logged in. Running Connect-AzAccount..."
    Connect-AzAccount
} else {
    Write-Host "Already authenticated to Azure."
}

# Get the current subscription
$subscriptionId = $context.Subscription.Id
$tenantId = $context.Tenant.Id

# Create a new service principal with a Contributor role (skip role assignment if permissions are insufficient)
Write-Host "Creating service principal..."
$sp = New-AzADServicePrincipal -DisplayName "TerraformServicePrincipal"

# Show the raw output of New-AzADServicePrincipal immediately after execution
Write-Host "`nAzure CLI Output:"
Write-Output $sp

# Capture the client secret (password) from the passwordCredentials array
$clientSecret = $sp.passwordCredentials[0].secretText

# Introduce a delay to ensure the ApplicationId is available
Start-Sleep -Seconds 10

# Attempt to fetch the ApplicationId in case of delay in service principal creation
$applicationId = $sp.ApplicationId

if (-not $applicationId) {
    Write-Host "ApplicationId not found initially, retrying to fetch..."
    $sp = Get-AzADServicePrincipal -ObjectId $sp.Id
    $applicationId = $sp.ApplicationId
}

# Attempt to assign Contributor role (this may fail if you don't have permission)
try {
    Write-Host "Attempting to assign Contributor role..."
    New-AzRoleAssignment -ObjectId $sp.Id -RoleDefinitionName Contributor -Scope "/subscriptions/$subscriptionId"
    Write-Host "Contributor role assigned successfully."
} catch {
    Write-Host "Failed to assign Contributor role. You may not have sufficient permissions."
}

# Notify the user that the client_id is the same as the ApplicationId of the service principal
Write-Host "`nNote: The Client_Id is the same as the ApplicationId of the service principal."

# Custom object to return the key details
$customOutput = [PSCustomObject]@{
    Client_Id        = $applicationId
    Client_Secret    = $clientSecret
    Tenant_Id        = $tenantId
    Subscription_Id  = $subscriptionId
}

# Output the full service principal object
Write-Host "Service principal created successfully."

# Output the custom object
Write-Host "Returning custom object with essential parameters:"
Write-Output $customOutput
