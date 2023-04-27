using TeamKville.Shared.Dto;

namespace TeamKville.Shared
{
    public class SharedClass
    {

        public static List<ProductDto> allProducts = new List<ProductDto>();

        public static UserDto activeUser;

        public static string apiKey = new Guid().ToString();

        public static string connectionStringBlob;

        public static string GetImage(string type, string name)
        {
            //Ändra till environment variable
            return ($"https://teamkvillestorage.blob.core.windows.net/{type}/{name}.png");
        }

        public static string CapitalizeFirstLetter(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
       
    }
}
