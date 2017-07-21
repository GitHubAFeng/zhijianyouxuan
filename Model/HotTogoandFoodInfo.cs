using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class HotTogoandFoodInfo
    {
        protected int _dataid;
        protected string _togoids;
        protected string _foodids;
        protected string _otherids;

        /// <summary>
        /// 编号
        /// </summary>
        public int Dataid
        {
            get
            {
                return this._dataid;
            }

            set
            {
                this._dataid = value;
            }
        }

        /// <summary>
        /// 商家IDS
        /// </summary>
        public string Togoids
        {
            get
            {
                return this._togoids;
            }

            set
            {
                this._togoids = value;
            }
        }

        /// <summary>
        /// 餐品IDs
        /// </summary>
        public string Foodids
        {
            get
            {
                return this._foodids;
            }

            set
            {
                this._foodids = value;
            }
        }

        /// <summary>
        /// 备用
        /// </summary>
        public string Otherids
        {
            get
            {
                return this._otherids;
            }

            set
            {
                this._otherids = value;
            }
        }


    }
}
