using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 餐馆地图坐标信息
    /// </summary>
    public class ETogoLocalInfo
    {
        private int _dataid;
        private int _togoid;
        private string _lat;
        private string _lng;
        private string _polygon;
        private decimal _radius;

        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 店铺编号
        /// </summary>
        public int TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// 商家配送范围坐标 不规则形状 设置商家时需要设置商家的配送范围（在谷歌地图上进行标注）
        /// "polygon":"28.02255295138866,120.63906669616699|28.026227643111696,120.66460132598877|28.019256987158677,120.71803092956543|"
        /// </summary>
        public string Polygon
        {
            set { _polygon = value; }
            get { return _polygon; }
        }

        /// <summary>
        /// 店铺坐标 横坐标
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        /// <summary>
        /// 店铺坐标 纵坐标
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

        /// <summary>
        /// 配送半径（在有不规则范围的情况下此字段为预留字段） 即：配送范围坐标和配送半径字段一般只有一个起作用
        /// </summary>
        public decimal Radius
        {
            set { _radius = value; }
            get { return _radius; }
        }
    }
}
