namespace Pintureria_Acuarela.Models.ViewModels.Businesses
{
    public class ProductsViewModel
    {
        public Business Business { get; set; } = new();
        public List<ProductBusiness> Products { get; set; } = [];
    }
}
