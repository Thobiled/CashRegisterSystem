using System.Globalization;


namespace CashRegister
{
    public class Product
    {
        decimal taxablePrice = 0;
        decimal basicTaxRate = 0;
        decimal dutyTaxRate = 0;
        decimal computedTax = 0;
        bool isTaxExempted = false;
        bool isImported = false;
        string name;

        public Product(string name, decimal taxablePrice,   decimal basicTaxRate, 
                       decimal dutyTaxRate, bool isTaxExempted, bool isImported)
        {
            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2;
            this.taxablePrice =  taxablePrice;
            this.basicTaxRate =  basicTaxRate;
            this.dutyTaxRate =  dutyTaxRate;
            this.isTaxExempted = isTaxExempted;
            this.isImported = isImported;
            this.name = name;
        }

        public void ComputeTax()
        {
            if (!this.isTaxExempted)
            {
                this.computedTax =  this.taxablePrice * (this.basicTaxRate / 100);
            }

            if ((this.isImported)  && (!this.isTaxExempted))
            {
                this.computedTax +=  this.taxablePrice * (this.dutyTaxRate / 100);
            }
        }

        public decimal ComputedTax
        {
            get { return this.computedTax; }
        }

        public decimal BasicTaxRate
        {
            get { return this.basicTaxRate; }
            set { this.basicTaxRate = value; }
        }

        public decimal DutyTaxRate
        {
            get { return this.dutyTaxRate; }
            set { this.dutyTaxRate = value; }
        }

        public decimal Price
        {
            get { return taxablePrice; }
            set { taxablePrice = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryDescription { get; set; } = string.Empty;

    }
}
