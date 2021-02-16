using bhs.Data;
using bhs.Dto;
using bhs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private BhsDbContext _bhsDbContext;

        public SalesController(BhsDbContext bhsDbContext)
        {
            _bhsDbContext = bhsDbContext;
        }

        [HttpGet]
        public IQueryable<Sale> GetAll()
        {
            return _bhsDbContext.Sales
                .Include(sale => sale.Seller)
                .Include(sale => sale.Vehicles);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _bhsDbContext.Sales
                .Include(sale => sale.Seller)
                .Include(sale => sale.Vehicles)
                .SingleOrDefaultAsync(sale => sale.Id == id);

            if(sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleDto createSaleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            createSaleDto.VehicleCodes = createSaleDto.VehicleCodes.Distinct().ToList();

            var seller = await _bhsDbContext.Sellers.FindAsync(createSaleDto.SellerCode);
            var vehicles = await _bhsDbContext.Vehicles
                .Where(vehicles => createSaleDto.VehicleCodes.Contains(vehicles.Code))
                .ToListAsync();


            if(seller == null || createSaleDto.VehicleCodes.Count != vehicles.Count)
            {
                return UnprocessableEntity();
            }

            var sale = new Sale
            {
                Seller = seller,
                Status = SaleStatus.WaitingPayment,
                Vehicles = vehicles
            };

            await _bhsDbContext.Sales.AddAsync(sale);

            await _bhsDbContext.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = sale.Id }, sale);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSale(UpdateSaleDto updateSaleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sale = await _bhsDbContext.Sales.FindAsync(updateSaleDto.Id);

            if(sale == null)
            {
                return NotFound();
            }

            var allowedTransitions = new List<Tuple<SaleStatus, SaleStatus>>()
            {
                new Tuple<SaleStatus, SaleStatus>(SaleStatus.WaitingPayment, SaleStatus.PaymentApproved),
                new Tuple<SaleStatus, SaleStatus>(SaleStatus.WaitingPayment, SaleStatus.Canceled),
                new Tuple<SaleStatus, SaleStatus>(SaleStatus.PaymentApproved, SaleStatus.InTransit),
                new Tuple<SaleStatus, SaleStatus>(SaleStatus.PaymentApproved, SaleStatus.Canceled),
                new Tuple<SaleStatus, SaleStatus>(SaleStatus.InTransit, SaleStatus.Delivered),
            };

            var isTransitionAllowed = allowedTransitions
                .Any(transition => transition.Item1 == sale.Status && transition.Item2 == updateSaleDto.Status);

            if (!isTransitionAllowed)
            {
                return UnprocessableEntity();
            }

            sale.Status = updateSaleDto.Status.Value;

            await _bhsDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
