using TeamKville.Shared.Dto;

namespace TeamKville.Shared
{
    public class SharedClass
    {

        public static List<ProductDto> allProducts = new List<ProductDto>();

        public static UserDto activeUser;

        public static string apiKey = new Guid().ToString();

        public static string connectionStringBlob = "DefaultEndpointsProtocol=https;AccountName=teamkvillestorage;AccountKey=1pN/KGxPtg3MjmQ/kslmQVuuczGvuOVbHCumUlMkyevlgTkNKNYRVntSTj3PeC2brCmxWZRIENcb+ASt557mkg==;BlobEndpoint=https://teamkvillestorage.blob.core.windows.net/;TableEndpoint=https://teamkvillestorage.table.core.windows.net/;";


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
