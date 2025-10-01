using DotNetEnv;

namespace Configuration
{
    public static class AppConfiguration
    {
        static AppConfiguration()
        {
            Env.Load();
        }

        public static string KeyVaultBaseUrl => 
            Environment.GetEnvironmentVariable("KEY_VAULT_BASE_URL") 
            ?? throw new InvalidOperationException("KEY_VAULT_BASE_URL environment variable is not set");

        public static string SecretName => 
            Environment.GetEnvironmentVariable("SECRET_NAME") 
            ?? throw new InvalidOperationException("SECRET_NAME environment variable is not set");

        public static string BlobContainerName => 
            Environment.GetEnvironmentVariable("BLOB_CONTAINER_NAME") 
            ?? throw new InvalidOperationException("BLOB_CONTAINER_NAME environment variable is not set");

        public static string BlobName => 
            Environment.GetEnvironmentVariable("BLOB_NAME") 
            ?? throw new InvalidOperationException("BLOB_NAME environment variable is not set");

        public static string TenantId => 
            Environment.GetEnvironmentVariable("TENANT_ID") 
            ?? throw new InvalidOperationException("TENANT_ID environment variable is not set");

        public static string ClientId => 
            Environment.GetEnvironmentVariable("CLIENT_ID") 
            ?? throw new InvalidOperationException("CLIENT_ID environment variable is not set");

        public static string ClientSecret => 
            Environment.GetEnvironmentVariable("CLIENT_SECRET") 
            ?? throw new InvalidOperationException("CLIENT_SECRET environment variable is not set");

        public static void ValidateConfiguration()
        {
            try
            {
                _ = KeyVaultBaseUrl;
                _ = SecretName;
                _ = BlobContainerName;
                _ = BlobName;
                _ = TenantId;
                _ = ClientId;
                _ = ClientSecret;
                Console.WriteLine("✓ Configuration validation passed");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Configuration validation failed: {ex.Message}");
                throw;
            }
        }
    }
}