using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

///引入新的命名空间
using System.Data;
using System.Web.Script.Services;
using AjaxControlToolkit;
using ASPNETAJAXWeb.AjaxEBusiness;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]    ///添加脚本服务
public class AjaxService:System.Web.Services.WebService
{
	public static string[] autoCompleteFileList = null;
	public AjaxService()
	{

	}

	[System.Web.Services.WebMethod()]
	[System.Web.Script.Services.ScriptMethod()]
	public string[] GetProductList(string prefixText,int count)
	{   ///检测参数是否为空
		if(string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;
		if(autoCompleteFileList == null)
		{	///从数据库中获取所有商品的名称
			Product product = new Product();
			DataSet ds = product.GetProducts();
			if(ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return null;
			///将商品名称保存到临时数组中
			string[] tempFileList = new string[ds.Tables[0].Rows.Count];
			for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				tempFileList[i] = ds.Tables[0].Rows[i]["Name"].ToString();
			}
			///对数组进行排序
			Array.Sort(tempFileList,new CaseInsensitiveComparer());
			autoCompleteFileList = tempFileList;
		}
		///定位二叉树搜索的起点
		int index = Array.BinarySearch(autoCompleteFileList,prefixText,new CaseInsensitiveComparer());
		if(index < 0)
		{   ///修正起点
			index = ~index;
		}
		///搜索符合条件的商品名称
		int matchCount = 0;
		for(matchCount = 0; matchCount < count && matchCount + index < autoCompleteFileList.Length; matchCount++)
		{   ///查看开头字符串相同的项
			if(autoCompleteFileList[index + matchCount].StartsWith(prefixText,StringComparison.CurrentCultureIgnoreCase) == false)
			{
				break;
			}
		}
		///处理搜索结果
		string[] matchResultList = new string[matchCount];
		if(matchCount > 0)
		{   ///复制搜索结果
			Array.Copy(autoCompleteFileList,index,matchResultList,0,matchCount);
		}
		return matchResultList;
	}
}

