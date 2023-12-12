namespace Pintureria_Acuarela.Models.ViewModels.Colors
{
    public class IndexViewModel
    {
        public IEnumerable<Color> Colors { get; set; } = new List<Color>();
        public Color CreateViewModel { get; set; } = new Color();
    }
}
