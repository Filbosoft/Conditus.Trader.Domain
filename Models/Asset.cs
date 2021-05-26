using Conditus.Trader.Domain.Enums;

namespace Conditus.Trader.Domain.Models
{
    public class AssetDetail
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public AssetType Type { get; set; }
        public string Exchange { get; set; }
        public Currency Currency { get; set; }
    }

    public class AssetOverview
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public AssetType Type { get; set; }
    }
}