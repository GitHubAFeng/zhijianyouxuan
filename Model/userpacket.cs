using System;
/// <summary>
/// userpacket:红包批次表(用于分享的红包)
/// </summary>
[Serializable]
public partial class userpacketInfo
{
	private int _id;
    private string _pid;
	private int _userid;
	private DateTime _exptime;
	private DateTime _pulltime;
	private decimal _money;
	private decimal _moneyline;
	private int _state;
	private string _pulltel;
	private int _reveint;
	private int _reveint1;
	private string _revevar;
	private string _revevar1;
	private DateTime _datetime1;
	private DateTime _datetime2;
	/// <summary>
	/// 
	/// </summary>
	public int id
	{
		set{ _id=value;}
		get{return _id;}
	}
	/// <summary>
    /// 红包获取方式 0注册获得红包 1 下单获得红包 2网站推送获得红包
	/// </summary>
    public string pid
	{
		set{ _pid=value;}
		get{return _pid;}
	}
	/// <summary>
	/// 会员编号
	/// </summary>
	public int userid
	{
		set{ _userid=value;}
		get{return _userid;}
	}
	/// <summary>
	/// 
	/// </summary>
	public DateTime exptime
	{
		set{ _exptime=value;}
		get{return _exptime;}
	}
	/// <summary>
	/// 
	/// </summary>
	public DateTime pulltime
	{
		set{ _pulltime=value;}
		get{return _pulltime;}
	}
	/// <summary>
    /// 表示每个红包的金额  随机的情况表示每个红包最多不超过多少，固定的情况表示每个红包的金额
	/// </summary>
	public decimal money
	{
		set{ _money=value;}
		get{return _money;}
	}
	/// <summary>
	/// 
	/// </summary>
	public decimal moneyline
	{
		set{ _moneyline=value;}
		get{return _moneyline;}
	}
	/// <summary>
	/// 有效期
	/// </summary>
	public int state
	{
		set{ _state=value;}
		get{return _state;}
	}
	/// <summary>
	/// 领取的手机号
	/// </summary>
	public string pulltel
	{
		set{ _pulltel=value;}
		get{return _pulltel;}
	}
	/// <summary>
	/// 红包个数
	/// </summary>
	public int reveint
	{
		set{ _reveint=value;}
		get{return _reveint;}
	}
	/// <summary>
    /// 表示金额获取方式  0表示随机 1表示固定 
	/// </summary>
	public int reveint1
	{
		set{ _reveint1=value;}
		get{return _reveint1;}
	}
	/// <summary>
	/// 红包随机字符
	/// </summary>
	public string revevar
	{
		set{ _revevar=value;}
		get{return _revevar;}
	}
	/// <summary>
    /// 红包配置中 表示订单使用条件 满多少使用 0表示无条件
	/// </summary>
	public string revevar1
	{
		set{ _revevar1=value;}
		get{return _revevar1;}
	}
	/// <summary>
	/// 
	/// </summary>
	public DateTime datetime1
	{
		set{ _datetime1=value;}
		get{return _datetime1;}
	}
	/// <summary>
	/// 
	/// </summary>
	public DateTime datetime2
	{
		set{ _datetime2=value;}
		get{return _datetime2;}
	}

}

