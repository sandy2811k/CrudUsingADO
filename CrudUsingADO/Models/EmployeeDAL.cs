using CrudUsingADO.Models;
using System.Data.SqlClient;
public class EmployeeDAL
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader dr;
    IConfiguration configuration;
    public EmployeeDAL(IConfiguration configuration)
    {
        this.configuration = configuration;
        con = new SqlConnection(this.configuration.GetConnectionString("defaultConection"));
    }

    public IEnumerable<Dept> GetDepts()
    {
        List<Dept> depts = new List<Dept>();
        string qry = "select * from Dept ";
        cmd = new SqlCommand(qry, con);
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                Dept d = new Dept();
                d.Did = Convert.ToInt32(dr["did"]);
                d.Dname = dr["dname"].ToString();
                depts.Add(d);
            }
        }
        con.Close();
        return depts;
    }

    public IEnumerable<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>();
        string qry = "select e.*,d.dname from Employee e inner join Dept d on d.did=e.did where e.isActive=1";
        cmd = new SqlCommand(qry, con);
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.Id = Convert.ToInt32(dr["id"]);
                emp.Name = dr["name"].ToString();
                emp.Email = dr["email"].ToString();
                emp.Age = Convert.ToInt32(dr["age"]);
                emp.ImageUrl = dr["imageurl"].ToString();
                emp.Salary = Convert.ToInt32(dr["salary"]);
                emp.IsActive = Convert.ToInt32(dr["isActive"]);
                emp.Did = Convert.ToInt32(dr["did"]);
                emp.Dname = dr["dname"].ToString();
                employees.Add(emp);
            }
        }
        con.Close();
        return employees;
    }

    public Employee GetEmployeeById(int id)
    {
        Employee emp = new Employee();
        string qry = "select e.*,d.dname from Employee e inner join Dept d on d.did=e.did where e.id=@id and e.isActive=1";
        cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@id", id);
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                emp.Id = Convert.ToInt32(dr["id"]);
                emp.Name = dr["name"].ToString();
                emp.Email = dr["email"].ToString();
                emp.Age = Convert.ToInt32(dr["age"]);
                emp.ImageUrl = dr["imageurl"].ToString();
                emp.Salary = Convert.ToInt32(dr["salary"]);
                emp.Did = Convert.ToInt32(dr["did"]);

            }
        }
        con.Close();
        return emp;
    }


    public int AddEmployee(Employee emp)
    {
        emp.IsActive = 1;
        int result = 0;
        string qry = "insert into Employee values(@name,@email,@age,@salary,@did,@imageurl,@isActive)";
        cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@name", emp.Name);
        cmd.Parameters.AddWithValue("@email", emp.Email);
        cmd.Parameters.AddWithValue("@age", emp.Age);
        cmd.Parameters.AddWithValue("@salary", emp.Salary);
        cmd.Parameters.AddWithValue("@did", emp.Did);
        cmd.Parameters.AddWithValue("@imageurl", emp.ImageUrl);
        cmd.Parameters.AddWithValue("@isActive", emp.IsActive);

        con.Open();
        result = cmd.ExecuteNonQuery();
        con.Close();
        return result;
    }
    public int UpdateEmployee(Employee emp)
    {
        emp.IsActive = 1;
        int result = 0;
        string qry = "update Employee set name=@name,email=@email,age=@age,salary=@salary,did=@did,imageurl=@imageurl,@isActive=isActive where id=@id";
        cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@name", emp.Name);
        cmd.Parameters.AddWithValue("@email", emp.Email);
        cmd.Parameters.AddWithValue("@age", emp.Age);
        cmd.Parameters.AddWithValue("@salary", emp.Salary);
        cmd.Parameters.AddWithValue("@did", emp.Did);
        cmd.Parameters.AddWithValue("@imageurl", emp.ImageUrl);
        cmd.Parameters.AddWithValue("@id", emp.Id);
        cmd.Parameters.AddWithValue("@isActive", emp.IsActive);
        con.Open();
        result = cmd.ExecuteNonQuery();
        con.Close();
        return result;

    }
    // soft delete 
    public int DeleteEmployee(int id)
    {
        int result = 0;
        string qry = "update Employee set isActive=0 where id=@id";
        cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@id", id);
        con.Open();
        result = cmd.ExecuteNonQuery();
        con.Close();
        return result;

    }
}
