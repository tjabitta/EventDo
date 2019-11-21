using System;
using System.Linq;
using EvendoTest_v1.Search.Models.Csv;
using Nest;

namespace EvendoTest_v1.Search.Models.Elasticsearch
{
    public class ItemSearchDocument
    {
        public ItemSearchDocument()
        {
        }

        public ItemSearchDocument(ItemRecord record)
        {
            Id = record.GTIN;
            // we want to do some work in setting
            // up the values that will be analyzed
            // thinking about what the user might
            // type into our search input
            Names = new[]
                {
                    record.Name,
                    record.BrandName
                }
                .Union(record.Name.Split(' '))
                .Union(record.BrandName.Split(' '))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            Name = record.Name;
            BrandName = record.BrandName;
            
            Data = record;
        }
        
        public string Id { get; set; }
        
        // We want to index the many variations
        // of a name and brand, so we store the strings
        // in an array.
        //
        // We also want to index and search differently
        [Text(
            Analyzer = Indices.IndexAnalyzerName,
            SearchAnalyzer = Indices.SearchAnalyzerName
        )]
        public string[] Names { get; set; }
        
        // we want to filter by name
        [Keyword]
        public string BrandName { get; set; }
        
        [Keyword]
        public string Name { get; set; }
        
        [Object(Enabled = false)]
        public ItemRecord Data { get; set; }
        
    }
}