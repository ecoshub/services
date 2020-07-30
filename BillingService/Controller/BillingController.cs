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

        [HttpPost]
        [ProducesResponseType (400)]
        [ProducesResponseType (500)]
        [ProducesResponseType (200)]
        public ActionResult<List<sale>> newInvoice (List<Guid> product_ids) {
            // * get products in.
            List<product> products = new List<product> ();
            IDictionary<Guid, uint> stocks = new Dictionary<Guid, uint> ();
            foreach (Guid id in product_ids) {
                if (stocks.ContainsKey (id)) {
                    stocks[id]++;
                } else {
                    // * avoide multiple count.
                    stocks.Add (id, 1);
                    products.Add (Comm.getProduct (id));
                }
            }
            // * stock check.
            string errorString = "";
            foreach (product prod in products) {
                uint saleAmount = stocks[prod.productId];
                uint stockAmount = prod.productStock;
                if (saleAmount > stockAmount) {
                    errorString += $"Stock for {prod.productName} is not enough. Requested {saleAmount}, have {stockAmount}. ";
                }
            }
            if (errorString != "") {
                return Ok (new CustomError ("Not enough stock", errorString, "NS00"));
            }
            // * update new stocks.
            List<sale> sales = new List<sale> ();
            Guid billId = Guid.NewGuid ();
            foreach (product prod in products) {
                uint saleAmount = stocks[prod.productId];
                uint stockAmount = prod.productStock;
                uint stockLeft = stockAmount - saleAmount;
                sale _sale = new sale (billId, prod.productId, DateTime.Now, saleAmount, prod.productPrice, stockLeft);
                sales.Add (_sale);
                product newProduct = Comm.updateStockAmount (prod.productId, stockLeft);
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

        [HttpGet]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<List<Guid>> getBills () {
            return _repo.getBills ();
        }

    }
}
