using CrudUsingADO.Models;
using System.Data.SqlClient;

namespace CrudUsingADO.Models
{
    public class ProductDAL
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConection"));
        }

        public IEnumerable<Category> GetCategory()
        {
            List<Category> categories = new List<Category>();
            string qry = "select * from Category";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Category d = new Category();
                    d.cid = Convert.ToInt32(dr["cid"]);
                    d.cname = dr["cname"].ToString();
                    categories.Add(d);
                }
            }
            con.Close();
            return categories;
        }

        public IEnumerable<Product> GetProduct()
        {
            List<Product> products = new List<Product>();
            string qry = "select p.*,c.cname from Product p inner join Category c on p.cid=c.cid where e.isActive=1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product product = new Product();
                    product.pid = Convert.ToInt32(dr["pid"]);
                    product.pname = dr["pname"].ToString();
                    product.ImageUrl = dr["imageurl"].ToString();
                    product.price = Convert.ToInt32(dr["price"]);
                    product.IsActive = Convert.ToInt32(dr["isActive"]);
                    product.cid = Convert.ToInt32(dr["cid"]);
                    product.cname = dr["cname"].ToString();
                    products.Add(product);
                }
            }
            con.Close();
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            string qry = "select p.*,c.cname from Product p inner join Category c on c.cid=p.cid where p.cid=@pid and p.isActive=1";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    product.pid = Convert.ToInt32(dr["pid"]);
                    product.pname = dr["pname"].ToString();
                    product.ImageUrl = dr["imageurl"].ToString();
                    product.price = Convert.ToInt32(dr["price"]);
                    product.cid = Convert.ToInt32(dr["cid"]);

                }
            }
            con.Close();
            return product;
        }

        public int AddProducts(Product product)
        {
            product.IsActive = 1;
            int result = 0;
            string qry = "insert into Product values(@pname,@price,@cid,@imageurl,@isActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@pname", product.pname);
            cmd.Parameters.AddWithValue("@price", product.price);
            cmd.Parameters.AddWithValue("@cid", product.cid);
            cmd.Parameters.AddWithValue("@imageurl", product.ImageUrl);
            cmd.Parameters.AddWithValue("@isActive", product.IsActive);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateProduct(Product product)
        {
            product.IsActive = 1;
            int result = 0;
            string qry = "update Product set pname=@pname,price=@price,cid=@cid,imageurl=@imageurl where pid=@pid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@pname", product.pname);
            cmd.Parameters.AddWithValue("@price", product.price);
            cmd.Parameters.AddWithValue("@cid", product.cid);
            cmd.Parameters.AddWithValue("@imageurl", product.ImageUrl);
            cmd.Parameters.AddWithValue("@id", product.pid);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        // soft delete 
        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "update Product set isActive=0 where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}