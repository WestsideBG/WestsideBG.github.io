using System.Drawing;

namespace Sweetdreams.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public ApplicationUser Author { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public decimal Price { get; private set; }

        public int Count { get; private set; }

        public IEnumerable<ProductSize> Sizes { get; private set; }

        public IEnumerable<ProductColor> Colors { get; private set; }

        public IEnumerable<ApplicationUser> Customers { get; private set; }

        public bool SoldOut
        {
            get
            {
                return this.Count < 1;
            }
        }

        public Product()
        {

        }

        public Product(string title, string description, ApplicationUser applicationUser, decimal price, int count)
        {
            Title = title;
            Description = description;
            Author = applicationUser;
            Price = price;
            Count = count;
            Customers = new List<ApplicationUser>();
            Sizes = new List<ProductSize>();
            Colors = new List<ProductColor>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

    }
}
