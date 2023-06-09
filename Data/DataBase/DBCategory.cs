using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shop.Data.Interfaces;
using shop.Data.Models;
using MySql.Data.MySqlClient;
using shop.Data.Common;

namespace shop.Data.DataBase
{
    public class DBCategory : ICategorys
    {
        public IEnumerable<Categorys> AllCategorys
        {
            get
            {
                List<Categorys> categorys = new List<Categorys>();
                MySqlConnection MySqlConnection = Connection.MySqlOpen();
                MySqlDataReader CategorysData = Connection.MySqlQuery("SELECT*FROM Shop.Categorys ORDER BY `Name`;", MySqlConnection);
                while (CategorysData.Read())
                {
                    categorys.Add(new Categorys()
                    {
                        Id = CategorysData.IsDBNull(0) ? -1 : CategorysData.GetInt32(0),
                        Name = CategorysData.IsDBNull(1) ? null : CategorysData.GetString(1),
                        Description = CategorysData.IsDBNull(2) ? null : CategorysData.GetString(2)
                    });
                }
                return categorys;
            }
        }
        public IEnumerable<Categorys> FindCategorys(string searchTerm)
        {
            List<Categorys> categorys = new List<Categorys>();

            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            MySqlDataReader CategorysData = Connection.MySqlQuery($"SELECT * FROM Shop.Categorys WHERE `Name` LIKE '%{searchTerm}%';", MySqlConnection);/* OR `Description` LIKE '%{searchTerm}%'*/

            while (CategorysData.Read())
            {
                categorys.Add(new Categorys()
                {
                    Id = CategorysData.IsDBNull(0) ? -1 : CategorysData.GetInt32(0),
                    Name = CategorysData.IsDBNull(1) ? "" : CategorysData.GetString(1),
                    Description = CategorysData.IsDBNull(2) ? "" : CategorysData.GetString(2),
                });
            }

            MySqlConnection.Close();
            return categorys;

        }
        public int Add(Categorys Category)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery(
                $"INSERT INTO `Categorys`(`Name`,`Description`) VALUES('{Category.Name}','{Category.Description}');",
                MySqlConnection);
            MySqlConnection.Close();
            int IdCategory = -1;
            MySqlConnection = Connection.MySqlOpen();
            MySqlDataReader mySqlDataReaderCategory = Connection.MySqlQuery(
                $"SELECT `Id` FROM `Categorys` WHERE `Name`='{Category.Name}' AND `Description`='{Category.Description}';",
                MySqlConnection);
            if (mySqlDataReaderCategory.HasRows)
            {
                mySqlDataReaderCategory.Read();
                IdCategory = mySqlDataReaderCategory.GetInt32(0);
            }
            MySqlConnection.Close();
            return IdCategory;
        }
        public void Update(Categorys category)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery($@"UPDATE `Categorys` SET
                            `Name` = '{category.Name}',
                            `Description` = '{category.Description}' WHERE `Id` = {category.Id};", MySqlConnection);
            MySqlConnection.Close();
        }
        public Categorys GetCategoryById(int id)
        {
            return AllCategorys.FirstOrDefault(i => i.Id == id);
        }
        public void Delete(Categorys category)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery($"DELETE FROM `Categorys` WHERE `Id` = {category.Id};", MySqlConnection);
            MySqlConnection.Close();
        }
    }
}
