using System;
using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

            };

            app.UpdateInventory();

            System.Console.ReadKey();

        }

        public void UpdateInventory()
        {
            foreach (var item in Items)
            {
                item.Quality = GetNewQuality(item);
                item.SellIn = GetNewSellIn(item);
            }
        }

        private static int GetNewQuality(Item item)
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

        private static int GetNewSellIn(Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros" ? item.SellIn : item.SellIn - 1;
        }

        private static int GetNewAgedBrieQuality(Item item)
        {
            var qualityChange = item.SellIn > 0 ? 1 : 2;
            return GetChangedQuality(item, qualityChange);
        }

        private static int GetNewBackstagePassQuality(Item item)
        {
            if (item.SellIn <= 0)
            {
                return 0;
            }

            var qualityChange = item.SellIn switch
            {
                < 6 => 3,
                < 11 => 2,
                _ => 1,
            };
            return GetChangedQuality(item, qualityChange);
        }

        private static int GetNewSulfurasQuality(Item item)
        {
            return item.Quality;
        }

        private static int GetNewConjuredQuality(Item item)
        {
            var qualityChange = item.SellIn > 0 ? -2 : -4;
            return GetChangedQuality(item, qualityChange);
        }

        private static int GetNewNormalItemQuality(Item item)
        {
            var qualityChange = item.SellIn > 0 ? -1 : -2;
            return GetChangedQuality(item, qualityChange);
        }

        private static int GetChangedQuality(Item item, int qualityChange)
        {
            var minimumQuality = qualityChange < 0 ? Math.Min(0, item.Quality) : int.MinValue;
            var maximumQuality = qualityChange > 0 ? Math.Max(50, item.Quality) : int.MaxValue;

            return Math.Clamp(item.Quality + qualityChange, minimumQuality, maximumQuality);
        }
    }
}
