namespace StockModule.BLL.StockSettings
{
    public class MaterialStockInfoDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public Single BarLength { get; set; }
        public List<StockListDto> Stocks { get; set; } = new List<StockListDto>();

    }
}