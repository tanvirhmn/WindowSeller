namespace StockModule.BLL.StockSettings
{
    public class StockListDto
    {
        public int Id { get; set; }
        public string Warehouse { get; set; } = String.Empty;
        public double Length { get; set; }
        public double Quantity { get; set; }
        public double Height { get; set; }
    }
}