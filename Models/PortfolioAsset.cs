using Amazon.DynamoDBv2.DataModel;

namespace Conditus.Trader.Domain.Models
{
    public class PortfolioAsset
    {
        [DynamoDBHashKey]
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}