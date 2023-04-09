namespace TeamKville.Shared
{
    public class SharedClass
    {
        public static string GetImage(string type, int id)
        {
            //Ändra till environment variable
            return ($"https://teamkvillestorage.blob.core.windows.net/{type}/{id}.png");
        }

        public static string CapitalizeFirstLetter(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
