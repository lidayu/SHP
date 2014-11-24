using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABC.EQUK.Entity
{
    public class UserInfo
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 登录状态：true：已登录；false：未登录
        /// </summary>
        public bool Logined { get; set; }

        /// <summary>
        /// 用户性别：0--女；1--男
        /// </summary>
        public int Gender { get; set; }

        /*
         * 待扩充其他属性：军官证、护照
         * 
         * 
         * 
         * */
        



    }
}
