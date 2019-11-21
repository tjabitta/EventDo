using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EvendoTest_v1.Search.Models.Csv;
using EvendoTest_v1.Search.Models.Elasticsearch;
using Nest;

namespace EvendoTest_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
            private readonly ElasticClient client;
        
        public ISearchResponse<ItemSearchDocument> Search { get; set; }

        public ItemsController(ElasticClient client)
        {
            this.client = client;
        }


        // GET: api/Items
        [HttpGet("{term}")]
        public IEnumerable<ItemSearchDocument> Get(string term)
        {

            if (!string.IsNullOrWhiteSpace(term))
            {
                Search =
                    client.Search<ItemSearchDocument>(s =>
                        s.Query(q => q
                                .Match(m => m
                                    .Field(f => f.Name)
                                    .Query(term)
                                    .Fuzziness(Fuzziness.EditDistance(1))
                                )
                            )
                            .Take(10)
                    );
            }
            return client.Search<ItemSearchDocument>().Documents.ToList();
            //return client.Search<ItemSearchDocument>().Documents.ToList();
            //return client.Search<ItemSearchDocument>(s => s.Take(10)).Documents.ToList();
            //return Search.Documents.ToList();
        }

    }
}
