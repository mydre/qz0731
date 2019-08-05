using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDemo.Db.Model
{
  public  class Sms
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column("phoneNumber")]
        public string phoneNumber { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string content { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Column("datetime")]
        public string datetime { get; set; }

        /// <summary>
        /// 短信接收状态
        /// 0是接收短信
        /// 1是发送短信
        /// </summary>
        [Column("isSend")]
        public string isSend { get; set; }
    }
}
