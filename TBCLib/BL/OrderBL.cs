using System;
using System.Collections.Generic;
using MySqlConnector;
using Model;
using OrderManagementApp.DAL;

namespace OrderManagementApp.BLL
{
    public class OrderManager
    {
        private readonly DataAccess dataAccess;

        public OrderManager(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public List<Customer> GetAllCustomers()
        {
            return dataAccess.GetAllCustomers();
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = dataAccess.GetAllOrders();
            

            foreach (var order in orders)
            {
                order.Customer = GetCustomerById(order.CustomerId);

            }

            return orders;
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return dataAccess.GetAllOrderDetails();
        }

        public Customer GetCustomerById(int customerId)
        {
            return dataAccess.GetCustomerById(customerId);
        }

        public Staff GetStaffById(int staffId)
        {
            return dataAccess.GetStaffById(staffId);
        }
        public List<Item> GetAllItems()
        {
            return dataAccess.GetAllItems();
        }
        public void AddOrder(Order order)
        {
            dataAccess.AddOrder(order);
        }
        
    }
}