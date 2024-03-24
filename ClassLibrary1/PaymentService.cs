using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;
using Talabat.Core.Models.OrderAggragation;
using Talabat.Core.Services.Contract;
using Product = Talabat.Core.Models.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration ,
            IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:Secretkey"];

            var Basket = await _basketRepository.GetBasketAsync(BasketId);

            if(Basket == null) { return null; };

            var ShippingPrice = 0m;

            if (Basket.DelivieryMethodId.HasValue)
            {
             var DelivaryMethod =   await _unitOfWork.Repository<DelivaryMethod>().GetAsync(Basket.DelivieryMethodId.Value) ;
                Basket.SippingPrice = DelivaryMethod.Cost;
                ShippingPrice = DelivaryMethod.Cost;
            }

            if(Basket?.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);

                    if(item.Price != Product.Price)
                        item.Price = Product.Price;
                    
                }
            }

            PaymentIntent paymentIntent;

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(Basket.PaymentIntentId)) //Create New Payment Intent 
            {
                var CreateOption = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Basket.Items.Sum(item => item.Price * 100 * item.Quantity * 100) + (long)ShippingPrice * 100 ,

                    Currency ="usd",

                    PaymentMethodTypes = new List<string>() { "card"} 
                };

                paymentIntent = await paymentIntentService.CreateAsync(CreateOption); //Intgrate with stripe //

                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;


            }
            else // Update Existing Payment Intent 
            {
                var UpdateOption = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Basket.Items.Sum(item => item.Price * 100 * item.Quantity * 100) + (long)ShippingPrice * 100
                };

                await paymentIntentService.UpdateAsync(Basket.PaymentIntentId, UpdateOption);
            }

            await _basketRepository.UpdateBasketAsync(Basket);

            return Basket;

        }

    }
}
