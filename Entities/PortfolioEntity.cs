using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;
using Conditus.DynamoDBMapper.Attributes;
using Conditus.DynamoDBMapper.PropertyConverters;
using Conditus.Trader.Domain.Models;

namespace Conditus.Trader.Domain.Entities
{
    [DynamoDBTable("Portfolios")]
    public class PortfolioEntity
    {
        [Required]
        [DynamoDBProperty]
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
        [DynamoDBProperty(typeof(ListMapPropertyConverter<PortfolioAsset>))]
        [MapList]
        public List<PortfolioAsset> Assets { get; set; } = new List<PortfolioAsset>();
        [Required]
        [DynamoDBRangeKey(typeof(DateTimePropertyConverter))]
        public DateTime CreatedAt { get; set; }
    }
}