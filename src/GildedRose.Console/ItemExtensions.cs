using System;

namespace GildedRose.Console
{
    internal static class ItemExtensions
    {
        internal static int GetNewQuality(this Item item)
        {
            return item.Name switch
            {
                "Aged Brie" => GetNewAgedBrieQuality(item),
                "Backstage passes to a TAFKAL80ETC concert" => GetNewBackstagePassQuality(item),
                "Sulfuras, Hand of Ragnaros" => GetNewSulfurasQuality(item),
                "Conjured Mana Cake" => GetNewConjuredQuality(item),
                _ => GetNewNormalItemQuality(item),
            };
        }

        internal static int GetNewSellIn(this Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros" ? item.SellIn : item.SellIn - 1;
        }

        private static int GetNewAgedBrieQuality(Item item)
        {
            var qualityChange = item.IsExpired() ? 2 : 1;
            return item.GetChangedQuality(qualityChange);
        }

        private static int GetNewBackstagePassQuality(Item item)
        {
            if (item.IsExpired())
            {
                return 0;
            }

            var qualityChange = item.SellIn switch
            {
                < 6 => 3,
                < 11 => 2,
                _ => 1,
            };
            return item.GetChangedQuality(qualityChange);
        }

        private static int GetNewSulfurasQuality(Item item)
        {
            return item.Quality;
        }

        private static int GetNewConjuredQuality(Item item)
        {
            var qualityChange = item.IsExpired() ? -4 : -2;
            return item.GetChangedQuality(qualityChange);
        }

        private static int GetNewNormalItemQuality(Item item)
        {
            var qualityChange = item.IsExpired() ? -2 : -1;
            return item.GetChangedQuality(qualityChange);
        }

        private static bool IsExpired(this Item item)
        {
            return item.SellIn <= 0;
        }

        private static int GetChangedQuality(this Item item, int qualityChange)
        {
            var minimumQuality = qualityChange < 0 ? Math.Min(0, item.Quality) : int.MinValue;
            var maximumQuality = qualityChange > 0 ? Math.Max(50, item.Quality) : int.MaxValue;

            return Math.Clamp(item.Quality + qualityChange, minimumQuality, maximumQuality);
        }
    }
}