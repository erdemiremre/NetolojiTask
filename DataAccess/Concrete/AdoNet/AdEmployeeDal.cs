using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.AdoNet
{
    public class AdEmployeeDal : Context, IEmployeeDal
    {
        public void Add(Employee entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {

                SqlCommand cmd = new SqlCommand("gp_EmployeeAdd ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
                cmd.Parameters.AddWithValue("@LastName", entity.LastName);
                cmd.Parameters.AddWithValue("@Title", entity.Title);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Delete(Employee entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_EmployeeDelete ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId);
                cmd.ExecuteNonQuery();

            }
        }

        public Employee Get(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_GetByIdEmployee ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    employee.EmployeeId = Convert.ToInt32(dr[0]);
                    employee.FirstName = dr[1].ToString();
                    employee.LastName = dr[2].ToString();
                    employee.Title = dr[3].ToString();
                }

            }
            return employee;
        }

        public List<Employee> GetAll(Expression<Func<Employee, bool>> filter = null)
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                SqlCommand cmd = new SqlCommand("gp_GetAllEmployee", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = Convert.ToInt32(dr[0]),
                        FirstName = (dr[1]).ToString(),
                        LastName = (dr[2]).ToString(),
                        Title = (dr[3]).ToString()
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public void Update(Employee entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_EmployeeUpdate  ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId);
                cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
                cmd.Parameters.AddWithValue("@LastName", entity.LastName);
                cmd.Parameters.AddWithValue("@Title", entity.Title);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
