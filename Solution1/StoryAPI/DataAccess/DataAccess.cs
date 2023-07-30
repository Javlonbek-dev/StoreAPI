using StoryAPI.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace StoryAPI.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration configuration;
        private readonly string dbconnection;
        private readonly string dateformat;
        public DataAccess(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DB"];
            dateformat = this.configuration["Constants:DateFormat"];
        }

        public List<ProductCategory> GetProductCategories()
        {
            var productCategories = new List<ProductCategory>();
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand cmd = new()
                {
                    Connection = connection,
                };

                string query = "SELECT * FROM ProductCategories;";
                cmd.CommandText = query;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var category = new ProductCategory()
                    {
                        Id = (int)reader["CategoryId"],
                        Category = (string)reader["Category"],
                        Subcategory = (string)reader["SubCategory"]
                    };
                    productCategories.Add(category);
                }
            }

            return productCategories;
        }

        public ProductCategory GetProductCategory(int id)
        {
                var productCategory = new ProductCategory();
                using (SqlConnection connection = new(dbconnection))
                {
                    SqlCommand command = new()
                    {
                        Connection = connection
                    };

                    string query = "SELECT * FROM ProductCategories WHERE CategoryId=" + id + ";";
                    command.CommandText = query;

                    connection.Open();
                    SqlDataReader r = command.ExecuteReader();
                    while (r.Read())
                    {
                        productCategory.Id = (int)r["CategoryId"];
                        productCategory.Category = (string)r["Category"];
                        productCategory.Subcategory = (string)r["Subcategory"];
                    }

                }
                return productCategory;
            }
    }
}
