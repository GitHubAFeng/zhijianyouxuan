function CheckTable(tableName) {
   if(!document.getElementById(tableName)){return false;}
   var AllRows = document.getElementById(tableName).getElementsByTagName("tr");
   for (var i = 1; i < AllRows.length ; i++) {
        AllRows[i].onmouseover = function(){this.className = "listover";};
        AllRows[i].onmouseout = function(){this.className = "list";};
        AllRows[i].checkbox = AllRows[i].cells[0].getElementsByTagName("input")[0];
        AllRows[i].onclick = function()
        {        
            if(this.checkbox.checked == true)
            {
                this.SetCheck(false);
            }
            else
            {
                this.SetCheck(true);
            }            
        }
        AllRows[i].SetCheck = function(bool)
        {
            if(bool == true)
            {
                this.checkbox.checked = true;
                this.style.background = "#D2E3F2";
            }
            else
            {    
                this.checkbox.checked = false;
                this.style.background = "#FFFFFF";
            }
        }
        AllRows[i].checkbox.onclick = function()  //解决点击checkbox引发时间冲突，有待改善
        {
            if(document.all)
            {
                this.parentNode.parentNode.click();
            }
            else
            {
               if (this.checked == false)
               {
                this.checked = true;
               }
               else
               {
                this.checked = false;
               }
               
            }
        }; 
    }
    
   this.CheckAll = function()
   {
      for(i=1;i<AllRows.length ;i++)
      {
         AllRows[i].SetCheck(true);
      }
   }
   this.CheckNo= function()
    {
       for(i=1;i<AllRows.length ;i++)
       {
           AllRows[i].SetCheck(false);
       }
    }
   this.ReCheck = function()
   {
       for(i=1;i<AllRows.length ;i++)
       {
           if(AllRows[i].checkbox.checked == true)
           {
                AllRows[i].SetCheck(false);
           }
           else
           {
                AllRows[i].SetCheck(true);
           }
       }
   }
   this.GetChecks = function()
   {
        var nums = new Array();
        var j = 0;
        for(var i=1; i<AllRows.length ; i++)
        {
            if(AllRows[i].checkbox.checked == true){nums[j] = AllRows[i].checkbox.value;j++;}
        }
        return nums;
    }    
}
var Table ;
var globlenums ;

function init()
{
    Table = new CheckTable("TBuserfood");
}
function cart()
{
    var nums = Table.GetChecks();
    if(nums == undefined || nums.length == 0)
    {
        alert("请选择要加入购物车的餐品!");
        return false;
    }            
    document.getElementById("hfmyfoodids").value = ArrayToString(nums);
    var pids = escape( encodeURI(ArrayToString(nums)));
    window.location = "../Shoppingcart.aspx?pid="+pids;
    return true;
}

function ArrayToString(arr)
{
    var temp = "";
    for(var i = 0; i< arr.length;i++)
    {
        temp += arr[i];
        if(i != arr.length - 1)
            temp += ",";
    }
    return temp;
}

function jDel() 
{
    var nums = Table.GetChecks();
    if (nums == undefined || nums.length == 0) 
    {
        alert("请选择要删除的对象!");
        return false;
    }
    document.getElementById("hdDels").value = ArrayToString(nums);

    return confirm("确定要删除吗？");
}


function marketcart() {
    var nums = Table.GetChecks();
    if (nums == undefined || nums.length == 0) {
        alert("请选择要加入购物车的餐品!");
        return false;
    }
    document.getElementById("hfmyfoodids").value = ArrayToString(nums);
    var pids = escape(encodeURI(ArrayToString(nums)));
    window.location = "../marketcart.aspx?pid=" + pids;
    return true;
}
