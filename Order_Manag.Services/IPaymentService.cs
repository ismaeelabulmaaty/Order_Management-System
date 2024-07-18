using Order_Manag.Core.Repository.Contract;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Services
{
    public interface IPaymentService
    {
        Task<Order_Manag.Core.Entites.Order> creatOrUpdatePaymentIntent(Order_Manag.Core.Entites.Order order);

        Task<Core.Entites.Order> UpdatePaymentIntentToSucceedOrfailed(string PaymentIntent, bool flag);
    }
}
