# 1. Change the execution policy to RemoteSigned (or adjust as necessary)
Write-Host "Setting the execution policy to RemoteSigned..."
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser -Force

# 2. Install the Az module if it's not already installed
if (-not (Get-Module -ListAvailable -Name Az)) {
    Write-Host "Installing Azure PowerShell Az module..."
    Install-Module -Name Az -AllowClobber -Force
} else {
    Write-Host "Az module is already installed."
}

# 3. Import the Az module to ensure it's available in the session
Write-Host "Importing the Az module..."
Import-Module Az

# 4. Log in to Azure using Connect-AzAccount if not already logged in
try {
    $azureContext = Get-AzContext
    if ($null -eq $azureContext) {
        Write-Host "No Azure context found, running Connect-AzAccount..."
        Connect-AzAccount
    } else {
        Write-Host "Already logged in to Azure."
    }
}
catch {
    Write-Host "Error detecting Azure context. Running Connect-AzAccount..."
    Connect-AzAccount
}

# 5. Set the Azure subscription context (modify SubscriptionId if necessary)
$subscription = Get-AzSubscription
if ($subscription.Count -eq 1) {
    Write-Host "Setting the subscription context to the only available subscription."
    Set-AzContext -SubscriptionId $subscription.Id
} elseif ($subscription.Count -gt 1) {
    Write-Host "Multiple subscriptions found. Available subscriptions:"
    
    # Print available subscriptions with name and ID
    $subscription | ForEach-Object {
        Write-Host "Subscription Name: $($_.Name), Subscription ID: $($_.Id)"
    }

    # Prompt user to select a Subscription ID
    $selectedSubscriptionId = Read-Host "Enter the Subscription ID you want to use"
    
    # Set the selected subscription context
    Set-AzContext -SubscriptionId $selectedSubscriptionId
} else {
    Write-Host "No subscriptions found. Ensure you have access to Azure subscriptions."
}

Write-Host "Azure environment setup completed."
