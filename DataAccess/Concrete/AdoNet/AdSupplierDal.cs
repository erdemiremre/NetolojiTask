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
    public class AdSupplierDal : Context, ISupplierDal
    {
        public void Add(Supplier entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {

                SqlCommand cmd = new SqlCommand("gp_SupplierAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", entity.ContactName);
                cmd.Parameters.AddWithValue("@Phone", entity.Phone);
                cmd.Parameters.AddWithValue("@ContactTitle", entity.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", entity.Address);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Delete(Supplier entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_SupplierDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@SupplierId", entity.SupplierId);
                cmd.ExecuteNonQuery();

            }
        }

        public Supplier Get(int id)
        {
            Supplier supplier = new Supplier();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_GetByIdSupplier", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierId", id);

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    supplier.SupplierId = Convert.ToInt32(dr[0]);
                    supplier.CompanyName = dr[1].ToString();
                    supplier.ContactName = dr[2].ToString();
                    supplier.Phone = dr[3].ToString();
                    supplier.ContactTitle = dr[4].ToString();
                    supplier.Address = dr[5].ToString();
                }

            }
            return supplier;
        }

        public List<Supplier> GetAll(Expression<Func<Supplier, bool>> filter = null)
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                SqlCommand cmd = new SqlCommand("gp_GetAllSupplier", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Supplier supplier = new Supplier
                    {
                        SupplierId = Convert.ToInt32(dr[0]),
                        CompanyName = (dr[1]).ToString(),
                        ContactName = (dr[2]).ToString(),
                        Phone = (dr[3]).ToString(),
                        ContactTitle = (dr[4]).ToString(),
                        Address = (dr[5]).ToString(),
                    };
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }

        public void Update(Supplier entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_SupplierUpdate ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierId", entity.SupplierId);
                cmd.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", entity.ContactTitle);
                cmd.Parameters.AddWithValue("@Phone", entity.Phone);
                cmd.Parameters.AddWithValue("@ContactTitle", entity.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", entity.Address);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
