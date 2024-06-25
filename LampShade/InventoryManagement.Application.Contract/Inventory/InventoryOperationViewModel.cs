namespace InventoryManagement.Application.Contract.Inventory;

public class InventoryOperationViewModel
{
    public long Id { get; set; }
    public bool Operation { get; set; } // in or out
    public long Count { get; set; }
    public long OperatorId { get; set; } // Wer?
    public string Operatior { get; set; }
    public string OperationDate { get; set; } 
    public long CurrentCount { get; set; } // meghdare anbar
    public string Description { get; set; } // Warum
    public long OrderId { get; set; } 
}