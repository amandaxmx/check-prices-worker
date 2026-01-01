using CheckPrices.Domain.Contracts;
using Microsoft.Playwright;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CheckPrices.Infra.Services
{
    public class PriceCheckerService : IPriceCheckerService
    {
        public async Task<decimal?> GetPriceFromUrlAsync(string url)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();

            try
            {
                var page = await browser.NewPageAsync();
                
                await page.GotoAsync(url);

                var priceSelectors = new[] {
                    ".a-price .a-offscreen",
                    "#priceblock_ourprice",  
                    ".price",                
                    "#price",
                    "[class*='price']",
                    ".product-price",
                    "a-price-whole"
                };

                string? priceText = null;

                foreach (var selector in priceSelectors)
                {
                    var locator = page.Locator(selector).First;

                    if (await locator.IsVisibleAsync())
                    {
                        priceText = await locator.TextContentAsync();

                        if (!string.IsNullOrWhiteSpace(priceText))
                        {
                            Console.WriteLine($"Preço encontrado com o seletor: {selector}");
                            break; 
                        }
                    }
                }

                if (priceText == null)
                {
                    Console.WriteLine("Nenhum seletor de preço funcionou.");
                }
                else
                {
                    priceText = priceText.Replace("R$", "");
                }

                await page.CloseAsync();

                if (!string.IsNullOrWhiteSpace(priceText))
                {
                    return ParsePrice(priceText);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        private decimal ParsePrice(string priceText)
        {
            var cleanedPriceText = Regex.Replace(priceText, @"[^\d,\.]", "");

            if (cleanedPriceText.LastIndexOf(',') > cleanedPriceText.LastIndexOf('.'))
            {
                cleanedPriceText = cleanedPriceText.Replace(".", "").Replace(',', '.');
            }
            else
            {
                cleanedPriceText = cleanedPriceText.Replace(",", "");
            }

            if (decimal.TryParse(cleanedPriceText, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
            {
                return price;
            }

            return 0;
        }
    }
}
