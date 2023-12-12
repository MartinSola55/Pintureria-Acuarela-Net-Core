namespace Pintureria_Acuarela.Models.ViewModels.Businesses
{
    public class PendingOrdersViewModel
    {
        public Business Business { get; set; } = new();
        public List<Order> PendingOrders { get; set; } = [];
    }
}
