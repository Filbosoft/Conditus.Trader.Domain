using System;
using Amazon.DynamoDBv2.DataModel;
using Conditus.DynamoDB.MappingExtensions.PropertyConverters;

namespace Conditus.Trader.Domain.Entities
{
    [DynamoDBTable("PortfolioGrowthPoints")]
    public class PortfolioGrowthPoint
    {
        [DynamoDBHashKey]
        public string PortfolioId { get; set; }
        [DynamoDBProperty]
        public decimal CurrentGrowth { get; set; }
        [DynamoDBProperty]
        public decimal CurrentMarketValue { get; set; }
        [DynamoDBProperty]
        public string OwnerId { get; set; }
        [DynamoDBRangeKey(typeof(DateTimePropertyConverter))]
        public DateTime GrowthPointTimestamp { get; set; }
    }
}