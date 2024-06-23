using System.Security.AccessControl;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg;

public class Inventory : EntityBase
{
    public Inventory(long productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        InStock = false;
    }

    public long ProductId { get; private set; }
    public double UnitPrice { get; private set; }
    public bool InStock { get; private set; }
    public List<InventoryOperation> Operations { get; private set; }

    private long CalculateCurrentCount()
    {
        var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
        var minus = Operations.Where(x => !x.Operation).Sum((x => x.Count));
        return plus - minus;
    }

    public void Increase(long count, long operatorId, string description )
    {
        var currentcount = CalculateCurrentCount() + count;
        var operation = new InventoryOperation(true, count, operatorId, currentcount,
            description, 0, Id );
        Operations.Add(operation);
        InStock = currentcount > 0;
    }

    public void Reduce(long count, long operatorId, string description, long orderId)
    {
        var currentcount = CalculateCurrentCount() - count;
        var operation = new InventoryOperation(false, count, operatorId,
            currentcount, description, orderId, Id);
        Operations.Add(operation);
        InStock = currentcount > 0;
    }
}



public class InventoryOperation
{
    public InventoryOperation(bool operation, long count, long operatorId, long currentCount, string description, long orderId, long inventoryId)
    {
        Operation = operation;
        Count = count;
        OperatorId = operatorId;
        CurrentCount = currentCount;
        Description = description;
        OrderId = orderId;
        InventoryId = inventoryId;
    }

    public long Id { get; private set; }
    public bool Operation { get; private set; } // in or out
    public long Count { get; private set; }
    public long OperatorId { get; private set; } // Wer?
    public DateTime OperationDate { get; private set; } 
    public long CurrentCount { get; private set; } // meghdare anbar
    public string Description { get; private set; } // Warum
    public long OrderId { get; private set; } 
    public long InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }    
    
}