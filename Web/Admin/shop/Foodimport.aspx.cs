using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.IO;

// 商家餐品信息管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息
// 2010-07-12

public partial class qy_54tss_Admin_Shop_Foodimport : System.Web.UI.Page
{
    EFoodSort dal = new EFoodSort();
    Foodinfo dalfood = new Foodinfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            int tid = HjNetHelper.GetQueryInt("tid", 0);
            hidTogoId.Value = tid.ToString();
        }
    }

    /// <summary>
    /// 导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void in_Click(object sender, EventArgs e)
    {
        int tid = HjNetHelper.GetQueryInt("tid", 0);//商家id
        string suffix = Path.GetExtension(this.fuFoodExcel.PostedFile.FileName);
        string filepath = "~/upload/excel/";
        if (!System.IO.Directory.Exists(Server.MapPath(filepath)))
        {
            System.IO.Directory.CreateDirectory(Server.MapPath(filepath));
        }
        string sFile = filepath + System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + suffix;  //上传后文件的新名


        if (suffix == ".xls" || suffix == ".XLS")
        {
            //保存文件
            try
            {
                this.fuFoodExcel.PostedFile.SaveAs(Server.MapPath(sFile));
            }
            catch (Exception ex)
            {
                HJlog.toLog(ex.Message);
                return;
            }
            //1.添加菜品(检查分类，没有分类就先添加分类(这个地方可比较可以少查询次数))

            //必要的判断以防止excel格式的问题导致程序错误
            try
            {
                //解析文件并保存数据到数据库

                System.Data.DataTable chooseDt = ExcelHelper.ExcelToDataTable(Server.MapPath(sFile), 0);//选中文件DataTable

                //excel格式:第一行是标题,第二行开始是数据
                string presortname = "";
                int n = 0;//0表示是添加分类，1表示此分类
                int sortid = 0;//当前分类编号
                foreach (DataRow item in chooseDt.Rows)
                {
                    FoodinfoInfo model = new FoodinfoInfo();
                    model.FoodName = item[0].ToString().Trim();
                    if (model.FoodName == "")//名称为空时，表示结束
                    {
                        break;
                    }

                    string sortname = item[2].ToString().Trim();//分类名称
                    //n presortname主要用来避免相同分类查询
                    if (n == 0)
                    {
                        presortname = sortname;
                    }
                    else
                    {
                        if (presortname == sortname)
                        {
                            //分类此前已经添加，直接用sortid
                            n = 1;
                        }
                        else
                        {
                            n = 0;
                            presortname = sortname;
                        }
                    }
                    //新添加分类
                    if (n == 0)
                    {
                        EFoodSortInfo sortmodel = new EFoodSortInfo();
                        sortmodel.SortName = WebUtility.InputText(sortname);
                        sortmodel.TogoNum = tid;
                        sortmodel.Jorder = 0;
                        sortid = dal.Add(sortmodel);
                        n = 1;
                    }

                    model.InUse = "y";
                    model.FoodNo = "";
                    model.FPrice = Convert.ToDecimal(item[3].ToString().Trim());
                    model.FPInDate = DateTime.Now;
                    model.FPActiveDate = DateTime.Now;
                    model.FPMaster = tid;
                    model.FoodNamePy = item[1].ToString().Trim();

                    string packagestr = item[4].ToString().Trim();
                    if (packagestr.Length == 0)
                    {
                        packagestr = "0";
                    }


                    model.FullPrice = Convert.ToDecimal(packagestr);
                    model.Remains = 0;
                    model.MaxPerDay = 0;
                    model.Taste = item[6].ToString().Trim();
                    model.Picture = "";
                    model.FoodType = sortid;
                    model.OrderNum = Convert.ToInt32(item[5].ToString().Trim());
                    model.IsRecommend = 0;
                    model.IsSpecial = 0;
                    model.isauth = 1;
                    model.OpenTime = "";

                    int fid = dalfood.Add(model);

                }
            }
            catch (Exception ex)
            {
                AlertScript.RegScript(this, "alert('导入出错，请检查excel');hideloadfix('tbinfood');");//

                HJlog.toLog(ex.ToString());
                return;
            }
            AlertScript.RegScript(this, "alert('批量导入成功');hideloadfix('tbinfood');");//
        }
        else
        {
            AlertScript.RegScript(this, "alert('文件格式不正确，请上传excel文件!');hideloadfix('tbinfood');");
        }
    }

    /// <summary>
    /// 删除所有菜品
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void del_Click(object sender, EventArgs e)
    {
        int tid = HjNetHelper.GetQueryInt("tid", 0);
        string sql = "";
        //菜品
        sql += "delete Foodinfo  where FPMaster = " + tid + ";";
        if (WebUtility.excutesql(sql) > 0)
        {
            AlertScript.RegScript(this, "alert('删除成功');hideload_super();");//
        }
        else
        {
            AlertScript.RegScript(this, "alert('操作失败，请重试');hideload_super();");//
        }
    }

    /// <summary>
    /// 删除所有菜品分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void delsort_Click(object sender, EventArgs e)
    {
        int tid = HjNetHelper.GetQueryInt("tid", 0);
        //分类
        string sql = "delete efoodsort where TogoNUm =" + tid + ";";
        if (WebUtility.excutesql(sql) > 0)
        {
            AlertScript.RegScript(this, "alert('删除成功');hideload_super();");//
        }
        else
        {
            AlertScript.RegScript(this, "alert('操作失败，请重试');hideload_super();");//
        }
    }
}
