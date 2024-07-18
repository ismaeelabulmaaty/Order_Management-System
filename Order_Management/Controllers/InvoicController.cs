using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Repository.Contract;
using Order_Manag.Core.Specifications.InvoicSpec;
using Order_Manag.Core.Specifications.ProductsSpec;
using Order_Management.DTOS;
using Order_Management.HandlingErrors;

namespace Order_Management.Controllers
{
    public class InvoicController : BaseController
    {
        private readonly IGenericRepository<Invoice> _invoicRepo;

        
        public InvoicController(IGenericRepository<Invoice> InvoicRepo )
        {
            _invoicRepo = InvoicRepo;
        }


       
        [HttpGet("GetAllInvoice")]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoiceAsync()
        {

            var Spec = new InvoicSpec();
            var Invoic = await _invoicRepo.GetAllWithSpecAsync(Spec);
            return Ok(Invoic);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Invoice>> GetInvoicById(int id)
        {
            var Spec = new InvoicSpec(id);
            var invoic = await _invoicRepo.GetByIdWithSpecAsync(Spec);
            if (invoic is null) return NotFound(new ApisResponse(404));
            return Ok(invoic);

        }





    }
}
