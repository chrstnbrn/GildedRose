using FluentAssertions;
using GildedRose.Console;
using System.Collections.Generic;
using Xunit;

namespace GildedRose.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void EmptyItems()
        {
            var program = new Program { Items = new List<Item>() };

            program.UpdateInventory();

            program.Items.Should().BeEmpty();
        }

        [Theory]
        [InlineData("Apple", 10, 8, 9, 7)]
        [InlineData("Apple", 0, 5, 0, 4)]
        [InlineData("Apple", 5, 0, 3, -1)]
        [InlineData("Apple", 5, -1, 3, -2)]
        [InlineData("Apple", 1, 0, 0, -1)]
        [InlineData("Apple", 1, -1, 0, -2)]
        [InlineData("Apple", -1, -1, -1, -2)]
        [InlineData("Apple", -2, -1, -2, -2)]
        [InlineData("Apple", 52, 5, 51, 4)]
        [InlineData("Conjured Mana Cake", 10, 5, 8, 4)]
        [InlineData("Conjured Mana Cake", 0, 5, 0, 4)]
        [InlineData("Conjured Mana Cake", 5, 0, 1, -1)]
        [InlineData("Conjured Mana Cake", 3, 0, 0, -1)]
        [InlineData("Conjured Mana Cake", 1, -1, 0, -2)]
        [InlineData("Conjured Mana Cake", -1, -1, -1, -2)]
        [InlineData("Conjured Mana Cake", -2, -1, -2, -2)]
        [InlineData("Conjured Mana Cake", 53, 5, 51, 4)]
        [InlineData("Aged Brie", 10, 8, 11, 7)]
        [InlineData("Aged Brie", 50, 8, 50, 7)]
        [InlineData("Aged Brie", 10, 0, 12, -1)]
        [InlineData("Aged Brie", 10, -1, 12, -2)]
        [InlineData("Aged Brie", -5, 5, -4, 4)]
        [InlineData("Aged Brie", -5, 0, -3, -1)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 11, 6, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 50, 11, 50, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 10, 7, 9)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 49, 10, 50, 9)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 50, 10, 50, 9)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 51, 10, 51, 9)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 52, 10, 52, 9)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 6, 7, 5)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 5, 8, 4)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 1, 8, 0)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 0, 0, -1)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, -1, 0, -2)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", -1, 5, 2, 4)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", -4, 5, -1, 4)]
        [InlineData("Sulfuras, Hand of Ragnaros", 10, 8, 10, 8)]
        [InlineData("Sulfuras, Hand of Ragnaros", 51, 8, 51, 8)]
        [InlineData("Sulfuras, Hand of Ragnaros", -5, -10, -5, -10)]
        public void OneItem(string name, int quality, int sellIn, int expectedQuality, int expectedSellIn)
        {
            var program = new Program { 
                Items = new List<Item> { 
                    new() { Name = name, Quality = quality, SellIn = sellIn }
                } 
            };

            program.UpdateInventory();

            var expected = new List<Item> { 
                new() { Name = name, Quality = expectedQuality, SellIn = expectedSellIn }
            };
            program.Items.Should().BeEquivalentTo(expected);
        }
    }
}