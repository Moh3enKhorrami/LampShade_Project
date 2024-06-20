using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application;

public class CustomerDiscountApplication : ICustomerDiscountApplication
{
    private readonly ICustomerDiscountRepository _customerDiscountRepository;

    public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
    {
        _customerDiscountRepository = customerDiscountRepository;
    }

    public OperationResult Define(DefineCustomerDiscount command)
    {
        var operation = new OperationResult();
        if (_customerDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            return operation.Failed(ApplicationMessages.Dulpicated);
        
        var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, 
            Convert.ToDateTime(command.StartDate) , Convert.ToDateTime(command.EndDate), command.Reason);
        _customerDiscountRepository.Create(customerDiscount);
        _customerDiscountRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditCustomerDiscount command)
    {
        var operation = new OperationResult();
        var customerDiscount = _customerDiscountRepository.Get(command.Id);
        if (customerDiscount == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_customerDiscountRepository.Exists(x =>
                x.Id == command.Id && x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        customerDiscount.Edit(command.ProductId, command.DiscountRate, Convert.ToDateTime(command.StartDate),
            Convert.ToDateTime(command.EndDate), command.Reason);
        _customerDiscountRepository.SaveChanges();
        return operation.Succedded();
    }

    public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
    {
        return _customerDiscountRepository.Search(searchModel);
    }

    public EditCustomerDiscount GetDetails(long id)
    {
        return _customerDiscountRepository.GetDetails(id);
    }
}