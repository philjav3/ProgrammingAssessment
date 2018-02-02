/* 
 * C# Programming Assessment
 * Phil Javinsky III
 * 3 October 2017
 */

using System;

namespace ProgrammingAssessment
{
	public enum ProductType { Book, Food, Medical, Other };

    public class Product {
        private int quantity;
        private string name;
        private double price;
        private ProductType type;
        private bool imported;

        // getter for the quantity of the product
		public int GetQuantity() {
			return quantity;
		}
        // getter for the product's name
		public string GetName() {
			return name;
		}
        // getter for the product's price
		public double GetPrice() {
			return price;
		}
        // setter for the product's price
        public void SetPrice(double p) {
            price = p;
		}
        // getter for the product's type
        public ProductType GetProductType() {
			return type;
		}
        // getter for the product's imported boolean
		public bool GetImported() {
            return imported;
		}

        /* default constructor for a product
         * parameters: pQuantity, an int of the quantity of the product
         *             pName, a string of the name of the product
         *             pPrice, a double for the price of the product
         *             pType, the type of the product. Book, Food, Medical, or Other
         *             pImported, a boolean for whether the product is imported or not
         */
        public Product(int pQuantity, string pName, double pPrice, ProductType pType, bool pImported) {
            quantity = pQuantity;
            name = pName;
            price = pPrice;
            type = pType;
            imported = pImported;
        }
    }

    class Program
    {
        // Adds up the total price of the basket.
        // parameters: products, an array of the products in the basket
        // return: total, the total price
        public static double Total(Product[] products) {
            double total = 0;

            for (int i = 0; i < products.Length; i++) {
                total += products[i].GetPrice() * products[i].GetQuantity();    
            }

            return total;
        }

        // Calculates the total tax for basket and the new prices of items that had tax applied.
        // parameters: products, an array of the products in the basket
        // return: totalTax, the total taxes for the basket
        public static double CalculateTax(Product[] products) {
            double totalTax = 0;

			for (int i = 0; i < products.Length; i++) {
				double taxRate = 0;

                // 10% sales tax + 5% sales tax
                if (products[i].GetProductType() == ProductType.Other && products[i].GetImported()) {
                    taxRate = 0.15;
				}
				// Import duty is an additional sales tax applicable on all imported goods at a rate of 5%, with no exemptions.
				else if (products[i].GetImported()) {
                    taxRate = 0.05;
                }
				// 10% sales tax on all goods except books, food, and medical products that are exempt.
				else if (products[i].GetProductType() == ProductType.Other) {
                    taxRate = 0.1;
                }

				double tax = products[i].GetPrice() * taxRate;

                // round up to nearest 0.05
                tax = Math.Ceiling(tax * 20) / 20;

                // set product's new price to include tax
				products[i].SetPrice(products[i].GetPrice() + tax);

                totalTax += tax * products[i].GetQuantity();
            }

            return totalTax;
		}

        // Prints the product's quantity, name, and price to the console.
        // parameters: products, an array of products in the basket
        public static void Print(Product[] products) {
            for (int i = 0; i < products.Length; i++) {
                if (products[i].GetQuantity() > 1) {
                    double price = products[i].GetPrice() * products[i].GetQuantity();
                    Console.WriteLine("" + products[i].GetQuantity() + " " + products[i].GetName() + ": " + price.ToString("0.00") 
                                      + " (" + products[i].GetPrice().ToString("0.00") + " each)");
                }
                else {
                    Console.WriteLine("" + products[i].GetQuantity() + " " + products[i].GetName() + ": " + products[i].GetPrice().ToString("0.00"));
                }
            }
        }

        static void Main(string[] args)
        {
            // Shopping Basket 1
            Product product0 = new Product(1, "book", 12.49, ProductType.Book, false);
            Product product1 = new Product(1, "music CD", 14.99, ProductType.Other, false);
            Product product2 = new Product(1, "chocolate bar", 0.85, ProductType.Food, false);
            Product[] basket1 = { product0, product1, product2 };

            Console.WriteLine("Shopping Basket 1:");
            Print(basket1);

            Console.WriteLine();
            Console.WriteLine("Output 1:");
            double basket1Tax = CalculateTax(basket1);
            Print(basket1);
            Console.WriteLine("Sales Taxes: " + basket1Tax.ToString("0.00"));
            Console.WriteLine("Total: " + Total(basket1));


			// Shoppping Basket 2
            Product product3 = new Product(1, "imported box of chocolates", 10.00, ProductType.Food, true);
			Product product4 = new Product(1, "imported bottle of purfume", 47.50, ProductType.Other, true);
			Product[] basket2 = { product3, product4 };

            Console.WriteLine("\n");
			Console.WriteLine("Shopping Basket 2:");
			Print(basket2);

			Console.WriteLine();
			Console.WriteLine("Output 2:");
			double basket2Tax = CalculateTax(basket2);
			Print(basket2);
			Console.WriteLine("Sales Taxes: " + basket2Tax.ToString("0.00"));
			Console.WriteLine("Total: " + Total(basket2));


			// Shopping Basket 3
            Product product5 = new Product(1, "imported bottle of perfume", 27.99, ProductType.Other, true);
			Product product6 = new Product(1, "bottle of perfume", 18.99, ProductType.Other, false);
            Product product7 = new Product(1, "packet of headache pills", 9.75, ProductType.Medical, false);
			Product product8 = new Product(1, "imported box of chocolates", 11.25, ProductType.Food, true);
			Product[] basket3 = { product5, product6, product7, product8 };

            Console.WriteLine("\n");
			Console.WriteLine("Shopping Basket 3:");
			Print(basket3);

			Console.WriteLine();
			Console.WriteLine("Output 3:");
			double basket3Tax = CalculateTax(basket3);
			Print(basket3);
			Console.WriteLine("Sales Taxes: " + basket3Tax.ToString("0.00"));
			Console.WriteLine("Total: " + Total(basket3));

			// Keep the console window open in debug mode.
			//Console.WriteLine("Press enter to exit.");
            //Console.ReadLine();
        }
    }
}
