using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
/// <summary>
/// 系統模块管理
/// </summary>
public partial class Admin_Permission_Modulelist : System.Web.UI.Page
{
    sys_Module dal = new sys_Module();
    private IList<sys_ModuleInfo> modulelist
    {
        get
        {
            object ob = ViewState["SqlWhere"];
            return (IList<sys_ModuleInfo>)ob;
        }

        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetData();
        }
    }

    protected IList<sys_ModuleInfo> getsub(object id)
    {
        return modulelist.Where(p => p.M_ParentID == Convert.ToInt32(id)).ToList<sys_ModuleInfo>();
    }

    protected void rtpNewsSortList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string Id = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "del":
                //判断权限 
                int _rs = WebUtility.checkOperator(4);
                if (_rs == 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                    return;
                }
                if (dal.DelList(Id) > 0)
                {
                    //刪除子級
                    dal.Delsub(Convert.ToInt32(Id));
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:刪除成功!','250','150','true','1000','true','text');init();");
                    GetData();
                    //删除权限
                    new sys_RolePermission().DelRolePermissionByPageCode(Convert.ToInt32(Id));
                    //更新缓存(所有角色的)
                    Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/Permissions");
                }
                else
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:刪除失敗!','250','150','true','1000','true','text');init();");
                    GetData();
                }
                break;
        }

    }

    protected void GetData()
    {
        IList<sys_ModuleInfo> list = dal.getAll();
        IList<sys_ModuleInfo> plist = list.Where(p => p.M_ParentID == 0).ToList<sys_ModuleInfo>();
        modulelist = list.Where(p => p.M_ParentID > 0).ToList<sys_ModuleInfo>();
        WebUtility.BindRepeater(rtpNewsSortList, plist);
    }
}
