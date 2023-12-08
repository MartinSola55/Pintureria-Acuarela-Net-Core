namespace Pinturería_Acuarela.Models.ViewModels.Home
{
    public class IndexViewModel
    {
        public ApplicationUser User = new();
        public int StocklessProducts = 0;
        public int StockAlertProducts = 0;
        public int PendingOrders = 0;
    }
}
