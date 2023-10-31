using System.Data.SqlClient;

namespace CrudUsingADO.Models
{
    public class CourseDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public CourseDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConection"));
        }


        public List<Course> GetCourses()
        {
            List<Course> courses = new List <Course>();
            string qry = "select * from Course";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Course course = new Course();
                    course.id = Convert.ToInt32(dr["id"]);
                    course.coursename = dr["coursename"].ToString();
                    course.duration = dr["duration"].ToString();
                    course.fees = Convert.ToInt32(dr["fees"]);


                    courses.Add(course);
                }
            }
            con.Close();
            return courses;
        }
        public Course GetCourseByid(int id)
        {
            Course course = new Course();
            string qry = "select * from Course where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    course.id = Convert.ToInt32(dr["id"]);
                    course.coursename = dr["coursename"].ToString();
                    course.duration = dr["duration"].ToString();
                    course.fees= Convert.ToInt32(dr["fees"]);


                }
            }
            con.Close();
            return course;
        }
        public int AddCourse(Course course)
        {
            int result = 0;
            string qry = "insert into Course values(@coursename,@duration,@fees)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@coursename", course.coursename);
            cmd.Parameters.AddWithValue("@duration", course.duration);
            cmd.Parameters.AddWithValue("@fees", course.fees);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateStudent(Course course)
        {
            int result = 0;
            string qry = "update Course set coursename=@coursename,duration=@duration,fees=@fees where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@coursename", course.coursename);
            cmd.Parameters.AddWithValue("@duration", course.duration);
            cmd.Parameters.AddWithValue("@fees", course.fees);
            cmd.Parameters.AddWithValue("@id", course.id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteCourse(int id)
        {
            int result = 0;
            string qry = "delete from Course where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
