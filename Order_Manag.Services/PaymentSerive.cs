using Microsoft.Extensions.Configuration;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Repository.Contract;
using Stripe;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Specifications.OrderSpec;
namespace Order_Manag.Services
{
    public class PaymentSerive : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Core.Entites.Order> _genericRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepo;

        public PaymentSerive(IConfiguration configuration,
            IGenericRepository<Core.Entites.Order> genericRepository,
            IGenericRepository<OrderItem> OrderItemRepo
            )
        {
            _configuration = configuration;
            _genericRepository = genericRepository;
            _orderItemRepo = OrderItemRepo;
        }
        public async Task<Core.Entites.Order> creatOrUpdatePaymentIntent(Core.Entites.Order order)
        {
            StripeConfiguration.ApiKey = _configuration["StripKeys:Secretkey"];

            Core.Entites.Order ByOrder = new Core.Entites.Order();

            if (ByOrder.OrderItems is null) return null;

            var subTotal = order.OrderItems.Sum(x => x.UnitPrice * x.Quantity);

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(ByOrder.PaymentIntentId?.ToString()))//create
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)subTotal,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "Card" }
                };
                paymentIntent = await service.CreateAsync(option);
                ByOrder.PaymentIntentId = (paymentIntent.Id);
                ByOrder.ClintSecret = paymentIntent.ClientSecret;
            }
            else //update
            {

                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)subTotal
                };
                paymentIntent = await service.UpdateAsync(ByOrder.PaymentIntentId.ToString(), option);
                ByOrder.PaymentIntentId = (paymentIntent.Id);
                ByOrder.ClintSecret = paymentIntent.ClientSecret;

            }


            _genericRepository.Update(ByOrder);
            return ByOrder;



        }

        public async Task<Core.Entites.Order> UpdatePaymentIntentToSucceedOrfailed(string PaymentIntent, bool flag)
        {
            var spec = new OrderWithPaymentSpec(PaymentIntent);
            var order = await _genericRepository.GetByIdWithSpecAsync(spec);
            if (flag)
            {
                order.Status = OrderStatus.PaymentSucceeded;
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed;
            }
            _genericRepository.Update(order);
            await _genericRepository.SaveChangesAsync();
            return order;
        }


    }
}
