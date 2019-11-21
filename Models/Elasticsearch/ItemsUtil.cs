using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using EvendoTest_v1.Search.Models.Csv;
using Nest;

namespace EvendoTest_v1.Search.Models.Elasticsearch
{
    public class ItemsUtil
    {
        public const string IndexName = "items";
        
        private ElasticClient client;
        
        public ItemsUtil(ElasticClient client)
        {
            this.client = client;
        }

        public async Task RunAsync()
        {
            // if the index exists, let's delete it
            // you probably don't want to do this kind of
            // index management in a production environment
            var index = await client.Indices.ExistsAsync(IndexName);
            
            if (index.Exists)
            {
                await client.Indices.DeleteAsync(IndexName);
            }
            
            // let's create the index
            var createResult = 
                await client.Indices.CreateAsync(IndexName, c => c
                    .Settings(s => s
                        .Analysis(a => a
                            // our custom search analyzer
                            .AddSearchAnalyzer()
                        )
                    )
                .Map<ItemSearchDocument>(m => m.AutoMap())
            );
            
            // let's load the data
            var file = File.Open("items.csv", FileMode.Open);
            using (var csv = new CsvReader(new StreamReader(file)))
            {
                // describe's the csv file
                csv.Configuration.RegisterClassMap<ItemsMapping>();

                var records = csv
                    .GetRecords<ItemRecord>()
                    .Select(record => new ItemSearchDocument(record))
                    .ToList();

                // we are pushing all the data in at once
                var bullkResult =
                    await client
                    .BulkAsync(b => b
                        .Index(IndexName)
                        .CreateMany(records)
                    );
            }
        }
    }
}