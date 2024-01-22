using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
       
    
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepositry;

        public OrderService(IUnitOfWork unitOfWork,
            IBasketRepository basketRepositry)
        {
            this._unitOfWork = unitOfWork;
            _basketRepositry = basketRepositry;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from repo 
            var basket = await _basketRepositry.GetBasketAsync(basketId);
            //get items from product repo 
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOreder = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOreder, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            //get delivery method 
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            // calc subtotal 
            var subtotal = items.Sum(item=>item.Price*item.Quantity);
            //create the order 
            var order = new Order(items,buyerEmail,shippingAddress,deliveryMethod,subtotal);
            _unitOfWork.Repository<Order>().Add(order);
            //TODO save to db 
          var result = await _unitOfWork.Complete();
            if (result <=0)
            {
                return null;
            }

            //delete basket 
            await _basketRepositry.DeleteBasketAsync(basketId);
            //return order 
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrdersByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id,buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

      
    }
}
