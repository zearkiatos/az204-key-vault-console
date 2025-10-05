using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Azure.Core;
using Configuration;

class Program
{
    private static readonly string keyVaultUrl = AppConfiguration.KeyVaultBaseUrl;
    private static readonly string secretName = AppConfiguration.SecretName;
    private static readonly string blobContainerName = AppConfiguration.BlobContainerName;
    private static readonly string blobName = AppConfiguration.BlobName;
    private static readonly string tenantId = AppConfiguration.TenantId;
    private static readonly string clientId = AppConfiguration.ClientId;
    private static readonly string clientSecret = AppConfiguration.ClientSecret;

    static async Task Main(string[] args)
    {
        Console.WriteLine($"Key Vault URL: {keyVaultUrl}, Secret Name: {secretName}, Blob Container: {blobContainerName}, Blob Name: {blobName}, Tenant ID: {tenantId}, Client ID: {clientId}, Client Secret: {clientSecret}");
        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        var client = new SecretClient(new Uri(keyVaultUrl), credential);
        KeyVaultSecret secret = await client.GetSecretAsync(secretName);
        string connectionString = secret.Value;

        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        var downloadInfo = await blobClient.DownloadAsync();
        using (var streamReader = new System.IO.StreamReader(downloadInfo.Value.Content))
        {
            string content = await streamReader.ReadToEndAsync();
            Console.WriteLine(content);
        }
    }
}
