using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Contexts;
using ProductService.DataTransferObjects;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Controllers {
    [Route ("/api/products")]
    [ApiController]
    public class ProductController : ControllerBase {

        private readonly IMapper _mapper;
        private readonly ProductDatabaseContext _context;
        private readonly IProductRepository _repo;

        public ProductController (IProductRepository repo, ProductDatabaseContext context, IMapper mapper) {
            _repo = repo;
            _context = context;
            _mapper = mapper;
        }

        // add product endpoint
        // "/api/products"
        [HttpPost]
        [ProducesResponseType (typeof (product), (int) HttpStatusCode.OK)]
        [ProducesResponseType (400)]
        [ProducesResponseType (200)]
        public ActionResult<OutProductObject> addProduct ([FromBody] AddProductObject newProduct) {
            if (newProduct == null) {
                return BadRequest (ModelState);
            }
            if (_repo.productExistsByName (newProduct.productName)) {
                return NotFound (ModelState);
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
