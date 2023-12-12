namespace Pintureria_Acuarela.Models.ViewModels.Businesses
{
    public class DetailsViewModel
    {
        public Business Business { get; set; } = new();
        public int StocklessProducts { get; set; }
        public int StockAlertProducts { get; set; }
        public int TotalProducts { get; set; }
        public int PendingOrders { get; set; }
        public double TotalLiters { get; set; }
    }
}
