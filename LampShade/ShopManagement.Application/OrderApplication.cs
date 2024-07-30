using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application;

public class OrderApplication : IOrderApplication
{
    private readonly IAuthHelper _authHelper;
    private readonly IOrderRepository _orderRepository;
    public OrderApplication(IAuthHelper authHelper,IOrderRepository orderRepository, IConfiguration configuration)
    {
        _authHelper = authHelper;
        _orderRepository = orderRepository;
    }

    public long PlaceOrder(Cart cart)
    {
        var currentAccountId = _authHelper.CurrentAccountId();
        var order = new Order(currentAccountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);
        foreach (var cartItem in cart.Items)
        {
            var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
             order.AddItem(orderItem);
        }
        _orderRepository.Create(order);
        _orderRepository.SaveChanges();
        return order.Id;
    }

    public void PaymentSucceeded(long orderId, long refId)
    {
        var order = _orderRepository.Get(orderId);
        order.PaymentSucceeded(refId);
        var issueTrackingNo = CodeGenerator.Generate("s");
        order.SetIssueTrackingNo(issueTrackingNo);
        //Reduce OrderItem Form Inventory
        _orderRepository.SaveChanges();

    }
} 