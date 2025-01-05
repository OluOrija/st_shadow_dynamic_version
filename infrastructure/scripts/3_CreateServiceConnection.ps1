# Parameters - Update these as necessary
$organizationUrl = ""  # Azure DevOps Organization URL
$projectName = ""  # Azure DevOps Project Name
$serviceConnectionName = ""  # Desired service connection name
$subscriptionId = "" # CloudAttribution Dev/Test # Azure Subscription ID
$subscriptionName = ""  # Azure Subscription Name
$tenantId = ""  # Azure AD Tenant ID
$azureSPAppId = ""  # Service Principal App ID
$azureSPPassword = ""  # Service Principal Password

# Scope level: "Subscription" or "ResourceGroup"
$scopeLevel = "Subscription"  # Can be set to "Subscription" or "ResourceGroup"

# If scopeLevel is ResourceGroup, set resourceGroupName
$resourceGroupName = "YOUR_RESOURCE_GROUP_NAME"  # Azure Resource Group, required if scope is "ResourceGroup"

# Step 1: Log in to Azure
az login

# Step 2: Log in to Azure DevOps
az devops login --organization $organizationUrl

# Step 3: Prepare the body for the service connection request
if ($scopeLevel -eq "Subscription") {
    $body = @{
        "data" = @{
            "subscriptionId" = $subscriptionId
            "subscriptionName" = $subscriptionName
            "environment" = "AzureCloud"
            "creationMode" = "Manual"
            "scopeLevel" = "Subscription"
            "spnObjectId" = $azureSPAppId
            "tenantId" = $tenantId
        }
        "authorization" = @{
            "parameters" = @{
                "serviceprincipalid" = $azureSPAppId
                "serviceprincipalkey" = $azureSPPassword
                "tenantid" = $tenantId
            }
            "scheme" = "ServicePrincipal"
        }
        "name" = $serviceConnectionName
        "type" = "azurerm"
        "url" = "https://management.azure.com/"
    }
} elseif ($scopeLevel -eq "ResourceGroup") {
    $body = @{
        "data" = @{
            "subscriptionId" = $subscriptionId
            "subscriptionName" = $subscriptionName
            "resourceGroup" = $resourceGroupName
            "environment" = "AzureCloud"
            "creationMode" = "Manual"
            "scopeLevel" = "ResourceGroup"
            "spnObjectId" = $azureSPAppId
            "tenantId" = $tenantId
        }
        "authorization" = @{
            "parameters" = @{
                "serviceprincipalid" = $azureSPAppId
                "serviceprincipalkey" = $azureSPPassword
                "tenantid" = $tenantId
            }
            "scheme" = "ServicePrincipal"
        }
        "name" = $serviceConnectionName
        "type" = "azurerm"
        "url" = "https://management.azure.com/"
    }
} else {
    Write-Host "Invalid scope level. Please set it to either 'Subscription' or 'ResourceGroup'."
    exit
}

# Step 4: Send the request to create the service connection
$uri = "$organizationUrl/$projectName/_apis/serviceendpoint/endpoints?api-version=6.0-preview.4"
$response = Invoke-RestMethod -Uri $uri -Method Post -ContentType "application/json" -Body ($body | ConvertTo-Json -Depth 99) -UseBasicParsing
$response

Write-Host "Service connection '$serviceConnectionName' created successfully in project '$projectName' scoped to $scopeLevel."
