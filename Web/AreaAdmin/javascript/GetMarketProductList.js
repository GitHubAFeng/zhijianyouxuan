/*根据商家的编号获取商家的菜单*/

function GetTogoFoosList()
{
    
    var _name = document.getElementById("hidTogoId");
    var _value = _name.value;
    
	$.ajax({  
	type: "GET",  
	url: "../ajax/GetMarketProductList.aspx",  
	data: { togoid:escape( _value) ,time:new Date().getTime()},  
	success: function(theResponse) 
	{
        document.getElementById("tbodyFoodList").innerHTML = theResponse;
        init();
    }//success end
	});//$.ajax({   end
}

/*计算费用 checkbox事件 数量input事件*/
function SumOrderPrice()
{
    var TotalPrice = parseFloat("0.0");
    var CurrentPrice =parseFloat("0.0");

    var Prices = new Array();  //价格
    var Discout = new Array(); //折扣
    var FoodNum = new Array(); //数量
        
    Prices = document.getElementById("hidPrices").value.split(',');
    Discout = document.getElementById("hidDiscounts").value.split(',');
    FoodNum = document.getElementById("hidFoodNums").value.split(',');
        
    //alert(document.getElementById("hidFoodNums").value);
    
    //计算费用 TODO;checkbox事件 input事件
    //alert(Prices);
    for( var i = 0; i < Prices.length ; i++)
    {        
        //计算费用
        TotalPrice += parseFloat(Prices[i])*parseInt(FoodNum[i]);
        CurrentPrice += parseFloat(Prices[i]) * parseFloat(Discout[i])/10*parseInt(FoodNum[i]);
    }
    
    document.getElementById("tbTotalPrice").value = TotalPrice+"";     //总价
    document.getElementById("tbCurrentprice").value = CurrentPrice+"" ;//现价
}