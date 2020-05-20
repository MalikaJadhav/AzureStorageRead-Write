using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOBStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images1");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            //To download image from BLOB to system
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("Img3.jpg");

            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\bpj\Downloads\Img3.jpg"))
            {
                blockBlob2.DownloadToStream(fileStream);
            }

            ///To upload image from system to BLOB
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("Butterfly.jpg");

            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\bpj\Downloads\Butterfly.jpg"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            Console.ReadKey();
        }
    }
}
