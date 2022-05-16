using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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


        // 	Son kullanma tarihinin dolmasına 1 ay kalan ürünleri veren Method
        public List<ProductDto> GetProductsWithOneMonth()
        {
            List<ProductDto> productDtos = new List<ProductDto>();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_GetProductsWithOneMonth ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductDto productDto = new ProductDto
                    {
                        ProductId = Convert.ToInt32(dr[0]),
                        ProductName = (dr[1]).ToString(),
                        CompanyName = (dr[2]).ToString(),
                        UnitPrice = Convert.ToInt16(dr[3]),
                        ExprationDate=Convert.ToDateTime(dr[4])
                    };
                    productDtos.Add(productDto);
                }
            }
            return productDtos;

        }

        //  Raf ömrünün dolmasına 1 hafta kalmış olan ürünleri listeleyen method
        public List<ProductDetailDto> GetProductsWithShelfLifeOfOneWeek()
        {
            List<ProductDetailDto> productDetailDtos = new List<ProductDetailDto>();
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_GetProductsWithShelfLifeOfOneWeek ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductDetailDto productDetailDto = new ProductDetailDto
                    {
                        ProductId = Convert.ToInt32(dr[0]),
                        ProductName = (dr[1]).ToString(),
                        CompanyName = (dr[2]).ToString(),
                        UnitPrice = Convert.ToInt16(dr[3]),
                        ExprationDate = Convert.ToDateTime(dr[4]),
                        BarKodNo = Convert.ToInt16(dr[5]),
                        ShelfLife = Convert.ToInt16(dr[6]),
                        StockInDate= Convert.ToDateTime(dr[7]),
                    };
                    productDetailDtos.Add(productDetailDto);
                }
            }
            return productDetailDtos;
        }

        //   --Miktarı eksik gelen siparişlerin listesi veren method--

        public List<ProductQuantityDto> GetProductsWithMissingQuantity()
        {
            List<ProductQuantityDto> productQuantityDtos = new List<ProductQuantityDto>();

            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("gp_GetProductsWithMissingQuantity ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProductQuantityDto productQuantityDto = new ProductQuantityDto
                    {
                        ProductId = Convert.ToInt32(dr[0]),
                        ProductName = (dr[1]).ToString(),
                        BarKodNo = Convert.ToInt16(dr[2]),
                        QuantityOrder = Convert.ToInt16(dr[3]),
                        QuantityReceive = Convert.ToInt16(dr[4]),
                    };
                    productQuantityDtos.Add(productQuantityDto);
                }
            }
            return productQuantityDtos;

        }

    }



}
