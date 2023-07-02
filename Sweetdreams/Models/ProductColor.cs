namespace Sweetdreams.Models
{
    public class ProductColor
    {
        public int Id { get; set; }

        public string ColorName { get; private set; }

        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public ProductColor()
        {

        }

        public ProductColor(string colorName)
        {
            ColorName = colorName;
        }
    }
}
