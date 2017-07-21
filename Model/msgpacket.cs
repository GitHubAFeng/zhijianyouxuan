using System;
/// <summary>
/// msgpacket:用户红包表
/// </summary>
[Serializable]
public partial class msgpacketInfo
{
	private int _id;
    private string _pid;
	private decimal _alltotal;
	private int _num;
	private decimal _eachmoney;
	private DateTime _validitytime;
	private decimal _moneyline;
	private decimal _cmoney;
	private DateTime _starttime;
	private DateTime _endtime;
	private int _reveint;
	private int _reveint1;
	private string _revevar;
	private string _revevar1;
	private DateTime _datetime1;
	private DateTime _datetime2;
	/// <summary>
	/// 编号
	/// </summary>
	public int id
	{
		set{ _id=value;}
		get{return _id;}
	}
    /// <summary>
    /// 红包编号
    /// </summary>
    public string pid
    {
        set { _pid = value; }
        get { return _pid; }
    }
	/// <summary>
	/// 红包金额
	/// </summary>
	public decimal alltotal
	{
		set{ _alltotal=value;}
		get{return _alltotal;}
	}
	/// <summary>
	/// 使用状态 0 未使用 1已使用
	/// </summary>
	public int num
	{
		set{ _num=value;}
		get{return _num;}
	}
	/// <summary>
    /// 
	/// </summary>
	public decimal eachmoney
	{
		set{ _eachmoney=value;}
		get{return _eachmoney;}
	}
	/// <summary>
    /// 过期时间
	/// </summary>
	public DateTime validitytime
	{
		set{ _validitytime=value;}
		get{return _validitytime;}
	}
	/// <summary>
	/// 使用条件（满X元使用）
	/// </summary>
	public decimal moneyline
	{
		set{ _moneyline=value;}
		get{return _moneyline;}
	}
	/// <summary>
	/// 
	/// </summary>
	public decimal cmoney
	{
		set{ _cmoney=value;}
		get{return _cmoney;}
	}
	/// <summary>
    /// 领取时间
	/// </summary>
	public DateTime starttime
	{
		set{ _starttime=value;}
		get{return _starttime;}
	}
	/// <summary>
	/// 
	/// </summary>
	public DateTime endtime
	{
		set{ _endtime=value;}
		get{return _endtime;}
	}
	/// <summary>
    /// 
	/// </summary>
	public int ReveInt
	{
		set{ _reveint=value;}
		get{return _reveint;}
	}
	/// <summary>
	/// 
	/// </summary>
	public int ReveInt1
	{
		set{ _reveint1=value;}
		get{return _reveint1;}
	}
	/// <summary>
	/// 领取的手机
	/// </summary>
	public string ReveVar
	{
		set{ _revevar=value;}
		get{return _revevar;}
	}
	/// <summary>
	/// 
	/// </summary>
	public string ReveVar1
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
    /// <summary>
	/// 
	/// </summary>
    public string cardnum
    {
        set;
        get;
    }

}

