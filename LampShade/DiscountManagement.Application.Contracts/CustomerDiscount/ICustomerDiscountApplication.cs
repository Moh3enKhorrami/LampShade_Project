using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.CustomerDiscount;

public interface ICustomerDiscountApplication
{
    OperationResult Define(DefineCustomerDiscount command);
    OperationResult Edit(EditCustomerDiscount command);
    List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command);
    EditCustomerDiscount GetDetails(long id);
    
}