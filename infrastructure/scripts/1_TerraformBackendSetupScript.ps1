# Notes
# Remember to run az login
# and set Subscription to the correct subscription you want to work with.
# Before calling/running this script.
# Might need to run if Connect-AzAccount is stuck on a old context. Update-AzConfig -EnableLoginByWam $false


# Variables
$tenantId = "cafdd3e2-65b1-4dd0-a086-42ae3c061563" #OlusegunOrija
$subscriptionId = "aba7de2d-a8b2-41b7-b387-40f891d5e3b7" # Jarvis
$resourceGroupName = "stshadow-core-rg"  # Set your resource group name
$location = "UK South"                    # Set your Azure region
$storageAccountName = "tfbackendsa$(Get-Random -Minimum 1000 -Maximum 9999)"  # Set your storage account name (must be unique)
$containerName = "tfbackend-states"          # Set your container name

# Ensure the user is authenticated
if (-not (Get-AzContext)) {
    Connect-AzAccount -TenantId $tenantId
}

# Set the Azure subscription context
Set-AzContext -SubscriptionId $subscriptionId -TenantId $tenantId

# Create Resource Group
$resourceGroup = New-AzResourceGroup -Name $resourceGroupName -Location $location
Write-Output "Resource Group '$resourceGroupName' created successfully in '$location'."

# Create Storage Account
$storageAccount = New-AzStorageAccount -ResourceGroupName $resourceGroupName -Name $storageAccountName -Location $location -SkuName Standard_LRS -Kind StorageV2
Write-Output "Storage Account '$storageAccountName' created successfully in '$location'."

# Get the Storage Account Key
$storageAccountKey = (Get-AzStorageAccountKey -ResourceGroupName $resourceGroupName -Name $storageAccountName)[0].Value

# Create Blob Container
$context = $storageAccount.Context
$container = New-AzStorageContainer -Name $containerName -Context $context
Write-Output "Container '$containerName' created successfully in Storage Account '$storageAccountName'."

# Output the Storage Account Name, Container Name, and Storage Account Key
Write-Output "`nStorage Account Name: $storageAccountName"
Write-Output "Container Name: $containerName"
Write-Output "Storage Account Access Key: $storageAccountKey"

# Optionally, you can return the outputs as an object for further processing
$results = [PSCustomObject]@{
    StorageAccountName = $storageAccountName
    ContainerName = $containerName
    StorageAccountAccessKey = $storageAccountKey
}

return $results
