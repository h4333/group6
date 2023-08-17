using System;
using BL;
using Model;
using OrderManagementApp.BLL;
using OrderManagementApp.DAL;
using Spectre.Console;
internal class Program
{
    private static void Main(string[] args)
    {
        short mainChoose = 0, imChoose;
        string[] mainMenu = { "Search Menu", "Create Order", "Exit" };
        string[] imMenu = { "Get By Product Id", "Get All Product", "Search By Product Name", "Exit" };
        ItemBL ibl = new ItemBL();
        StaffBL sta = new StaffBL();
        string[] Login = { "login" };
        List<Item> lst = new List<Item>();
        do
        {
            StaffBL staffBL = new StaffBL();
            Staff? orderStaff;
            bool active = true;
            while (active = true)
            {
                string UserName;
                Console.WriteLine(@"            
                                            ▄▄             
▀████▀                      ██             
  ██                                       
  ██       ▄██▀██▄ ▄█▀████████ ▀████████▄  
  ██      ██▀   ▀████  ██   ██   ██    ██  
  ██     ▄██     ███████▀   ██   ██    ██  
  ██    ▄███▄   ▄███        ██   ██    ██  
██████████ ▀█████▀ ███████▄████▄████  ████▄
                  █▀     ██                
                  ██████▀                  



                    ");
                Console.Write("User Name : ");
                UserName = Console.ReadLine() ?? "";
                if (UserName == "0")
                {
                    active = false;
                    break;
                }
                else
                {
                    orderStaff = staffBL.Login(UserName);
                }
                Console.Clear();
                if (orderStaff != null)
                {

                    while (true)
                    {
                        mainChoose = Menu("                         Order Management", mainMenu);
                        switch (mainChoose)
                        {
                            case 1:
                                do
                                {

                                    imChoose = Menu("                       Product Management", imMenu);
                                    switch (imChoose)
                                    {
                                        case 1:
                                            Console.Write("\nInput Product Id: ");
                                            int itemId;
                                            if (int.TryParse(Console.ReadLine(), out itemId))
                                            {
                                                Item i = ibl.GetItemById(itemId);
                                                if (i != null)
                                                {

                                                    Console.WriteLine("Product Name: " + i.ItemName);                                                   
                                                    Console.WriteLine("Quanity: " + i.Quantity);
                                                    Console.WriteLine("Product Status: " + i.ItemStatus);
                                                    Console.Write("Item Price: " + i.Price );
                                                    Console.WriteLine(" VND");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("There is no Item with id " + itemId);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Your Choose is wrong!");
                                            }
                                            Console.WriteLine("\n    Press Enter key to back menu...");
                                            Console.ReadLine();
                                            break;
                                        case 2:

                                            lst = ibl.GetAll();
                                            Console.WriteLine("\nItem Count: " + lst.Count());
                                            Console.ReadKey();
                                            break;
                                        case 3:
                                            lst = ibl.GetByName("I");
                                            Console.WriteLine("\nItem Count By Name: " + lst.Count());
                                            break;

                                    }
                                } while (imChoose != imMenu.Length);
                                break;
                            case 2:

                                string connectionString = "server=localhost;user id=root;password=ciandideZ_5;port=3306;database=OrderDB;IgnoreCommandTransaction=true;"; // Thay bằng chuỗi kết nối thực tế của bạn
                                DataAccess dataAccess = new DataAccess(connectionString);
                                OrderManager orderManager = new OrderManager(dataAccess);
                                Console.WriteLine("---- Create New Order ----");

                                List<Customer> customers = orderManager.GetAllCustomers();
                                Console.WriteLine("Customers:");
                                foreach (var customer in customers)
                                {
                                    Console.WriteLine($"{customer.CustomerId}. {customer.CustomerName}");
                                }
                                Console.Write("Select Customer (ID): ");
                                int selectedCustomerId = int.Parse(Console.ReadLine());

                                List<Item> items = orderManager.GetAllItems();
                                Console.WriteLine("Drinks Menu:");
                                foreach (var item in items)
                                {
                                    Console.WriteLine($"{item.ItemId}. {item.ItemName}");
                                }
                                Console.Write("Select Product (ID): ");
                                int selectedItemId = int.Parse(Console.ReadLine());

                                Console.Write("Quantity: ");
                                int quantity = int.Parse(Console.ReadLine());

                                Order newOrder = new Order
                                {
                                    CustomerId = selectedCustomerId,
                                    OrderDate = DateTime.Now,
                                    OrderStatus = "Pending"
                                };

                                OrderDetail newOrderDetail = new OrderDetail
                                {
                                    ItemId = selectedItemId,
                                    Quantity = quantity
                                };

                                newOrder.OrderDetails = new List<OrderDetail> { newOrderDetail };

                                orderManager.AddOrder(newOrder);

                                Console.WriteLine("Order created successfully!");
                                Console.ReadKey();
                                Console.Clear();
                                var table = new Table();
                                table.AddColumn("Order Id ");
                                table.AddColumn("Customer ID ");
                                table.AddColumn("Date ");
                                table.AddColumn("Status ");
                                table.AddColumn("Product ");
                                table.AddColumn("Quantity ");
                                foreach (var orderDetail in newOrder.OrderDetails)
                                {
                                    table.AddRow(newOrder.OrderId.ToString(), newOrder.CustomerId.ToString(), newOrder.OrderDate.ToString(), newOrder.OrderStatus, orderDetail.ItemId.ToString(), orderDetail.Quantity.ToString());
                                }

                                AnsiConsole.Render(table);


                                Console.ReadKey();
                                break;
                            case 3:
                                Console.ReadKey();
                                break;

                        }

                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid Username or Password !");
                }

            }
        } while (mainChoose != mainMenu.Length);

        short Menu(string title, string[] menuItems)
        {
            string logo = @"==========================================================================================



                       ▄▄▄▄   ▄▄▄▄                           ▄▄                           
  ▄▄█▀▀▀█▄█          ▄█▀ ▀▀ ▄█▀ ▀▀                          ███                           
▄██▀     ▀█          ██▀    ██▀                              ██                           
██▀       ▀ ▄██▀██▄ █████  █████   ▄▄█▀██  ▄▄█▀██     ▄██▀██████████▄   ▄██▀██▄▀████████▄ 
██         ██▀   ▀██ ██     ██    ▄█▀   ██▄█▀   ██    ██   ▀▀██    ██  ██▀   ▀██ ██   ▀██ 
██▄        ██     ██ ██     ██    ██▀▀▀▀▀▀██▀▀▀▀▀▀    ▀█████▄██    ██  ██     ██ ██    ██ 
▀██▄     ▄▀██▄   ▄██ ██     ██    ██▄    ▄██▄    ▄    █▄   ████    ██  ██▄   ▄██ ██   ▄██ 
  ▀▀█████▀  ▀█████▀▄████▄ ▄████▄   ▀█████▀ ▀█████▀    ██████▀███  ████▄ ▀█████▀  ██████▀  
                                                                                 ██       
                                                                               ▄████▄     

  
                                        
                                       ";
            short choose = 0;
            Console.WriteLine($"\n" + logo);
            string line = "==========================================================================================";
            Console.WriteLine(line);
            Console.WriteLine(" " + title);
            Console.WriteLine(line);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine(line);
            do
            {
                Console.Write("Your choice: ");
                try
                {
                    choose = short.Parse(Console.ReadLine() ?? "");
                }
                catch
                {
                    Console.WriteLine("Your Choose is wrong!");
                    continue;
                }
            } while (choose <= 0 || choose > menuItems.Length);
            return choose;
        }
    }
}