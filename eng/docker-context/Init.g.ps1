# Auto-generated initialization script for container startup

# Configure git user identity from Machine environment variables
$gitUserName = [Environment]::GetEnvironmentVariable('GIT_USER_NAME', 'Machine')
$gitUserEmail = [Environment]::GetEnvironmentVariable('GIT_USER_EMAIL', 'Machine')
if ($gitUserName) {
    git config --global user.name $gitUserName
}
if ($gitUserEmail) {
    git config --global user.email $gitUserEmail
}

# Disable autocrlf to prevent EOL conversion issues with Claude Code's Edit tool
git config --global core.autocrlf false

# Configure git safe.directory for all mounted directories
$gitDirectories = @(
    'C:\src\PostSharp-2026.0\PostSharp',
    'C:\src\PostSharp-2026.0\PostSharp.Documentation',
    'C:\src\PostSharp.Engineering'
)

foreach ($dir in $gitDirectories) {
    if ($dir) {
        $normalizedDir = ($dir -replace '\\\\', '/').TrimEnd('/') + '/'
        git config --global --add safe.directory $normalizedDir
    }
}

