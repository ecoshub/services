using System;
using System.Collections.Generic;
using BillingService.Models;

namespace BillingService.Tools {
    public class Misc {
        public static List<Stock> getStocks (List<Guid> product_ids) {
            Counter counter = new Counter ();
            foreach (Guid id in product_ids) {
                product _product = Comm.getProduct (id);
                if (_product != null) {
                    counter.count (_product);
                }
            }
            List<Stock> stocks = counter.getStocks ();
            return stocks;
        }

        public static CustomError stockCheck (List<Stock> stocks) {
            List<string> notEnoughStock = new List<string> ();
            bool less = false;
            foreach (Stock st in stocks) {
                if (st.count > st.prod.productStock) {
                    less = true;
                    notEnoughStock.Add (st.prod.productName);
                }
            }
            string errorString = "";
            if (notEnoughStock.Count != 0) {
                if (notEnoughStock.Count == 1) {
                    errorString = "Stock amount is less then buy amount for: '" + notEnoughStock[0] + "'";
                } else {
                    errorString = "Stock amount is less then buy amount for: '" + notEnoughStock[0] + "'";
                    for (int i = 1; i < notEnoughStock.Count; i++) {
                        errorString += ", '" + notEnoughStock[i] + "'";
                    }
                }
                if (less) {
                    return new CustomError ("Low Stock", errorString, "11");
                }
            }
            return null;
        }
    }
}
