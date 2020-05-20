using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TblStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            //Reference to the ConnectionString in the App.Config file  

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("customers");
            table.CreateIfNotExists();

            CreateCustomer(table, new Customer("Malika", "malika@outlook.com", "registered"));
            RetrieveCustomer(table, "registered", "malika@outlook.com");

            Console.ReadKey();
        }
        static void CreateCustomer(CloudTable table, Customer user)
        {
            TableOperation insert = TableOperation.InsertOrReplace(user);
            table.Execute(insert);
            Console.WriteLine("Record inserted");
        }
        public static void RetrieveCustomer(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation tableOperation = TableOperation.Retrieve<Customer>(partitionKey, rowKey);
            TableResult tableResult = table.Execute(tableOperation);
            Console.WriteLine(((Customer)tableResult.Result).CustomerName);
        }
    }
}
