using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ASPNETAJAXWeb.AjaxEBusiness
{
	public class Product
	{
		public Product()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataSet GetProducts()
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_GetProducts";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///设置执行方式为存储过程
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			///定义DataSet
			DataSet ds = new DataSet();
			try
			{   ///打开连接
				con.Open();
				///填充数据
				da.Fill(ds,"DataTable");
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return ds;
		}

		public DataSet GetProductByFenlei(int categoryID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_GetProductByFenlei";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///设置执行方式为存储过程
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			da.SelectCommand.Parameters.Add("@CategoryID",SqlDbType.Int,4);
			da.SelectCommand.Parameters[0].Value = categoryID;
			///定义DataSet
			DataSet ds = new DataSet();
			try
			{   ///打开连接
				con.Open();
				///填充数据
				da.Fill(ds,"DataTable");
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return ds;
		}

		public DataSet GetAttributeByFenlei(int categoryID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行SQL语句
			string cmdText = "SELECT * FROM Attribute WHERE CategoryID=@CategoryID";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///创建参数并赋值
			da.SelectCommand.Parameters.Add("@CategoryID",SqlDbType.Int,4);
			da.SelectCommand.Parameters[0].Value = categoryID;
			///定义DataSet
			DataSet ds = new DataSet();
			try
			{   ///打开连接
				con.Open();
				///填充数据
				da.Fill(ds,"DataTable");
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return ds;
		}

		public DataSet GetAttributeByProduct(int productID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_GetAttributeByProduct";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///设置执行方式为存储过程
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			da.SelectCommand.Parameters.Add("@ProductID",SqlDbType.Int,4);
			da.SelectCommand.Parameters[0].Value = productID;
			///定义DataSet
			DataSet ds = new DataSet();
			try
			{   ///打开连接
				con.Open();
				///填充数据
				da.Fill(ds,"DataTable");
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return ds;
		}

		public SqlDataReader GetSingleProduct(int productID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///创建SQL语句
			string cmdText = "SELECT [Product].*,[User].Username FROM Product INNER JOIN [User] ON [Product].UserID = [User].ID WHERE [Product].ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = productID;
			///定义SqlDataReader
			SqlDataReader dr;
			try
			{   ///打开连接
				con.Open();
				///读取数据
				dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}

			return dr;
		}
		
		public int AddProduct(string name,int categoryID,int userID,decimal price,int stock,string remark)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_AddProduct";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@Name",SqlDbType.VarChar,50);
			cmd.Parameters.Add("@CategoryID",SqlDbType.Int,4);
			cmd.Parameters.Add("@UserID",SqlDbType.Int,4);
			cmd.Parameters.Add("@Price",SqlDbType.Money,8);
			cmd.Parameters.Add("@Stock",SqlDbType.Int,4);
			cmd.Parameters.Add("@Remark",SqlDbType.Text);			
			cmd.Parameters[0].Value = name;
			cmd.Parameters[1].Value = categoryID;
			cmd.Parameters[2].Value = userID;
			cmd.Parameters[3].Value = price;
			cmd.Parameters[4].Value = stock;
			cmd.Parameters[5].Value = remark;

			cmd.Parameters.Add("@RETURN",SqlDbType.Int,4);
			cmd.Parameters[6].Direction = ParameterDirection.ReturnValue;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return (int)cmd.Parameters[6].Value;
		}

		public int UpdateProduct(int productID,string name,string remark)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_UpdateProduct";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters.Add("@Name",SqlDbType.VarChar,50);
			cmd.Parameters.Add("@Remark",SqlDbType.Text);
			cmd.Parameters[0].Value = productID;
			cmd.Parameters[1].Value = name;
			cmd.Parameters[2].Value = remark;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

		public int UpdateProductPicture(int productID,string pictureUrl)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行的SQL语句
			string cmdText = "UPDATE [Product] SET PictureUrl=@PictureUrl WHERE ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters.Add("@pictureUrl",SqlDbType.VarChar,255);
			cmd.Parameters[0].Value = productID;
			cmd.Parameters[1].Value = pictureUrl;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

		public int UpdateProductStock(int productID,int currentSaleNumber)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行的SQL语句
			string cmdText = "UPDATE [Product] SET Stock=Stock-@CurrentSaleNumber,SaleNumber=SaleNumber+@CurrentSaleNumber WHERE ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters.Add("@CurrentSaleNumber",SqlDbType.Int,4);
			cmd.Parameters[0].Value = productID;
			cmd.Parameters[1].Value = currentSaleNumber;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

		public int UpdateProductViewCount(int productID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行的SQL语句
			string cmdText = "UPDATE [Product] SET ViewCount=ViewCount+1 WHERE ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = productID;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

		public int DeleteProduct(int productID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_DeleteProduct";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = productID;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

		public int AddAttributeValue(int productID,int AttributeID,string value)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_AddProductAttribute";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@ProductID",SqlDbType.Int,4);
			cmd.Parameters.Add("@AttributeID",SqlDbType.Int,4);
			cmd.Parameters.Add("@Value",SqlDbType.Text);
			cmd.Parameters[0].Value = productID;
			cmd.Parameters[1].Value = AttributeID;
			cmd.Parameters[2].Value = value;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

		public DataSet GetCommentByProdcut(int productID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行SQL语句
			string cmdText = "SELECT * FROM ProductComment WHERE ProductID=@ProductID ORDER BY CreateDate DESC";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///创建参数并赋值
			da.SelectCommand.Parameters.Add("@ProductID",SqlDbType.Int,4);
			da.SelectCommand.Parameters[0].Value = productID;
			///定义DataSet
			DataSet ds = new DataSet();
			try
			{   ///打开连接
				con.Open();
				///填充数据
				da.Fill(ds,"DataTable");
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return ds;
		}

		public int AddProductComment(string title,string body,string ip,string email,int productID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行的SQL语句
			string cmdText = "INSERT INTO ProductComment(Title,Body,IP,Email,ProductID,CreateDate)VALUES(@Title,@Body,@IP,@Email,@ProductID,GETDATE())";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@Title",SqlDbType.VarChar,200);
			cmd.Parameters.Add("@Body",SqlDbType.VarChar,1000);
			cmd.Parameters.Add("@IP",SqlDbType.VarChar,50);
			cmd.Parameters.Add("@Email",SqlDbType.VarChar,255);
			cmd.Parameters.Add("@ProductID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = title;
			cmd.Parameters[1].Value = body;
			cmd.Parameters[2].Value = ip;
			cmd.Parameters[3].Value = email;
			cmd.Parameters[4].Value = productID;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

		public int DeleteProductComment(int commentID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行的SQL语句
			string cmdText = "DELETE ProductComment WHERE ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = commentID;

			int result = -1;
			try
			{	///打开连接
				con.Open();
				///操作数据
				result = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return result;
		}

	}
}
