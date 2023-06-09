using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shop.Data.Common;
using shop.Data.Interfaces;
using shop.Data.Models;
using MySql.Data.MySqlClient;

namespace shop.Data.DataBase
{
    public class DBItems :IItems
    {
        
        public IEnumerable<Categorys> Categorys = new DBCategory().AllCategorys;
        public IEnumerable<Items> AllItems
        {
            get
            {
                List<Items> items = new List<Items>();
                MySqlConnection MySqlConnection = Connection.MySqlOpen();
                MySqlDataReader ItemsData = Connection.MySqlQuery("SELECT*FROM Shop.Items ORDER BY `Name`;", MySqlConnection);
                while (ItemsData.Read())
                {
                    items.Add(new Items()
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? "" : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? "" : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? "" : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Category = ItemsData.IsDBNull(5) ? null : Categorys.Where(x => x.Id == ItemsData.GetInt32(5)).First()

                    });
                }
                MySqlConnection.Close();
                return items;
            }
        }
        public int Add(Items Item)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery(
                $"INSERT INTO `Items`(`Name`,`Description`,`Img`,`Price`,`IdCategory`) VALUES('{Item.Name}','{Item.Description}','{Item.Img}',{Item.Price},{Item.Category.Id});",
                MySqlConnection);
            MySqlConnection.Close();
            int IdItem = -1;
            MySqlConnection = Connection.MySqlOpen();
            MySqlDataReader mySqlDataReaderItem = Connection.MySqlQuery(
                $"SELECT `Id` FROM `Items` WHERE `Name`='{Item.Name}' AND `Description`='{Item.Description}'AND `Price`={Item.Price} AND `IdCategory`={Item.Category.Id};",
                MySqlConnection);
            if (mySqlDataReaderItem.HasRows)
            {
                mySqlDataReaderItem.Read();
                IdItem = mySqlDataReaderItem.GetInt32(0);
            }
            MySqlConnection.Close();
            return IdItem;
        }
        public void Update(Items item)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery($@"UPDATE `Items` SET
                            `Name` = '{item.Name}',
                            `Description` = '{item.Description}',
                            `Img` = '{item.Img}',
                            `Price` = {item.Price},
                            `IdCategory` = {item.Category.Id}
                            WHERE `Id` = {item.Id};", MySqlConnection);
            MySqlConnection.Close();
        }
        public Items GetItemById(int id)
        {
            return AllItems.FirstOrDefault(i => i.Id == id);
        }
        public void Delete(Items item)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery($"DELETE FROM `Items` WHERE `Id` = {item.Id};", MySqlConnection);
            MySqlConnection.Close();
        }


    }
}
