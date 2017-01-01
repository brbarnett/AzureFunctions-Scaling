using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace BB.ScalingAzureFunctions.Api.Models
{
    public class Person : TableEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}