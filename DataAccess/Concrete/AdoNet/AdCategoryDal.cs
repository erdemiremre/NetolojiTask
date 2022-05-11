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
    public class AdCategoryDal : Context, ICategoryDal
    {
        public void Add(Category entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {

                SqlCommand cmd = new SqlCommand("gp_CategoryAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
                cmd.Parameters.AddWithValue("@Description", entity.Description);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Delete(Category entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_CategoryDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                cmd.ExecuteNonQuery();

            }
        }

        public Category Get(int id)
        {
            Category category = new Category();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_GetByIdCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", id);
               
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    category.CategoryId = Convert.ToInt32(dr[0]);
                    category.CategoryName = dr[1].ToString();
                    category.Description = dr[2].ToString();
                }
                
            }
            return category;
        }

        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                SqlCommand cmd = new SqlCommand("gp_GetAllCategory", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Category category = new Category
                    {
                        CategoryId = Convert.ToInt32(dr[0]),
                        CategoryName = (dr[1]).ToString(),
                        Description = (dr[2]).ToString()
                    };
                    categories.Add(category);
                }
            }
            return categories;
        }

        public void Update(Category entity)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_CategoryUpdate ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                cmd.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
                cmd.Parameters.AddWithValue("@Description", entity.Description);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
