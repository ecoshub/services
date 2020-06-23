using System;
using System.Collections.Generic;
using System.Linq;
using BillingService.Contexts;
using BillingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingService.Repositories {

    public class BillingRepository : IBillingRepository {

        private readonly BillingDatabaseContext _context;

        public BillingRepository (BillingDatabaseContext context) {
            _context = context;
        }

        public List<sale> getBill (Guid billId) {
            throw new NotImplementedException ();
        }

        public void invoice (List<Guid> product_ids) {
            throw new NotImplementedException ();
        }
    }
}
