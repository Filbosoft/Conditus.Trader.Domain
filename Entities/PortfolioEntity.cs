using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;
using Conditus.DynamoDB.MappingExtensions.Attributes;
using Conditus.DynamoDB.MappingExtensions.PropertyConverters;
using Conditus.Trader.Domain.Entities.Indexes;
using Conditus.Trader.Domain.Models;

namespace Conditus.Trader.Domain.Entities
{
    [DynamoDBTable("Portfolios")]
    public class PortfolioEntity
    {
        [Required]
        [DynamoDBLocalSecondaryIndexRangeKey(PortfolioLocalSecondaryIndexes.PortfolioIdIndex)]
        public string Id { get; set; }
        [Required]
        [DynamoDBProperty]
        public string PortfolioName { get; set; }
        [Required]
        [DynamoDBHashKey]
        public string OwnerId { get; set; }
        [Required]
        [DynamoDBProperty]
        public decimal Capital { get; set; }
        [DynamoDBProperty]
        [Required]
        public string CurrencyCode { get; set; }
        [DynamoDBProperty(typeof(DynamoDBListMapPropertyConverter<PortfolioAsset>))]
        [DynamoDBMapList]
        public List<PortfolioAsset> Assets { get; set; } = new List<PortfolioAsset>();
        [Required]
        [DynamoDBRangeKey(typeof(DateTimePropertyConverter))]
        public DateTime CreatedAt { get; set; }
    }
}