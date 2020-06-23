using System.Collections.Generic;
using BillingService.Models;

namespace BillingService.Tools {
    public class Stock {
        public product prod { get; set; }
        public uint count { get; set; }
        public Stock (product _product) {
            prod = _product;
            count = 1;
        }
        public void inc () {
            count++;
        }
    }

    public class Counter {
        private List<Stock> stocks = new List<Stock> ();

        public Counter () { }

        public void count (product _product) {
            bool found = false;
            foreach (Stock n in stocks) {
                if (n.prod.productName == _product.productName) {
                    found = true;
                    n.inc ();
                }
            }
            if (!found) {
                stocks.Add (new Stock (_product));
            }
        }

        public List<Stock> getStocks () {
            return stocks;
        }

        public uint getCount (product _product) {
            foreach (Stock n in stocks) {
                if (n.prod.productName == _product.productName) {
                    return n.count;
                }
            }
            return 0;
        }
    }
}
