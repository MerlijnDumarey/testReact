using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFit.MongoDb.Api.Models
{
    public class BeFitDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CategoriesCollectionName { get; set; } = null!;
        public string StudentsCollectionName { get; set; } = null!;
        public string TestsCollectionName { get; set; } = null!;
        public string LectorsCollectionName { get; set; } = null!;
        public string AttemptsCollectionName { get; set; } = null!;
    }
}
