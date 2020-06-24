using System;
using System.Collections.Generic;
using BillingService.Contexts;
using BillingService.Models;
using BillingService.Repositories;
using BillingService.Tools;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Controller {
    [Route ("/api/billing")]
    [ApiController]
    public class BillingController : ControllerBase {

        private readonly BillingDatabaseContext _context;
        private readonly IBillingRepository _repo;

        public BillingController (IBillingRepository repo, BillingDatabaseContext context) {
            _repo = repo;
            _context = context;
        }
        // * invoicing endpoint
        // * "/api/billing
        [HttpPost]
        [ProducesResponseType (400)]
        [ProducesResponseType (500)]
        [ProducesResponseType (200)]
        public ActionResult<List<sale>> invoice (List<Guid> product_ids) {
            List<Stock> stocks = Misc.getStocks (product_ids);
            CustomError err = Misc.stockCheck (stocks);
            if (err != null) {
                return Ok (err);
            }

            List<sale> sales = new List<sale> ();
            Guid billId = Guid.NewGuid ();
            // stock update for each sale.
            foreach (Stock st in stocks) {
                uint stockLeft = st.prod.productStock - st.count;
                sale _sale = new sale (billId, st.prod.productId, DateTime.Now, st.count, st.prod.productPrice, stockLeft);
                sales.Add (_sale);
                _repo.addInvoice (_sale);
                product prod = Comm.updateStockAmount (st.prod.productId, stockLeft);
                if (prod == null) {
                    return Problem ("Fatal Error", "Product service down", 500, "Service Error", "Fatal");
                }
            }
            return Ok (sales);
        }
        // * get bill endpoint
        // * "/api/billing
        [HttpGet ("{bill_id}")]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<List<sale>> getBill (Guid bill_id) {
            List<sale> sales = _repo.getBill (bill_id);
            if (sales == null) {
                return NotFound ();
            }
            return sales;
        }

    }
}
