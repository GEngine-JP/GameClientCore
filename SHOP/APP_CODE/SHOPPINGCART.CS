using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ASPNETAJAXWeb.AjaxEBusiness
{
	public class ShoppingCartItem
	{
		private int productID = -1;
		private string name = string.Empty;
		private int number = 0;
		private decimal price = 0.0m;

		public int ProductID
		{
			get
			{
				return productID;
			}
			set
			{
				productID = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public int Number
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}

		public decimal Price
		{
			get
			{
				return price;
			}
			set
			{
				price = value;
			}
		}
	}

	/// <summary>
	/// 购物车
	/// </summary>
	public class ShoppingCart
	{
		public const string SHOPPINTCARTKEY = "SHOPPINTCARTKEY";
		private ArrayList shoppingCartList;
		private HttpSessionState session = null;

		public ArrayList ShoppingCartList
		{
			get
			{
				return shoppingCartList;
			}
		}

		private ShoppingCart()
		{

		}
		/// <summary>
		/// 购物车初始化
		/// </summary>
		/// <param name="session"></param>
		public ShoppingCart(HttpSessionState session)
		{
			this.session = session;
			if(session != null)
			{
				if(session[SHOPPINTCARTKEY] != null)
				{
					shoppingCartList = (ArrayList)session[SHOPPINTCARTKEY];
				}
				else
				{
					shoppingCartList = new ArrayList();
					session[SHOPPINTCARTKEY] = shoppingCartList;
				}
			}
		}

		/// <summary>
		/// 向购物车中的添加商品
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		public int AddProductToShoppingCart(ShoppingCartItem product)
		{
			if(product == null) return -1;
			///获取购物车中的商品
			shoppingCartList = (ArrayList)session[SHOPPINTCARTKEY];
			if(shoppingCartList == null) return -1;

			///比较购物车中是否已经添加了该商品
			int index = 0;
			for(index = 0; index < shoppingCartList.Count; index++)
			{   ///如果已经添加了，则修改购物车中商品的数量
				if(((ShoppingCartItem)shoppingCartList[index]).ProductID == product.ProductID)
				{
					((ShoppingCartItem)shoppingCartList[index]).Number++;
					break;
				}
			}
			///如果没有添加，则把该商品添加到购物车中
			if(index == shoppingCartList.Count)
			{
				shoppingCartList.Add(product);
			}
			///重新保存购物车中的数据
			session[SHOPPINTCARTKEY] = shoppingCartList;
			return 1;
		}

		/// <summary>
		/// 删除购物车中的商品
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		public int DeleteProductFromShoppingCart(ShoppingCartItem product)
		{
			if(product == null) return -1;
			///获取购物车中的商品
			shoppingCartList = (ArrayList)session[SHOPPINTCARTKEY];
			if(shoppingCartList == null) return -1;

			///从购物车查找被删除的商品
			foreach(ShoppingCartItem item in shoppingCartList)
			{
				if(item.ProductID == product.ProductID)
				{   ///移除该商品
					shoppingCartList.Remove(item);
					break;
				}
			}
			///重新保存购物车中的数据
			session[SHOPPINTCARTKEY] = shoppingCartList;
			return 1;
		}

		/// <summary>
		/// 更新购物车中的商品
		/// </summary>
		/// <param name="products"></param>
		/// <returns></returns>
		public int UpdateShoppingCart(ArrayList products)
		{   
			if(products == null || products.Count <= 0)return -1;			
			///获取购物车中的商品
			shoppingCartList = (ArrayList)session[SHOPPINTCARTKEY];
			if(shoppingCartList == null) return -1;
			///更新购物车中的商品
			for(int index = 0; index < shoppingCartList.Count; index++)
			{
				foreach(ShoppingCartItem product in products)
				{
					if(((ShoppingCartItem)shoppingCartList[index]).ProductID == product.ProductID)
					{
						((ShoppingCartItem)shoppingCartList[index]).Number = product.Number;
						break;
					}
				}				
			}

			///重新保存购物车中的数据
			session[SHOPPINTCARTKEY] = shoppingCartList;
			return 1;
		}

		/// <summary>
		/// 清空购物车中的商品
		/// </summary>
		/// <returns></returns>
		public int ClearShoppingCart()
		{	///获取购物车中的商品
			shoppingCartList = (ArrayList)session[SHOPPINTCARTKEY];
			if(shoppingCartList == null) return -1;
			///清空购物车中的商品
			shoppingCartList.Clear();
			session[SHOPPINTCARTKEY] = null;
			return 1;
		}
	}

	public class Order
	{
		public Order()
		{

		}

		public string GetOrderLastOrderNo()
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行SQL语句
			string cmdText = "SELECT TOP 1 [Order].OrderNo FROM [Order] WHERE DATEDIFF(year,CreateDate,GETDATE()) = 0 AND DATEDIFF(month,CreateDate,GETDATE()) = 0 AND DATEDIFF(day,CreateDate,GETDATE()) = 0 ORDER BY CreateDate DESC";
			///创建SqlDataAdapter
			SqlCommand cmd = new SqlCommand(cmdText,con);

			object orderNo;
			try
			{   ///打开连接
				con.Open();
				///填充数据
				orderNo = cmd.ExecuteScalar();
			}
			catch(Exception ex)
			{   ///抛出异常
				throw new Exception(ex.Message,ex);
			}
			finally
			{   ///关闭连接
				con.Close();
			}

			return orderNo == null ? string .Empty : orderNo.ToString();
		}

		public DataSet GetOrderByUser(int userID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行SQL语句
			string cmdText = "SELECT [Order].* FROM [Order]  WHERE [Order].UserID=@UserID ORDER BY CreateDate DESC";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///创建参数并赋值
			da.SelectCommand.Parameters.Add("@UserID",SqlDbType.Int,4);
			da.SelectCommand.Parameters[0].Value = userID;
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

		public SqlDataReader GetSingleOrder(int orderID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///创建SQL语句
			string cmdText = "SELECT [Order].*,OrderItem.ProductID,OrderItem.Number FROM [Order] INNER JOIN OrderItem ON [Order].ID = OrderItem.OrderID WHERE [Order].ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = orderID;
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
		public DataSet GetOrderItemByOrder(int orderID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行SQL语句
			string cmdText = "SELECT OrderItem.*,[Product].Name,[Product].Price FROM OrderItem INNER JOIN [Product] ON [Product].ID = OrderItem.ProductID WHERE OrderItem.OrderID=@OrderID";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///创建参数并赋值
			da.SelectCommand.Parameters.Add("@OrderID",SqlDbType.Int,4);
			da.SelectCommand.Parameters[0].Value = orderID;
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

		public int AddOrder(string orderNo,int userID,int totalNumber,decimal totalMoney)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_AddOrder";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@orderNo",SqlDbType.VarChar,50);
			cmd.Parameters.Add("@UserID",SqlDbType.Int,4);
			cmd.Parameters.Add("@totalNumber",SqlDbType.Int,4);			
			cmd.Parameters.Add("@totalMoney",SqlDbType.Money);
			cmd.Parameters[0].Value = orderNo;
			cmd.Parameters[1].Value = userID;
			cmd.Parameters[2].Value = totalNumber;
			cmd.Parameters[3].Value = totalMoney;

			cmd.Parameters.Add("@RETURN",SqlDbType.Int,4);
			cmd.Parameters[4].Direction = ParameterDirection.ReturnValue;

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

			return (int)cmd.Parameters[4].Value;
		}

		public int AddOrderItem(int orderID,int productID,int number)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "INSERT INTO OrderItem(OrderID,ProductID,Number)VALUES(@OrderID,@ProductID,@Number)";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@OrderID",SqlDbType.Int,4);
			cmd.Parameters.Add("@ProductID",SqlDbType.Int,4);
			cmd.Parameters.Add("@Number",SqlDbType.Int,4);
			cmd.Parameters[0].Value = orderID;
			cmd.Parameters[1].Value = productID;
			cmd.Parameters[2].Value = number;

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

		public int UpdateOrderStatus(int orderID,byte status)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行的SQL语句
			string cmdText = "UPDATE [Order] SET Status=@Status WHERE ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters.Add("@Status",SqlDbType.TinyInt,1);
			cmd.Parameters[0].Value = orderID;
			cmd.Parameters[1].Value = status;

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
