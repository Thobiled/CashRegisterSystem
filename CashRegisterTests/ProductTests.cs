using CashRegister;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Tests
{
    [TestClass()]
    public class ProductTests
    {
        Product testProduct;


        public ProductTests()
        {
            testProduct = new Product("Bread Loaf", new decimal(34.21), 10, 5, false, true);
        }

        [TestMethod()]
        public void ComputeTaxTest()
        {
            if (testProduct != null)
            {
                testProduct.ComputeTax();

                Assert.AreEqual(new decimal(39.34).ToString("C2"), (testProduct.ComputedTax + testProduct.Price).ToString("C2"));
            }
            else
            {
                Assert.Fail();
            }
        }

    }
}