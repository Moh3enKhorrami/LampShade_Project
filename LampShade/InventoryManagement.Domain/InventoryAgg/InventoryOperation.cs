namespace InventoryManagement.Domain.InventoryAgg;

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
        OperationDate = DateTime.Now;
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