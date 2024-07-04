using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    public class ShoppingBasket
    {
        private List<Product> productsList;

        public ShoppingBasket()
        {
            productsList = new List<Product>();
        }

        public void Add(Product product)
        {
            if ((product != null) && (productsList != null))
            {
                productsList.Add(product);
            }
        }

        public Product? GetAt(int  productIndex)
        {
            Product? tempProduct = null;

            if (productsList != null)
            {
                tempProduct = productsList[productIndex];
            }
            return tempProduct;
        }

        public int Count()
        {
            int productCount = 0;

            if (productsList != null)
            {
                productCount = productsList.Count;
            }
            return productCount;
        }
    }
}
