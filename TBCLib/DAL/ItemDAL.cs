using System.Xml.XPath;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using MySqlConnector;
using Model;
using System.Data;



namespace DAL
{
    public static class ItemFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_Item_NAME = 1;
    }
    public class ItemDAL
    {
        private string query;
        private MySqlConnection connection = DBConfig.GetConnection();

        public Item GetItemById(int itemId)
        {
            Item item = null;
            try
            {
                query = @"select Item_id,Item_name,Quantity,Item_status,Item_Price
                        
                        from Items where Item_id=@ItemId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", itemId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    item = GetItem(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { }

            return item;
        }
        internal Item GetItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemId = reader.GetInt32("Item_id");
            item.ItemName = reader.GetString("Item_name");
            item.Quantity = reader.GetInt32("quantity");
            item.ItemStatus = reader.GetString("Item_status");
            item.Price = reader.GetDecimal("Price");          
            return item;
        }
        public List<Item> GetItems(int itemFilter, Item item)
        {
            List<Item> lst = null;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("", connection);
                switch (itemFilter)
                {
                    case ItemFilter.GET_ALL:
                        query = @"select Item_id,Item_name,Quantity,Item_status,Price '') from Items";
                        break;
                    case ItemFilter.FILTER_BY_Item_NAME:
                        query = @"select Item_id,Item_name,Quantity,Item_status,Price '') from Items
                                where Item_name like concat('%',@ItemName,'%');";
                        command.Parameters.AddWithValue("@ItemName", item.ItemName);
                        break;
                }
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                lst = new List<Item>();
                while (reader.Read())
                {
                    lst.Add(GetItem(reader));
                }
                reader.Close();
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return lst;
        }
    }
}