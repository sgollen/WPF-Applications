using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Date: Nov 20/18
namespace PaySorter
{
    // define connection string
    public class DB
    {
        const string CONNECT_STRING = "INSERT A CORRECT CONNECTION STRING. CHNAGED DUE TO SECURITY PURPOSES";
        SqlConnection conn;

        static DB _db;

        // Open DB connection using string defined above
        private DB()
        {
            conn = new SqlConnection(CONNECT_STRING);
            conn.Open();
        }

        // get specific instance of DB
        public static DB GetInstance()
        {
            if (_db == null)
                _db = new DB();
            return _db;
        }

        //identify next ID to select for new employees
        public int GetNextId()
        {
            var id = 0;
            var cmdString = "SELECT MAX(EmployeeID) FROM Employee";
            var cmd = new SqlCommand(cmdString, conn);
            var o = cmd.ExecuteScalar();
            id = int.Parse((string)o);

            return id + 1;
        }

        // Method returns list, with option to sort based on input
        public List<Employee> Get(int sortType)
        {
            var ps = new List<Employee>();
            var cmdString = "SELECT EmployeeID, Name, Position, HourlyPayRate" +
                               " FROM Employee";
            var cmd = new SqlCommand(cmdString, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ps.Add(new Employee()
                {
                    EmployeeID = int.Parse(rd.GetString(rd.GetOrdinal("EmployeeID"))),
                    Name = rd.GetString(rd.GetOrdinal("Name")),
                    Position = rd.GetString(rd.GetOrdinal("Position")),
                    HourlyPayRate = int.Parse(rd.GetString(rd.GetOrdinal("HourlyPayRate"))),
                });
            }

            rd.Close();

            if (sortType == 0)
            {
               ps.Sort(new EmployeeSortDesPayRate());
            }
            else if (sortType == 1)
            {
               ps.Sort(new EmployeeSortAscPayRate());
            }

            return ps;
        }

        public void Add(Employee e)
        {
            if (e.EmployeeID == 0)
                e.EmployeeID = GetNextId();

            var cmdString = "INSERT INTO Employee " +
                                    "(EmployeeId, Name, Position, HourlyPayRate)" +
                                    "VALUES" +
                                    "(@EMPLOYEEID, @NAME, @POSITION, @HOURLYPAYRATE)";

            var cmd = new SqlCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@EMPLOYEEID", e.EmployeeID);
            cmd.Parameters.AddWithValue("@NAME", e.Name);
            cmd.Parameters.AddWithValue("@POSITION", e.Position);
            cmd.Parameters.AddWithValue("@HOURLYPAYRATE", e.HourlyPayRate);

            cmd.ExecuteNonQuery();
        }
    }
}
