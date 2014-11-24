using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABC.EQUK.Entity
{
    public class TransactionInfo
    {
        /// <summary>
        /// 交易ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 交易名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 交易图形按钮，图像路径
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 交易dll路径
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 交易传参
        /// </summary>
        public string Parameter { get; set; }
    }
}
