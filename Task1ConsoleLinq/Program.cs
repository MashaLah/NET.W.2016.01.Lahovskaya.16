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
            Console.WriteLine("Add more carrot to storage");
            using (var db = new ShawarmaEntities())
            {
                //1.Add more carrot to storage
                string productName = "Carrot";
                var product = db.Ingredients.Where(i => i.IngredientName == productName).First();
                if (product == null) throw new ArgumentException($"{productName} doesn't exist in storage.");
                Console.WriteLine($"Was {product.TotalWeight}");
                product.TotalWeight += 100;
                db.SaveChanges();
                Console.WriteLine($"Now {product.TotalWeight}");
            }

            //2. Shawarma selling
            Console.WriteLine("Shawarma selling");
             using (var db = new ShawarmaEntities())
             {
                string shawarmaName = "Home Shawarma";
                int? shawarmaID = db.Shawarmas.Where(s => s.ShawarmaName == shawarmaName)
                    .Select(s => s.ShawarmaID).First();
                if (shawarmaID == null) throw new ArgumentException($"{shawarmaName} is not in menu.");
                var ingredients = db.ShawarmaRecipes.Where(s => s.ShawarmaID == shawarmaID);
                foreach (var item in ingredients)
                    Console.WriteLine($"Before {item.Ingredient.IngredientName} {item.Weight} {item.Ingredient.TotalWeight}");
                foreach (var item in ingredients)
                    item.Ingredient.TotalWeight -= item.Weight;
                foreach (var item in ingredients)
                    Console.WriteLine($"After {item.Ingredient.IngredientName} {item.Weight} {item.Ingredient.TotalWeight}");
            }

            Console.WriteLine("New shawarma recipe");
            using (var dbsr = new ShawarmaEntities()) 
            {
                //3. New shawarma recipe
                string shawarmaName = "Home Shawarma";
                int shawarmaID = dbsr.Shawarmas.Where(s => s.ShawarmaName == shawarmaName)
                    .Select(s => s.ShawarmaID).First();
                //if (shawarmaID==null) throw new ArgumentException($"{shawarmaName} is not in menu.");
                int ingredientID = dbsr.Ingredients.Where(i => i.IngredientName == "Onion")
                    .Select(i => i.IngredientID).First();

                ShawarmaRecipe sr = new ShawarmaRecipe
                {
                    ShawarmaID = shawarmaID,
                    IngredientID = ingredientID,
                    Weight = 13
                };
                Console.WriteLine("Was");
                foreach (var item in dbsr.ShawarmaRecipes)
                {
                    Console.Write(item.ShawarmaRecipeID+" ");
                }

                dbsr.ShawarmaRecipes.Add(sr);
                dbsr.SaveChanges();
            }
            Console.WriteLine();
            using (var db = new ShawarmaEntities())
            {
                Console.WriteLine("Now");
                foreach (var item in db.ShawarmaRecipes)
                {
                    Console.Write(item.ShawarmaRecipeID+" ");
                }
            }

            Console.WriteLine("Set new price");
            using (var db = new ShawarmaEntities())
            {
                //4. Set new price
                int shawarmaID = 1;
                int sellingPointID = 1;
                var itemForNewPrice = db.PriceControllers.Where(pc => pc.ShawarmaID == shawarmaID
                && pc.SellingPointID == sellingPointID).First();
                Console.WriteLine($"Was {itemForNewPrice.Price}");
                itemForNewPrice.Price = 9;
                db.SaveChanges();
                Console.WriteLine($"Now {itemForNewPrice.Price}");
            }

            Console.WriteLine(" Add new selling point");
             using (var db = new ShawarmaEntities())
             {
                //5. Add new selling point
                string adress = "7 Avenue 7";
                string category = "Cafe";
                string title = "Unearthly Shawarma";
                int categoryID = db.SellingPointCategories.Where(s => s.SellingPointCategoryName == category)
                    .Select(s => s.SellingPointCategoryID).First();
                 SellingPoint sp = new SellingPoint
                 {
                     Adress = adress,
                     SellingPointCategoryID = categoryID,
                     ShawarmaTitle = title
                 };

                Console.WriteLine("Was");
                foreach (var item in db.SellingPoints)
                    Console.Write(item.SellingPointID + " ");

                 db.SellingPoints.Add(sp);
                 db.SaveChanges();
             }
            Console.WriteLine();
            using (var db = new ShawarmaEntities())
            {
                Console.WriteLine("Now");
                foreach (var item in db.SellingPoints)
                    Console.WriteLine(item.SellingPointID + " ");
            }

            Console.WriteLine("Add new Seller");
            using (var db = new ShawarmaEntities())
             {
                //6. Add new Seller
                string name = "Gandalph";
                string sellingPoint = "Delisious Shawarma";
                int sellingPointID = db.SellingPoints.Where(s => s.ShawarmaTitle == sellingPoint)
                    .Select(s => s.SellingPointID).First();
                 Seller seller = new Seller
                 {
                     SellerName = name,
                     SellingPointID = sellingPointID
                 };

                 Console.WriteLine("Was");
                 foreach (var item in db.Sellers)
                     Console.WriteLine(item.SellerID);

                 db.Sellers.Add(seller);
                 db.SaveChanges();
             }
             using (var db = new ShawarmaEntities())
             {
                 Console.WriteLine("Now");
                 foreach (var item in db.Sellers)
                     Console.WriteLine(item.SellerID);
             }

             Console.WriteLine("Selling Report for period");
            //7.Selling Report for period
             using (var db = new ShawarmaEntities())
             {
                 DateTime dateBegin = DateTime.Now.AddDays(-15);
                 DateTime dateEnd = DateTime.Now;
                 string shawarmaTitle = "Best Shawarma";
                 int sellingPointID = db.SellingPoints.Where(s => s.ShawarmaTitle == shawarmaTitle)
                     .Select(s => s.SellingPointID).First();

                 var report = from sellingPoint in db.SellingPoints
                             join seller in db.Sellers
                             on sellingPoint.SellingPointID equals seller.SellingPointID
                             join orderHeader in db.OrderHeaders
                             on seller.SellerID equals orderHeader.SellerID
                             join orderDetail in db.OrderDetails
                             on orderHeader.OrderHeaderID equals orderDetail.OrderHeaderID
                             join shawarma in db.Shawarmas
                             on orderDetail.ShawarmaID equals shawarma.ShawarmaID
                             join priceController in db.PriceControllers
                             on sellingPoint.SellingPointID equals priceController.SellingPointID
                             where sellingPoint.SellingPointID == sellingPointID
                             && priceController.ShawarmaID == shawarma.ShawarmaID
                             && orderHeader.OrderDate < dateEnd && orderHeader.OrderDate > dateBegin
                             select new
                             {
                                 sellingPoint = sellingPoint.ShawarmaTitle,
                                 shawarmaID = orderDetail.ShawarmaID,
                                 shawarmaName = shawarma.ShawarmaName,
                                 totalPrice = priceController.Price * orderDetail.Quantity
                             };

                var groupedReport = from rep in report
                                    group rep by rep.shawarmaID into gr
                                    select new
                                    {
                                        gr.Key,
                                        totalPrice = gr.Sum(a => a.totalPrice),
                                        nameShawarma = gr.Select(n => n.shawarmaName).FirstOrDefault(),
                                        sellingPoint = gr.Select(sp => sp.sellingPoint).FirstOrDefault()
                                    };
                Console.WriteLine($"from {dateBegin} to {dateEnd}");
                 foreach (var item in groupedReport)
                     Console.WriteLine($" Selling Point {item.sellingPoint} Shawarma {item.nameShawarma} Total Price {item.totalPrice}");

             }

            Console.WriteLine("Seller report");
            //8. Seller report
             using (var db = new ShawarmaEntities())
             {
                 DateTime dateBegin = DateTime.Now.AddDays(-10).Date;
                 DateTime dateEnd = DateTime.Now.Date;
                 TimeSpan difference = dateEnd - dateBegin;
                 int differenceInDays = difference.Days;

                var sellerReport = from seller in db.Sellers
                                   join timeController in db.TimeControllers
                                   on seller.SellerID equals timeController.SellerID
                                   join orderHeader in db.OrderHeaders
                                   on seller.SellerID equals orderHeader.SellerID
                                   join orderDetail in db.OrderDetails
                                   on orderHeader.OrderHeaderID equals orderDetail.OrderHeaderID
                                   join shawarma in db.Shawarmas
                                   on orderDetail.ShawarmaID equals shawarma.ShawarmaID
                                   where orderHeader.OrderDate < dateEnd && orderHeader.OrderDate > dateBegin
                                   select new
                                   {
                                       selId = seller.SellerID,
                                       selName = seller.SellerName,
                                       workingTime = ((timeController.WorkEnd.Hour * 60 + timeController.WorkEnd.Minute)
                                       - (timeController.WorkStart.Hour * 60 + timeController.WorkEnd.Minute)),
                                       cookingTime = shawarma.CookingTime * orderDetail.Quantity,
                                   } into rep
                                   group rep by rep.selId
                                    into grouprep
                                   select new
                                   {
                                       id = grouprep.Key,
                                       sellerName = grouprep.Select(n => n.selName).FirstOrDefault(),
                                       cookingTime = grouprep.Sum(t => t.cookingTime),
                                       workingTime = grouprep.Select(w => w.workingTime).FirstOrDefault()*differenceInDays,
                                       totalTime = grouprep.Sum(t => t.cookingTime)+ grouprep.Select(w => w.workingTime).FirstOrDefault() * differenceInDays
                                   };
                Console.WriteLine($"from {dateBegin} to {dateEnd}");
                                      foreach (var item in sellerReport)
                                           Console.WriteLine($"{item.id} Seller {item.sellerName} Total time {item.totalTime} Cooking time {item.cookingTime} Working time {item.workingTime}");
             }
                Console.ReadKey();
        }
    }
}
