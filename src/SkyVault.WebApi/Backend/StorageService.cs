using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApi.Backend
{
    public class StorageService
    {
        private readonly string? _connectionString;
        private readonly string? _containerName;

        public StorageService()
        {
            _connectionString = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION_STRING");
            _containerName = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONTAINER_NAME");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Storage account connection string is not set in the environment variables.");
            }

            if (string.IsNullOrEmpty(_containerName))
            {
                throw new InvalidOperationException("Storage account container name is not set in the environment variables.");
            }
        }

        public async Task<PreSignedUrlResponse> GetPreSignedUrlAsync(FileType fileFormat, MessageType messageType)
        {
            string uploadDirectory = messageType switch
            {
                MessageType.Birthday => "birthdaywishes",
                MessageType.Promotion => "promotions",
                _ => throw new ArgumentException("Invalid file type specified.")
            };

            string uniqueFileName = $"{Guid.NewGuid()}.{fileFormat}";
            string blobName = $"{uploadDirectory}/{uniqueFileName}";
            string presignedUrl = await GeneratePreSignedUrl(blobName);
            return new PreSignedUrlResponse(presignedUrl, uniqueFileName);
        }

        private async Task<string> GeneratePreSignedUrl(string blobName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync();

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            BlobSasBuilder sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerClient.Name,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Write | BlobSasPermissions.Create);

            Uri sasUri = blobClient.GenerateSasUri(sasBuilder);

            return sasUri.ToString();
        }

        public async Task DeleteFileAsync(string fileName, MessageType messageType)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            string uploadDirectory = messageType switch
            {
                MessageType.Birthday => "birthdaywishes",
                MessageType.Promotion => "promotions",
                _ => throw new ArgumentException("Invalid file type specified.")
            };

            string blobName = $"{uploadDirectory}/{fileName}";

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        public string GetFileUrl(string fileName, MessageType messageType)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            string uploadDirectory = messageType switch
            {
                MessageType.Birthday => "birthdaywishes",
                MessageType.Promotion => "promotions",
                _ => throw new ArgumentException("Invalid file type specified.")
            };

            string blobName = $"{uploadDirectory}/{fileName}";

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            Uri blobUri = blobClient.Uri;

            return blobUri.ToString();
        }

    }
}
