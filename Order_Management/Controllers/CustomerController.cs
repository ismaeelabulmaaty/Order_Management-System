using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Repository.Contract;
using Order_Manag.Core.Specifications;
using Order_Manag.Core.Specifications.NewFolder;
using Order_Manag.Core.Specifications.ProductsSpec;
using Order_Management.DTOS;
using Order_Management.HandlingErrors;

namespace Order_Management.Controllers
{

    public class CustomerController : BaseController
    {
        private readonly IGenericRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;

        public CustomerController(IGenericRepository<Customer> CustomerRepo )
        {
            _customerRepo = CustomerRepo;
            
        }

       
        [HttpGet("{id}")]        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]        public async Task<ActionResult<Customer>> GetCustomerById(int id)        {            var Spec = new CustomerSpec(id);            var customer = await _customerRepo.GetByIdWithSpecAsync(Spec);            if (customer is null) return NotFound(new ApisResponse(404));
            return Ok(customer);        }

        [HttpPost()]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> Add(CustomerRequestDto cust)
        {

            var customer = await _customerRepo.AddAsync(new Customer()
            {
                Name = cust.Name,
                Email = cust.Email
            });
            await _customerRepo.SaveChangesAsync();
            if (customer is null) return NotFound(new ApisResponse(404));
            return Ok(customer);

        }
    }
}
