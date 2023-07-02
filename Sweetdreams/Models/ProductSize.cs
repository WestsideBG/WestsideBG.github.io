namespace Sweetdreams.Models
{
    public class ProductSize
    {
        public int Id { get; private set; }

        public string Size { get; private set; }

        public int ProductId { get; private set; }

        public Product Product { get; private set; }

        public ProductSize()
        {

        }

        public ProductSize(string size)
        {
            Size = size;
        }
    }
}
