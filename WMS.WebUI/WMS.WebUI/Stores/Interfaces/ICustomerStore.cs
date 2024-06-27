using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.ViewModels.CustomerViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface ICustomerStore
{
    Task<PaginatedApiResponse<CustomerDisplayViewModel>> GetCustomers(CustomerQueryParameters queryParameters);
    Task<CustomerDisplayViewModel>GetById(int id);
    Task<CustomerDisplayViewModel> Create(CustomerActionViewModel customer);
    Task Update(CustomerActionViewModel customer);
    Task Delete(int id);
}
