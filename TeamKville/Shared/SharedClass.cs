using TeamKville.Shared.Dto;

namespace TeamKville.Shared
{
    public class SharedClass
    {
        public static string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut ornare eros congue, vehicula ante blandit, maximus felis. Nulla lacus odio, eleifend at magna id, dictum finibus libero. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. In elit odio, lacinia id turpis et, mattis pellentesque urna. \r\n\r\nMorbi nec augue ut metus commodo vulputate vel ac sapien. Nullam nibh nibh, lobortis eget convallis vel, cursus eu urna. Nam non semper leo.Fusce odio urna, euismod eu ex ut, elementum lobortis odio. Fusce imperdiet, mi ac fermentum elementum, turpis quam porta neque, sit amet molestie enim urna ultrices sem. \r\n\r\nAliquam id mattis est. Aenean mi risus, semper nec enim quis, condimentum tempus dui. Ut diam lacus, dignissim ac varius commodo, lacinia id magna. Vivamus quis velit et ipsum volutpat congue id sed sem. Duis placerat ultricies augue non mattis.\r\n\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Ut ornare eros congue, vehicula ante blandit, maximus felis. Nulla lacus odio, eleifend at magna id, dictum finibus libero. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. In elit odio, lacinia id turpis et, mattis pellentesque urna. ";

        public static List<ProductDto> allProducts = new List<ProductDto>();
        public static string GetImage(string type, int id)
        {
            //Ändra till environment variable
            return ($"https://teamkvillestorage.blob.core.windows.net/{type}/{id}.png");
        }

        public static string CapitalizeFirstLetter(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static void CreateFakeProductList()
        {
            allProducts.Clear();
            var rand = new Random();

            string[] tempGenres = { "Action", "Äventyr", "RPG", "Racing" };
            int[] tempAges = { 3, 7, 12, 16, 18 };

            for (int i = 0; i < 25; i++)
            {
                allProducts.Add(new ProductDto { Id = i, Name = "Spel" + i, Category = "game", Description = loremIpsum, Genre = tempGenres[rand.Next(0, 3)], Age = tempAges[rand.Next(0, 5)], Price = 599, Rating = rand.Next(1, 5) });
            }

            for (int i = 25; i < 50; i++)
            {
                allProducts.Add(new ProductDto { Id = i, Name = "Kontroller" + i, Description = loremIpsum, Category = "peripherals", Price = 799, Rating = rand.Next(1, 5) });
            }
        }
    }
}
