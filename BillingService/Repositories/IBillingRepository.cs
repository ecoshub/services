using System;
using System.Collections.Generic;
using BillingService.Models;

namespace BillingService.Repositories {
    public interface IBillingRepository {
        void invoice (List<Guid> product_ids);
        List<sale> getBill (Guid billId);
    }
}
