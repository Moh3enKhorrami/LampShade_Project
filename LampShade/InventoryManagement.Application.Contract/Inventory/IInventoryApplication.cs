using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory;

public interface IInventoryApplication
{
     OperationResult Create(CreateInventory command);
     OperationResult Edit(EditInventory command);
     OperationResult Increase(IncreaseInventory command);
     OperationResult Reduce(List<ReduceInventory> command);
     OperationResult Reduce(ReduceInventory command);
     EditInventory Getdetails(long id);
     List<InventoryViewModel> Search(InventorySearchmodel searchmodel);
}