using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABC.SHP.Entity
{
    /// <summary>
    /// SHP框架实体类
    /// </summary>
    public class SHPFramework
    {
        /// <summary>
        /// SHP内部版本
        /// </summary>
        public static string SHPInnerVersion { get; set; }

        /// <summary>
        /// 人行分配的网点号
        /// </summary>
        public static string BranchID { get; set; }

        /// <summary>
        /// 固定资产编号
        /// </summary>
        public static string AssetID { get; set; }

        /// <summary>
        /// 终端授权级别
        /// </summary>
        public static string AuthLev { get; set; }

        /// <summary>
        /// 终端号：根据IP从分行数据库获取
        /// </summary>
        public static string TerminalID { get; set; }

        /// <summary>
        /// 分配柜员号：根据IP从分行DB获取
        /// </summary>
        public static string TellerID { get; set; }

        /// <summary>
        /// Cefruntime运行时已加载
        /// </summary>
        public static bool CefRuntimeLoaded 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// webkit浏览组件实例化
        /// </summary>
        public static bool dSeeCreated { get; set; }
    }
}
