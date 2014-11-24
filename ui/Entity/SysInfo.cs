using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABC.SHP.Entity
{
    /// <summary>
    /// 交易信息类，加载配置文件信息
    /// </summary>
    public class SysInfo
    {
        /// <summary>
        /// 交易名称
        /// </summary>
        public string sName { get; set; }

        /// <summary>
        /// 交易类型，CS || BS
        /// </summary>
        public string sType { get; set; }

        /// <summary>
        /// 交易内核，BS架构时有效
        /// </summary>
        public string sCore { get; set; }

        /// <summary>
        /// 交易命令。CS架构为程序集，BS为目标地址
        /// </summary>
        public string sCommand { get; set; }

        /// <summary>
        /// 交易参数。CS为构造函数传入参数，BS为目标地址+请求字符串
        /// </summary>
        public string sParameter { get; set; }
    }
}
