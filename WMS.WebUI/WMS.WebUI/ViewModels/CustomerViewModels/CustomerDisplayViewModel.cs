namespace WMS.WebUI.ViewModels.CustomerViewModels
{
    public class CustomerDisplayViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public decimal? Discount { get; set; } = 0;
        //public ICollection<SaleDto> Sales { get; set; }
        
    }
}
