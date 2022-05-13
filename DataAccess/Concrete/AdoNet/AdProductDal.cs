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
    public class AdProductDal : Context, IProductDal
    {
        public void Add(Product entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {

                SqlCommand cmd = new SqlCommand("gp_ProductAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@ProductName", entity.ProductName);
                cmd.Parameters.AddWithValue("@BarkodNo", entity.BarkodNo);
                cmd.Parameters.AddWithValue("@SupplierId", entity.SupplierId);
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Delete(Product entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_ProductDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
                cmd.ExecuteNonQuery();

            }
        }

        public Product Get(int id)
        {
            Product product = new Product();

            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_GetByIdProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    product.ProductId = Convert.ToInt32(dr[0]);
                    product.ProductName = dr[1].ToString();
                    product.BarkodNo = Convert.ToInt16(dr[2]);
                    product.SupplierId = Convert.ToInt32(dr[3]);
                    product.CategoryId = Convert.ToInt32(dr[4]);
                    product.UnitPrice = Convert.ToInt16(dr[5]);
                }

            }
            return product;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(Conn))
            {
                SqlCommand cmd = new SqlCommand("gp_GetAllProduct", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product product = new Product
                    {
                        ProductId = Convert.ToInt32(dr[0]),
                        ProductName = (dr[1]).ToString(),
                        BarkodNo = Convert.ToInt16(dr[2]),
                        SupplierId = Convert.ToInt32(dr[3]),
                        CategoryId = Convert.ToInt32(dr[4]),
                        UnitPrice = Convert.ToInt16(dr[5]),
                    };
                    products.Add(product);
                }
            }
            return products;
        }

        public void Update(Product entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_ProductUpdate ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", entity.ProductName);
                cmd.Parameters.AddWithValue("@BarkodNo", entity.BarkodNo);
                cmd.Parameters.AddWithValue("@SupplierId", entity.SupplierId);
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
