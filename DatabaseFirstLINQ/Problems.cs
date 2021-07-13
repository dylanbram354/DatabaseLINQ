using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;
using System.Collections.Generic;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();

            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var usersCount = _context.Users.ToList().Count;
            Console.WriteLine(usersCount);

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.

            var productsOver150 = _context.Products.Where(p => p.Price > 150);

            foreach (Product product in productsOver150)
            {
                Console.WriteLine(product.Name + ": " + product.Price);
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.

            var productsWithS = _context.Products.Where(p => p.Name.ToUpper().Contains("S"));
            foreach (Product product in productsWithS)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            var usersBefore2016 = _context.Users.Where(u => u.RegistrationDate < new DateTime(2016, 1, 1));
            foreach (User user in usersBefore2016)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.

            var users = _context.Users.Where(u => new DateTime(2016, 1, 1) < u.RegistrationDate && u.RegistrationDate < new DateTime(2018, 1, 1));
            foreach (User user in users)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var productsInCart = _context.ShoppingCarts.Include(entry => entry.Product).Include(entry => entry.User).Where(entry => entry.User.Email == "afton@gmail.com");
            foreach (ShoppingCart shoppingCart in productsInCart)
            {
                Console.WriteLine(shoppingCart.Product.Name + " " + shoppingCart.Product.Price + " " + shoppingCart.Quantity);
            }

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var sumOfPrices = _context.ShoppingCarts.Include(entry => entry.Product).Include(entry => entry.User).Where(entry => entry.User.Email == "oda@gmail.com")
                .Select(sc => sc.Product.Price).Sum();

            Console.WriteLine(sumOfPrices);

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var employeeUserIds = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Employee").Select(ur => ur.User.Id).ToList();
            var employeeCartsWithInfo = _context.ShoppingCarts.Include(e => e.User).Include(e => e.Product).Where(e => employeeUserIds.Contains(e.UserId));

            foreach(ShoppingCart item in employeeCartsWithInfo)
            {
                Console.WriteLine($"{item.User.Email} - {item.Product.Name} - {item.Product.Price} - {item.Quantity}");
            }

        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123",

            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Tool Chest",
                Description = "craftsman tool Box",
                Price = 15,
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Tool Chest").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();

            ShoppingCart newSelection = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1,
            };

            _context.ShoppingCarts.Add(newSelection);
            _context.SaveChanges();

        }
        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var myProduct = _context.Products.Where(p => p.Name == "Tool Chest").SingleOrDefault();
            myProduct.Price = 1000;
            _context.Products.Update(myProduct);
            _context.SaveChanges();

            foreach (Product product in _context.Products)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }

        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Include(user => user.User).Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Include(user => user.User).Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);   
            _context.SaveChanges();         

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.

            var oda = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(oda);
            _context.SaveChanges();
            foreach (User user in _context.Users)
            {
                Console.WriteLine(user.Email);
            }

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine($"Please enter your email");
            string userEmail = Console.ReadLine();
            Console.WriteLine($"Please enter a password");
            string userPw = Console.ReadLine();
            
            var checkUser = _context.Users.Where(user => user.Email == userEmail).Where(pw => pw.Password == userPw).Any();
            if (checkUser)
            {
                Console.WriteLine($"Signed In");
                
            }
            else
            {
                Console.WriteLine($"Invalid Email or Password");
                
            }
            
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.

            List<int> userIds = _context.Users.Select(u => u.Id).ToList();
            decimal? grandTotal = 0;
            foreach (int id in userIds)
            {
                var cart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Id == id).Select(sc => new { sc.Product.Price, sc.Quantity });
                decimal? total = 0;
                foreach (var item in cart)
                {
                    total += item.Price*item.Quantity;
                }
                grandTotal += total;
                Console.WriteLine($"User {id} total: {total}");
            }
            Console.WriteLine($"Grand total of all carts: {grandTotal}");
        }

        // BIG ONE
            void AddProduct()
            {
                List<int> allProductIds = _context.Products.Select(p => p.Id).ToList();
                bool validEntry = false;
                int id = 0;
                while (!validEntry) {
                    Console.WriteLine("Enter the ID number of the product you wish to add.");
                    string selectedId = Console.ReadLine();
                    try
                    {
                        id = Int32.Parse(selectedId);
                        if (allProductIds.Contains(id))
                        {
                            validEntry = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID");
                        }
                        
                    }
                    catch
                    {
                        Console.WriteLine("Invalid entry");
                    }
                }
                var cart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Id == user.Id);
                List<int> cartProductIds = cart.Select(sc => sc.Product.Id).ToList();
                if (cartProductIds.Contains(id))
                {
                    var cartItem = _context.ShoppingCarts.Where(sc => sc.ProductId == id && sc.UserId == user.Id).SingleOrDefault();
                    cartItem.Quantity += 1;
                    _context.ShoppingCarts.Update(cartItem);
                    _context.SaveChanges();
                }
                else
                {
                    ShoppingCart newCartItem = new ShoppingCart()
                    {
                        UserId = user.Id,
                        ProductId = id,
                        Quantity = 1
                    };
                    _context.ShoppingCarts.Add(newCartItem);
                    _context.SaveChanges();
                }
                Console.WriteLine($"Product added.");
                ViewCart();
            }

            void DeleteProduct()
            {
                List<int> cartIds = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Id == user.Id).Select(sc => sc.Product.Id).ToList();
                bool validEntry = false;
                int id = 0;
                while (!validEntry) {
                    Console.WriteLine("Enter the ID number of the product you wish to delete.");
                    string selectedId = Console.ReadLine();
                    try
                    {
                        id = Int32.Parse(selectedId);
                        if (cartIds.Contains(id))
                        {
                            validEntry = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID");
                        }
                        
                    }
                    catch
                    {
                        Console.WriteLine("Invalid entry");
                    }
                }
                var cart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Id == user.Id);
                List<int> cartProductIds = cart.Select(sc => sc.Product.Id).ToList();
                if (cartProductIds.Contains(id))
                {
                    var cartItem = _context.ShoppingCarts.Where(sc => sc.ProductId == id && sc.UserId == user.Id).SingleOrDefault();
                    _context.ShoppingCarts.Remove(cartItem);
                    _context.SaveChanges();
                     Console.WriteLine($"Product Deleted.");
                }
                else
                {
                   Console.WriteLine($"No Item found");
                   
                }
                ViewCart();
            }
            while (!validLogin)
            {
                SignIn(); 
            }
            while (validLogin)
            {
                MainMenu();
            }
        }

    }
}
