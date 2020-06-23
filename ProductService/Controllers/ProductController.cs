using System;
using System.Collections.Generic;
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
    }
}
