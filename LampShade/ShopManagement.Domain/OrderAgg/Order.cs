using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg;

public class Order : EntityBase
{
    public Order(long accountId, double totalAmount, double discountAmount, double payAmount)
    {
        AccountId = accountId;
        TotalAmount = totalAmount;
        DiscountAmount = discountAmount;
        PayAmount = payAmount;
        Items = new List<OrderItem>();
        IsPaid = false;
        IsCanceled = false;
        RefId = 0;
    }
    public long AccountId { get; private set; }
    public double TotalAmount { get; private set; }
    public double DiscountAmount { get; private set; }
    public double PayAmount { get; private set; }
    public bool IsPaid { get; private set; } // Payment status
    public bool IsCanceled { get; private set; }
    public string IssueTrackingNo { get; private set; }  // Issue Tracking
    public long RefId { get; private set; } // Purchase number == True
    public List<OrderItem> Items { get; private set; }

    public void PaymentSucceeded(long refId)
    {
        IsPaid = true;
        if (refId != 0)
            RefId = refId;
    }

    public void SetIssueTrackingNo(string number)
    {
        IssueTrackingNo = number;
    }

    public void Cancel()
    {
        IsCanceled = true;
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }
}