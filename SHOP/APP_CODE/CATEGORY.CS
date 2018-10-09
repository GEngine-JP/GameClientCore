using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ASPNETAJAXWeb.AjaxEBusiness
{
	public class Category
	{
		public Category()
		{
			///
		}

		public void InitCatalogTreeView(TreeView tv)
		{
			DataSet ds = GetFenleis();
			if(ds == null) return;
			if(ds.Tables.Count <= 0) return;
			DataTable dt = ds.Tables[0];

			tv.Nodes.Clear();     ///清空树的所有节点		
			DataRow[] rowList = dt.Select("ParentID='0'");
			if(rowList.Length < 1) return;

			///创建根节点
			TreeNode root = new TreeNode();
			///设置根节点属性
			root.Text = rowList[0]["Name"].ToString();
			///设置根节点的value值
			root.Value = rowList[0]["ID"].ToString();
			root.Target = "Product";
			root.NavigateUrl = "~/Product.aspx?CategoryID="	+ root.Value;
			root.Expanded = true;

			///添加根节点
			tv.Nodes.Add(root);

			///创建其他节点
			CreateChildNode(root,dt,"Product","~/Product.aspx?CategoryID=");
		}

		private void CreateChildNode(TreeNode parentNode,DataTable dt,string target,string url)
		{
			///选择数据时，添加了排序表达式OrderBy
			DataRow[] rowList = dt.Select("ParentID='" + parentNode.Value
				+ "'","ShowOrder");
			foreach(DataRow row in rowList)
			{   ///创建新节点
				TreeNode node = new TreeNode();
				///设置节点的属性
				node.Text = row["Name"].ToString();
				node.Value = row["ID"].ToString();
				node.Target = target;
				node.NavigateUrl = url + node.Value;
				node.Expanded = true;
				parentNode.ChildNodes.Add(node);
				///递归调用，创建其他节点
				CreateChildNode(node,dt,target,url);
				if(node.ChildNodes.Count > 0)
				{
					node.SelectAction = TreeNodeSelectAction.None;
				}
			}
		}

		public DataSet GetFenleis()
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_GetFenleis";
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

		public DataSet GetSubFenlei(int categoryID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_GetSubFenlei";
			///创建SqlDataAdapter
			SqlDataAdapter da = new SqlDataAdapter(cmdText,con);
			///设置执行方式为存储过程
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			da.SelectCommand.Parameters.Add("@ParentID",SqlDbType.Int,4);
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

		public SqlDataReader GetSingleFenlei(int categoryID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///创建SQL语句
			string cmdText = "SELECT * FROM Category WHERE ID=@ID";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = categoryID;
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

		public void InitFenleiList(ListControl list)
		{
			DataSet ds = GetFenleis();
			if(ds == null) return;
			if(ds.Tables.Count <= 0) return;
			DataTable dt = ds.Tables[0];

			list.Items.Clear();     ///清空树的所有节点		
			DataRow[] rowList = dt.Select("ParentID='0'","ShowOrder");
			if(rowList.Length < 1) return;

			string name = string.Empty;
			string value = string.Empty;
			foreach(DataRow row in rowList)
			{
				name = "|--" + row["Name"].ToString();
				value = row["ID"].ToString();
				list.Items.Add(new ListItem(name,value));
				CreateSubNode(list,dt,row["ID"].ToString(),name);
			}
		}

		private void CreateSubNode(ListControl list,DataTable dt,string parentValue,string parentName)
		{	///选择数据时，添加了排序表达式OrderBy
			DataRow[] rowList = dt.Select("ParentID='" + parentValue + "'","ShowOrder");
			string name = string.Empty;
			string value = string.Empty;
			foreach(DataRow row in rowList)
			{
				name = parentName + " |--" + row["Name"].ToString();
				value = row["ID"].ToString();
				list.Items.Add(new ListItem(name,value));
				CreateSubNode(list,dt,row["ID"].ToString(),name);
			}
		}

		public int AddFenlei(string name,int parentID,string remark)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_AddFenlei";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@Name",SqlDbType.VarChar,50);
			cmd.Parameters.Add("@ParentID",SqlDbType.Int,4);
			cmd.Parameters.Add("@Remark",SqlDbType.Text);
			cmd.Parameters[0].Value = name;
			cmd.Parameters[1].Value = parentID;
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

		public int UpdateFenlei(int categoryID,string name,string remark)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_UpdateFenlei";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters.Add("@Name",SqlDbType.VarChar,50);
			cmd.Parameters.Add("@Remark",SqlDbType.Text);
			cmd.Parameters[0].Value = categoryID;
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

		public int UpdateFenleiOrder(int categoryID,string moveFlag)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_UpdateFenleiOrder";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters.Add("@MoveFlag",SqlDbType.VarChar,20);
			cmd.Parameters[0].Value = categoryID;
			cmd.Parameters[1].Value = moveFlag;

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

		public int DeleteFenlei(int categoryID)
		{	///获取连接字符串
			string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
			///创建连接
			SqlConnection con = new SqlConnection(connectionString);
			///设置被执行存储过程的名称
			string cmdText = "Pr_DeleteCategory";
			///创建SqlCommand
			SqlCommand cmd = new SqlCommand(cmdText,con);
			///设置执行方式为存储过程
			cmd.CommandType = CommandType.StoredProcedure;
			///创建参数并赋值
			cmd.Parameters.Add("@ID",SqlDbType.Int,4);
			cmd.Parameters[0].Value = categoryID;

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
