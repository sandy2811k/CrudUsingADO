using System.Data.SqlClient;

namespace CrudUsingADO.Models
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public StudentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConection"));
        }


        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            string qry = "select * from Student";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Student student = new Student();
                    student.RollNo= Convert.ToInt32(dr["rollno"]);
                    student.Name = dr["name"].ToString();
                    student.city = dr["city"].ToString();
                    student.percentage = Convert.ToInt32(dr["percentage"]);

                    students.Add(student);
                }
            }
            con.Close();
            return students;
        }
        public Student GetStudentByRollNo(int rollno)
        {
            Student student = new Student();
            string qry = "select * from Student where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", rollno);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    student.RollNo = Convert.ToInt32(dr["rollno"]);
                    student.Name = dr["name"].ToString();
                    student.city = dr["city"].ToString();
                    student.percentage = Convert.ToInt32(dr["percentage"]);


                }
            }
            con.Close();
            return student;
        }
        public int AddStudent(Student student)
        {
            int result = 0;
            string qry = "insert into Student values(@name,@city,@percentage)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@city", student.city);
            cmd.Parameters.AddWithValue("@percentage", student.percentage);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateStudent(Student student)
        {
            int result = 0;
            string qry = "update Student set name=@name,city=@city,percentage=@percentage where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@city",  student.city);
            cmd.Parameters.AddWithValue("@percentage", student.percentage);
            cmd.Parameters.AddWithValue("@rollno", student.RollNo);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteStudent(int rollno)
        {
            int result = 0;
            string qry = "delete from Student where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", rollno);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
