using System;
using System.Collections.Generic;
using System.Linq;
using BillingService.Contexts;
using BillingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingService.Repositories {

    public class BillingRepository : IBillingRepository {

        private readonly BillingDatabaseContext _context;

        public BillingRepository(BillingDatabaseContext context) {
            _context = context;
        }

        public List<sale> getBill(Guid billId) {
            return _context.sale.AsNoTracking().Where(b => b.billId == billId).ToList();
        }

        public void invoice(List<Guid> product_ids) {
            throw new NotImplementedException();
        }

        public void addInvoice(sale _sale) {
            _context.Add(_sale);
            _context.SaveChanges();
            _context.Entry(_sale).State = EntityState.Detached;
        }

        public List<Guid> getBills() {
            List<Guid> bills = _context.sale.Select(b => b.billId).ToList();
            return bills;
        }
    }
}
