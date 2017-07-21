var net = new Object();

//请求状态属性
net.READY_STATE_UNINITIALIZED = 0;    //未初始化
net.READY_STATE_LOADING = 1;          //正在加载
net.READY_STATE_LOADED = 2;           //已加载
net.READY_STATE_INTERACTIVE = 3;      //交互中
net.READY_STATE_COMPLETE = 4;         //完成

net.ContentLoader = function(url, params, method, onload, onerror) 
{
    this.req = null;
    this.onload = onload;
    this.onerror = (onerror) ? onerror : this.defaultError;
    this.loadXMLDoc(url, method, params);
}

net.ContentLoader.prototype.loadXMLDoc = function(url, method, params) 
{
    if (!method) 
    {
        method = "GET";
    }
    if (method == "POST") 
    {
        contentType = 'application/x-www-form-urlencoded;charset=gb2312';
    }
    if (window.XMLHttpRequest) 
    {
        this.req = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) 
    {
        try 
        {
            this.req = new ActiveXObject('MSXML2.XMLHTTP');
        }
        catch (e) 
        {
            try 
            {
                this.req = new ActiveXObject('Microsoft.XMLHTTP');
            }
            catch (e) 
            {
                this.onerror.call(this);
            }
        }
    }
    if (this.req) 
    {
        try 
        {
            var loader = this;
            this.req.onreadystatechange = function() 
            {
                loader.onReadyState.call(loader);
            }
            this.req.open(method, url, true);
            if (contentType) 
            {
                this.req.setRequestHeader('Content-Type', contentType);
            }
            this.req.send(params);
        }
        catch (err) 
        {
            this.onerror.call(this);
        }
    }
}


net.ContentLoader.prototype.onReadyState = function() 
{
    var req = this.req;
    if (req.readyState == net.READY_STATE_COMPLETE) 
    {
        if (req.status == 200 || req.status == 0) 
        {
            this.onload.call(this, req.responseText);
        }
        else 
        {
            this.onerror.call(this, req.responseText);
        }
    }
}

net.ContentLoader.prototype.defaultError = function() 
{
    alert("error fetching data!"
    + "\n\nreadyState:" + this.req.readyState
    + "\nstatus: " + this.req.status
    + "\nheaders: " + this.req.getAllResponseHeaders());
}

function TogoNO_Focus() 
{
    $F('msgTogoNO').innerHTML = "<font color='red'>确保序号有效</font>";
}


function CheckTogoNO()
{
    var val = $F('tbTogoNO').value;

    if (val.trim() == "") 
    {
        return false;
    }
    new net.ContentLoader("../../ajax/Check.aspx", "type=TogoNO&value=" + val, "POST", togono_deal, togono_err);
}

function togono_deal() 
{
    if (this.req.responseText == "1") 
    {
        $F('msgTogoNO').innerHTML = "<font color='red'>配对完成！</font>";
    }
    else 
    {
        $F('msgTogoNO').innerHTML = "<font color='red'>配对失败，请更换序列号重新输入！</font>";
    }
}

function togono_err() 
{
    $F('msgTogoNO').innerHTML = "<font color='red'>服务器忙，请稍后！</font>";
}






//function CheckIsFit() 
//{
//    
//}

//var res = false;

function check() 
{
    var val = $F('tbTogoNO').value;

    if (val.trim() == "") 
    {
        return false;
    }
    new net.ContentLoader("../Check.aspx", "type=TogoNO&value=" + val, "POST", togono_deal1, togono_err1);
    return false;
}

function togono_deal1() 
{
    if (this.req.responseText == "1") 
    {
         res = true; 
         return true;
    }
    else 
    {
        $F('msgTogoNO').innerHTML = "<font color='red'>配对失败，请更换序列号重新输入！</font>";
    }
}

function togono_err1() 
{
    $F('msgTogoNO').innerHTML = "<font color='red'>服务器忙，请稍后！</font>";
}