using System;
/// <summary>
/// msgpacketrecord:红包记录表
/// </summary>
[Serializable]
public partial class msgpacketrecordInfo
{
	private int _id;
	private int _pid;
	private DateTime _pulltime;
	private decimal _pullmoney;
    private string _pulltel;
	private int _reveint;
	private string _revevar;
	private DateTime _datetime1;
	/// <summary>
	/// 自增字段
	/// </summary>
	public int id
	{
		set{ _id=value;}
		get{return _id;}
	}
	/// <summary>
	/// 红包编号
	/// </summary>
	public int pid
	{
		set{ _pid=value;}
		get{return _pid;}
	}
	/// <summary>
	/// 使用时间
	/// </summary>
	public DateTime pulltime
	{
		set{ _pulltime=value;}
		get{return _pulltime;}
	}
	/// <summary>
	/// 使用金额
	/// </summary>
	public decimal pullmoney
	{
		set{ _pullmoney=value;}
		get{return _pullmoney;}
	}
    /// <summary>
    /// 手机号
    /// </summary>
    public string pulltel
    {
        set { _pulltel = value; }
        get { return _pulltel; }
    }
	/// <summary>
	/// 
	/// </summary>
	public int reveint
	{
		set{ _reveint=value;}
		get{return _reveint;}
	}
	/// <summary>
	/// 
	/// </summary>
	public string revevar
	{
		set{ _revevar=value;}
		get{return _revevar;}
	}
	/// <summary>
	/// 
	/// </summary>
	public DateTime datetime1
	{
		set{ _datetime1=value;}
		get{return _datetime1;}
	}

}

