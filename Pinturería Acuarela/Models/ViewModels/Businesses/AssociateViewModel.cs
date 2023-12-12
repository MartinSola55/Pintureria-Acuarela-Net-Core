namespace Pintureria_Acuarela.Models.ViewModels.Businesses
{
    public class AssociateViewModel
    {
        public Business Business { get; set; } = new();
        public ProductBusiness ProductBusiness { get; set; } = new();
        public List<Product> Products { get; set; } = [];
    }
}
