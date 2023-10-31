using System.Data.SqlClient;

namespace CrudUsingADO.Models
{
    public class BookDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public BookDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConection"));
        }


        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            string qry = "select * from Book";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dr["id"]);
                    book.Name = dr["name"].ToString();
                    book.Author = dr["author"].ToString();
                    book.Price = Convert.ToInt32(dr["price"]);


                    books.Add(book);
                }
            }
            con.Close();
            return books;
        }
        public Book GetBookById(int id)
        {
            Book book = new Book();
            string qry = "select * from Book where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    book.Id = Convert.ToInt32(dr["id"]);
                    book.Name = dr["name"].ToString();
                    book.Author = dr["author"].ToString();
                    book.Price = Convert.ToInt32(dr["price"]);


                }
            }
            con.Close();
            return book;
        }
        public int AddBook(Book book)
        {
            int result = 0;
            string qry = "insert into Book values(@name,@author,@price)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", book.Name);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@price", book.Price);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateBook(Book book)
        {
            int result = 0;
            string qry = "update Book set name=@name,author=@author,price=@price where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", book.Name);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@price", book.Price);
            cmd.Parameters.AddWithValue("@id", book.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteBook(int id)
        {
            int result = 0;
            string qry = "delete from Book where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }

}