using WMS.WebUI.ViewModels.PartnerViewModels;

namespace WMS.WebUI.Mappings;

public static class SupplierMappings
{
    public static PartnerViewModel ToPartner(this SupplierViewModel viewModel)
    {
        return new PartnerViewModel()
        {
            Id = viewModel.Id,
            FullName = viewModel.FullName,
            PhoneNumber = viewModel.PhoneNumber,
            Balance = viewModel.Balance,
            Type = PartnerType.Supplier,
        };
    }
}
