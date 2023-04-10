namespace TeamKville.Server.Data.DataModels
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int Age { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Comment>? Comments { get; set; }

    }
}