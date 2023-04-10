﻿namespace TeamKville.Server.Data.DataModels
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
