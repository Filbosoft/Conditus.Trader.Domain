using System;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;
using Conditus.DynamoDB.MappingExtensions.PropertyConverters;
using Conditus.Trader.Domain.Enums;
using Conditus.Trader.Domain.Entities.LocalSecondaryIndexes;
using Conditus.DynamoDB.MappingExtensions.Attributes;

namespace Conditus.Trader.Domain.Entities
{
    [DynamoDBTable("Orders")]
    public class OrderEntity
    {
        [DynamoDBLocalSecondaryIndexRangeKey(OrderLocalSecondaryIndexes.UserOrderIdIndex)]
        public string Id { get; set; }
        [DynamoDBLocalSecondaryIndexRangeKey(OrderLocalSecondaryIndexes.UserOrderPortfolioIndex)]
        [DynamoDBSelfContainingCompositeKey(nameof(CreatedAt))]
        [Required]
        public string PortfolioId { get; set; }
        [DynamoDBHashKey]
        [Required]
        public string OwnerId { get; set; }
        [DynamoDBLocalSecondaryIndexRangeKey(OrderLocalSecondaryIndexes.UserOrderTypeIndex)]
        [DynamoDBSelfContainingCompositeKey(nameof(CreatedAt))]
        [Required]
        public OrderType OrderType { get; set; } //Type is a keyword in dynamodb and can therefore not be used in expressions
        [DynamoDBLocalSecondaryIndexRangeKey(OrderLocalSecondaryIndexes.UserOrderAssetIndex)]
        [DynamoDBSelfContainingCompositeKey(nameof(CreatedAt))]
        [Required]
        public string AssetSymbol { get; set; }
        [DynamoDBProperty]
        public AssetType AssetType { get; set; }
        [DynamoDBProperty]
        [Required]
        public string AssetName { get; set; }
        [DynamoDBProperty]
        [Required]
        public int Quantity { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey(OrderGlobalSecondaryIndexes.OrderStatusIndex)]
        public OrderStatus OrderStatus { get; set; } //Status is a keyword in dynamodb and can therefore not be used in expressions
        [DynamoDBLocalSecondaryIndexRangeKey(OrderLocalSecondaryIndexes.UserOrderStatusIndex)]
        public string OrderStatusCreateAtCompositeKey { get; set; }
        [DynamoDBProperty]
        [Required]
        public decimal Price { get; set; }
        [DynamoDBProperty]
        [Required]
        public string CurrencyCode { get; set; }
        [DynamoDBRangeKey(typeof(DateTimePropertyConverter))]
        [DynamoDBGlobalSecondaryIndexRangeKey(OrderGlobalSecondaryIndexes.OrderStatusIndex)]
        [Required]
        public DateTime CreatedAt { get; set; }
        [DynamoDBProperty(typeof(DateTimePropertyConverter))]
        public DateTime? CompletedAt { get; set; }
        [DynamoDBProperty(typeof(DateTimePropertyConverter))]
        [Required]
        public DateTime ExpiresAt { get; set; }
    }
}