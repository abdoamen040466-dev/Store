using Store.Domain.Entities.Orders;

namespace Store.Services.Specifications.Orders;

public class OrderSpecification : BaseSpecifications<Guid, Order>
{
    public OrderSpecification(Guid id, string email) : base(o => o.Id == id && o.UserEmail.ToLower() == email.ToLower())
    {
        Includes.Add(o => o.DeliveryMethod);
        Includes.Add(o => o.Items);
    }
    public OrderSpecification(string email) : base(o => o.UserEmail.ToLower() == email.ToLower())
    {
        Includes.Add(o => o.DeliveryMethod);
        Includes.Add(o => o.Items);

        AddOrderByDescending(o => o.OrderDate);
    }
}
