using System.Collections.Generic;
using AutoMapper;
using ProductService.DataTransferObjects;
using ProductService.Models;

namespace ProductService.Profiles {
    public class ProductProfile : Profile {

        public ProductProfile () {
            CreateMap<AddProductObject, product> ();
            CreateMap<product, OutProductObject> ();
            CreateMap<UpdateProductObject, product> ();
            CreateMap<GetBillObject, IEnumerator<sale>> ();
        }

    }
}
