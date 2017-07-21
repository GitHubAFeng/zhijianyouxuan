
 function CheckName()
 {
    if($I("tbCustomerName").value.trim()=="")
    {
        showmsg($I("msgName"),"此项为必填项。",true)
        $I("tbCustomerName").focus();
        return false;
    }
    new net.ContentLoader("Ajax/AjaxCheck.aspx",'type=customername&value='+$I("tbCustomerName").value,"POST",name_deal,name_err);
 }
 
 function name_deal()
 {
      if(this.req.responseText == "0")
      {
           showmsg($I("msgName"),"<img style='display:inline;' src='images/reg_ok.gif' />",false);
      }
      else
      {
           showmsg($I("msgName"),"该用户名不存在。",true);
      }
 }
 
 function name_err()
 {
       showmsg($I("msgName"),"服务器忙，请稍候再试。",true);
 }
 
     
 function err()
 {
       showmsg($I("msgEmail"),"服务器忙，请稍候再试。",true);
 }
    
 function SendSuccess()    //显示密码发送成功
 {
       $I("divSuccess").style.display = "";
 }
        
 function showmsg(obj,msg,iserr)   //显示提示信息
 {
        if(iserr)
        { 
            obj.className = "notice_error";
        }
        else
        {
            obj.className = "notice";
        }
        obj.innerHTML = msg;
 }

 function CheckForm()
 {
        if( $I("tbEmail").value.trim() =="" )
        {
            showmsg($I("msgEmail"), "此项为必填项。", true);
        }
        else
        {
            $I("btOK").disabled="disabled";
            $I("loading").style.display = "";
            new net.ContentLoader("Ajax/SendPassword.aspx",'email='+$I("tbEmail").value,"POST",send_deal,send_err);
            return false;  
        } 
  }
        
  function send_deal()
  {
        if(this.req.responseText == 1)
        {
              $I("loading").style.display = "none";
              SendSuccess();
        }
        else if(this.req.responseText == '-1') {

        $I("loading").style.display = "";
        $I("loading").innerHTML = "邮箱不存在.";
              $I("btOK").disabled="";
        }
        else 
        {
            $I("btOK").disabled = "";
            $I("loading").style.display = "";
            $I("loading").innerHTML = "发送失败.";
        }            
   }
   
   function send_err()
   {
         showmsg($I("msgCode"),"服务器忙，请稍候再试。",true);
   }