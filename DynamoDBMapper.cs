using System;
using System.Collections;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace Conditus.Trader.Domain
{
    public static class DynamoDBMapper
    {
        public static Dictionary<string, AttributeValue> GetAttributeMap(object entity)
        {
            var map = new Dictionary<string, AttributeValue>();

            foreach (var property in entity.GetType().GetProperties())
            {
                var propertyType = property.PropertyType;
                var propertyValue = property.GetValue(entity);
                if (propertyValue == null) continue;

                AttributeValue mapValue = GetMapValue(propertyType, propertyValue);

                map.Add(property.Name, mapValue);
            }

            return map;
        }

        private static AttributeValue GetMapValue(Type propertyType, object propertyValue)
        {
            if (propertyType == typeof(decimal) || propertyType == typeof(int) || propertyType == typeof(long))
                return new AttributeValue { N = propertyValue.ToString() };

            if (propertyType.IsEnum)
                return new AttributeValue { N = ((int)propertyValue).ToString() };

            if (propertyType == typeof(string))
                return new AttributeValue { S = (string)propertyValue };

            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                return new AttributeValue { S = propertyValue.ToString() };

            return new AttributeValue { M = GetAttributeMap(propertyValue) };
        }

        public static T MapAttributeMapToEntity<T>(Dictionary<string, AttributeValue> attributeMap)
            where T : new()
        {
            var entity = new T();

            foreach (var property in entity.GetType().GetProperties())
            {
                var entityProperty = entity.GetType().GetProperty(property.Name);
                if (entityProperty == null || !entityProperty.CanWrite) continue;

                var attributeValue = attributeMap.GetAttributeValue(property.Name);
                if (attributeValue == null) continue;

                object propertyValue = GetPropertyValue(property.PropertyType, attributeValue);

                if(propertyValue != null) entityProperty.SetValue(entity, propertyValue);
            }

            return entity;
        }

        private static object GetPropertyValue(Type propertyType, AttributeValue attributeValue)
        {
            if (propertyType == typeof(decimal))
                return Convert.ToDecimal(attributeValue.N);

            if (propertyType == typeof(int))
                return Convert.ToInt32(attributeValue.N);

            if (propertyType.IsEnum)
                return Enum.ToObject(propertyType, int.Parse(attributeValue.N));

            if (propertyType == typeof(string))
                return attributeValue.S;
            
            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                return GetDateTimeFromUnixTimeMS(attributeValue.N);
            
            if (propertyType == typeof(object))
                return Convert.ChangeType(MapAttributeMapToEntity<object>(attributeValue.M), propertyType);

            return null;
        }

        public static DateTime GetDateTimeFromUnixTimeMS(string unixTime)
        {
            try
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTime));
                return dateTimeOffset.UtcDateTime;
            }
            catch (ArgumentOutOfRangeException)
            {
                return new DateTime();
            }
        }

        public static AttributeValue GetAttributeValue(this DateTime dateTime)
        {
            var unixTime = GetUnixTimeMSFromDateTime(dateTime);
            var value = new AttributeValue { N = unixTime.ToString() };

            return value;
        }

        public static long GetUnixTimeMSFromDateTime(DateTime dateTime)
        {
            var offset = new DateTimeOffset(dateTime, new TimeSpan());
            var unixTime = offset.ToUnixTimeMilliseconds();

            return unixTime;
        }

        public static AttributeValue GetAttributeValue(this Enum enumValue)
        {
            var numericValue = Convert.ToUInt32(enumValue);
            var value = new AttributeValue { N = numericValue.ToString() };

            return value;
        }

        public static AttributeValue GetAttributeValue(this Dictionary<string, AttributeValue> attributeMap, string key)
        {
            AttributeValue attributeValue;
            attributeMap.TryGetValue(key, out attributeValue);

            return attributeValue;
        }
    }
}