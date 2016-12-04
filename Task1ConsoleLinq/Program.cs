using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1ConsoleLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShawarmaEntities())
            {
                //1.Add more carrot to storage

                var product = db.Ingredient.Where(i => i.IngredientName == "Carrot").First();
                Console.WriteLine($"Was {product.TotalWeight}");
                product.TotalWeight += 100;
                db.SaveChanges();
                Console.WriteLine($"Now {product.TotalWeight}");

                //3. New shawarma recipe
                ShawarmaRecipe sr = new ShawarmaRecipe
                {
                    ShawarmaID = 2,
                    IngredientID = 12,
                    Weight = 25
                };
                Console.WriteLine("Was");
                foreach (var item in db.ShawarmaRecipe)
                {
                    Console.WriteLine(item.ShawarmaRecipeID);
                }

                db.ShawarmaRecipe.Add(sr);
                db.SaveChanges();

                Console.WriteLine("Now");
                foreach (var item in db.ShawarmaRecipe)
                {
                    Console.WriteLine(item.ShawarmaRecipeID);
                }

                //4. Set new price

                var itemForNewPrice = db.PriceController.Where(pc => pc.ShawarmaID == 1 
                && pc.SellingPointID == 1).First();
                Console.WriteLine($"Was {itemForNewPrice.Price}");
                itemForNewPrice.Price = 10;
                db.SaveChanges();
                Console.WriteLine($"Now {itemForNewPrice.Price}");

                //5. Add new selling point

                SellingPoint sp = new SellingPoint
                {
                    Adress = "7 Avenue 7",
                    SellingPointCategoryID = 2,
                    ShawarmaTitle = "Unearthly Shawarma"
                };

                Console.WriteLine("Was");
                foreach (var item in db.SellingPoint)
                    Console.WriteLine(item.SellingPointID);

                db.SellingPoint.Add(sp);
                db.SaveChanges();

                Console.WriteLine("Now");
                foreach (var item in db.SellingPoint)
                    Console.WriteLine(item.SellingPointID);

                //6. Add new Seller
                Seller s = new Seller
                {
                    SellerName = "Gandalph",
                    SellingPointID = 2
                };

                Console.WriteLine("Was");
                foreach (var item in db.Seller)
                    Console.WriteLine(item.SellerID);

                db.Seller.Add(s);
                db.SaveChanges();

                Console.WriteLine("Now");
                foreach (var item in db.Seller)
                    Console.WriteLine(item.SellerID);

                //7.

                Console.ReadKey();
            }
        }
    }
}
