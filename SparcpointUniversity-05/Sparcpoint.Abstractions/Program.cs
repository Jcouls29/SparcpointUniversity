using System.Threading.Tasks;

namespace Sparcpoint.Abstractions
{
    public static class Program
    {
        public static async Task Main()
        {
            IDelimitedRecordImporter<Product> importer = null;

            var records = await importer.ImportCsvFromFile(@"C:\Temp\products.csv");
        }

        private class Product
        {
            public string Name { get; set; }
            public string Sku { get; set; }
        }
    }
}
