using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Contexts;
using ProductService.DataTransferObjects;
using ProductService.Models;
using ProductService.Repositories;
using ProductService.Tools;

namespace ProductService.Controllers {
    [Route ("/api/products")]
    [ApiController]
    public class ProductController : ControllerBase {

        private readonly IMapper _mapper;
        private readonly ProductDatabaseContext _context;
        private readonly IProductRepository _repo;

        private string alreadyExistsHeader = "Already Exists";
        private string alreadyExistsBody = "The Product you have provided is already exists.";
        private string alreadyExistsCode = "PE001";

        public ProductController (IProductRepository repo, ProductDatabaseContext context, IMapper mapper) {
            _repo = repo;
            _context = context;
            _mapper = mapper;
        }

        // * get product endpoint
        // * "/api/products/{product_id}"
        [HttpGet ("{product_id}")]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<product> getProduct (Guid product_id) {
            product selected = _repo.getProduct (product_id);
            if (selected == null) {
                return NotFound ();
            }
            return Ok (selected);
        }

        // * get all product
        // * "/api/products"
        // * This endpoint is created for testing/debugging this service.
        [HttpGet]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult getAllProducts () {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            IEnumerable<product> all = _repo.getAllProducts ();
            if (all == null) {
                return NotFound ();
            }
            return Ok (all);
        }

        // * add product endpoint
        // * "/api/products"
        [HttpPost]
        [ProducesResponseType (typeof (product), (int) HttpStatusCode.OK)]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<OutProductObject> addProduct ([FromBody] AddProductObject newProduct) {
            if (newProduct == null) {
                return BadRequest (ModelState);
            }
            if (_repo.productExistsByName (newProduct.productName)) {
                CustomError productAlreadyExists = new CustomError (
                    alreadyExistsHeader,
                    alreadyExistsBody,
                    alreadyExistsCode);
                return BadRequest (productAlreadyExists);
            }
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            product newProd = _mapper.Map<product> (newProduct);
            _repo.addProduct (newProd);
            return Ok (_mapper.Map<OutProductObject> (newProd));
        }

        // * update product endpoint
        // * "/api/products"
        [HttpPut]
        [ProducesResponseType (typeof (OutProductObject), (int) HttpStatusCode.OK)]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<OutProductObject> updateProduct (UpdateProductObject newProduct) {
            if (newProduct == null) {
                return BadRequest (ModelState);
            }
            product selected = _repo.getProduct (newProduct.productId);
            if (selected == null) {
                return NotFound ();
            }
            product newProd = _mapper.Map<product> (newProduct);
            if (selected.productName != newProd.productName) {
                if (_repo.productExistsByName (newProd.productName)) {
                    CustomError productAlreadyExists = new CustomError (
                        alreadyExistsHeader,
                        alreadyExistsBody,
                        alreadyExistsCode);
                    return NotFound (productAlreadyExists);
                }
            }
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            newProd.productRegisterDate = DateTime.Now;
            _repo.updateProduct (newProd);
            OutProductObject outProduct = _mapper.Map<OutProductObject> (newProd);
            return Ok (outProduct);
        }

        // * delete product endpoint
        // * "/api/products/{product_id}"
        [HttpDelete ("{product_id}")]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<OutProductObject> deleteProduct (Guid product_id) {
            product selected = _repo.getProduct (product_id);
            if (selected == null) {
                return NotFound ();
            }
            _repo.deleteProduct (product_id);
            OutProductObject outProduct = _mapper.Map<OutProductObject> (selected);
            return Ok (outProduct);
        }

        // * delete product endpoint
        // * "/api/products/reset
        [HttpPost ("reset")]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult resetSeed () {
            // clear products
            _context.product.RemoveRange (_context.product.OrderBy (c => c.productName).ToList ());
            // add again
            product temp = new product ("Asus N550JK", 20, 7799.90, "Asus N550JK laptop");
            _context.product.Add (temp);
            _context.SaveChanges ();
            temp = new product ("IPhone S12", 56, 10999.90, "IPhone S12 Smartphone");
            _context.product.Add (temp);
            _context.SaveChanges ();
            temp = new product ("Logitech K360", 48, 299.90, "Logitech wireless keyboard");
            _context.product.Add (temp);
            _context.SaveChanges ();
            temp = new product ("Logitech A67", 20, 7799.90, "Logitech 3+1 Sound System");
            _context.product.Add (temp);
            _context.SaveChanges ();
            temp = new product ("Samsung A50", 85, 7799.90, "Samsung A50 Smartphone");
            _context.product.Add (temp);
            _context.SaveChanges ();
            return Ok ();
        }

        // * update product stock endpoint
        // * "/api/products/{product_id}/stock/{new_stock}  
        [HttpPut ("{product_id}/stock/{new_stock}")]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<product> updateProductStock (Guid product_id, uint new_stock) {
            product selected = _repo.getProduct (product_id);
            if (selected == null) {
                return NotFound ();
            }
            selected.productStock = new_stock;
            _repo.updateProduct (selected);
            return Ok (selected);
        }
        // * buy product(s) endpoint
        // * "/api/products/buy
        [HttpPost ("buy")]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<List<sale>> buyProduct (List<Guid> product_ids) {
            List<sale> sales;
            CustomError err;
            (sales, err) = _repo.buyProduct (product_ids);
            if (err != null) {
                return BadRequest (err);
            }
            return Ok (sales);
        }

        // * buy product(s) endpoint
        // * "/api/products/bill
        [HttpGet ("bill/{bill_id}")]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<List<sale>> getBill (Guid bill_id) {
            List<sale> sales = _repo.getBill (bill_id);
            if (sales == null) {
                return NotFound ();
            }
            return Ok (sales);
        }
    }
}
