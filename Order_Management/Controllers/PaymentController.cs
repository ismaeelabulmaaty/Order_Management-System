using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Repository.Contract;
using Order_Manag.Core.Specifications.ProductsSpec;
using Order_Manag.Services;
using Order_Management.DTOS;
using Order_Management.HandlingErrors;
using Stripe;

namespace Order_Management.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Order> _orderRepo;
        const string endpointSecret = "whsec_3d5244002fd404cd35e24302007fc09740098642c41f8e3ef83a75f0d5e562b2";
        public PaymentController(IPaymentService paymentService , IMapper mapper , IGenericRepository<Order> OrderRepo)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _orderRepo = OrderRepo;
            
        }

        [HttpGet("CreateOrUpdatePaymentIntent/{Id}")]
        public async Task<ActionResult<Order>> CreateOrUpdatePaymentIntent(int Id)
        {
            var Spec = new OrderWithIncludeAndQuerySpec(Id);            var order = await _orderRepo.GetByIdWithSpecAsync(Spec);
            await _paymentService.creatOrUpdatePaymentIntent(order);
            if (order is null) return BadRequest(new ApisResponse(400));
            return Ok(order);

        }


        [HttpPost("StripeWebHook")]
        public async Task<IActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);


                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    await _paymentService.UpdatePaymentIntentToSucceedOrfailed(paymentIntent.Id, false);

                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    await _paymentService.UpdatePaymentIntentToSucceedOrfailed(paymentIntent.Id, true);
                }


                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }


    }
}
