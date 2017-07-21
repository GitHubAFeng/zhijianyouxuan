
// EAddress.css:地址簿相关操作.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-03-05

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Hangjing.Common;

namespace Hangjing.Control
{
    [DefaultProperty("Text"), ToolboxData("<{0}:NoTagLable runat=server></{0}:NoTagLable>")]
    public class NoTagLable : System.Web.UI.Control
    {
        // Methods
        protected override void Render(HtmlTextWriter writer)
        {
            if (!StringHelper.IsNullorEmpty(this.NoWrap))
            {
                writer.Write("<nobr>");
            }
            writer.Write(this.Text);
            if (!StringHelper.IsNullorEmpty(this.NoWrap))
            {
                writer.Write("</nobr>");
            }
        }

        // Properties
        public virtual string NoWrap
        {
            get
            {
                object obj2 = this.ViewState["NoWrap"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["NoWrap"] = value;
            }
        }

        public virtual string Text
        {
            get
            {
                object obj2 = this.ViewState["Text"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }
    }

 

}
