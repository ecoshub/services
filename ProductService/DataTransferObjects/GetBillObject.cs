using System.Collections.Generic;
using ProductService.Models;

namespace ProductService.DataTransferObjects {
    public class GetBillObject {
        public IEnumerable<sale> bill { get; set; }
    }
}
