using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Web.Script.Serialization;

public partial class Admin_Permission_RolePermission : System.Web.UI.Page
{
    sys_Module dal = new sys_Module();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetData();
            //獲取現在權限，在頁面中選中
            string json = "";
            int rid = HjNetHelper.GetQueryInt("id", 0);

            sys_Roles dalrole = new sys_Roles();
            sys_RolesInfo model = dalrole.GetModel(rid);
            lbhead.InnerText = "权限分配 - "+model.R_RoleName;

            string sql = "P_RoleID =" + rid;
            sys_RolePermission dalrp = new sys_RolePermission();
            IList<sys_RolePermissionInfo> rlist = dalrp.GetList(999, 1, sql, "PermissionID", 1);
            json = WebUtility.ObjectToJson("rp", rlist);
            hidhas.Value = json;
        }
    }

    protected void GetData()
    {
        IList<sys_ModulePermitionInfo> allmplist = new sys_ModulePermition().GetList(10000, 1, " 1=1 ", "ReveInt", 1);
        IList<sys_ModuleInfo> list = dal.getAll();
        IList<sys_ModuleInfo> plist = list.Where(p => p.M_ParentID == 0).ToList<sys_ModuleInfo>();
        foreach (sys_ModuleInfo item in plist)
        {
            item.sublist = list.Where(p => p.M_ParentID == item.ModuleID).ToList<sys_ModuleInfo>();
            foreach (sys_ModuleInfo m in item.sublist)
            {
                m.mplist = allmplist.Where(p => p.ModuleID == m.ModuleID).ToList<sys_ModulePermitionInfo>();
            }    
        }

        WebUtility.BindRepeater(rtpNewsSortList, plist);
    }

    protected void sava_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        sys_RolePermission dalrp = new sys_RolePermission();
        int rid = HjNetHelper.GetQueryInt("id", 0);
        //刪除原來有的權限
        dalrp.DelRolePermissionByRid(rid);
        string json = hidhas.Value;
        IList<sys_RolePermissionInfo> rplist = new JavaScriptSerializer().Deserialize<IList<sys_RolePermissionInfo>>(json);
        foreach (sys_RolePermissionInfo item in rplist)
        {
            item.P_RoleID = rid;
            item.P_ApplicationID = 0;
            dalrp.Add(item);
        }
        AlertScript.RegScript(this.Page, UpdatePanel1, "hideload_super();showMessage('操作成功','success','true',5);init();");
        //更新缓存(所有角色的)
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/Permissions");
    }
}
