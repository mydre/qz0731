using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDemo.Db.Model
{
  public   class Calls
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("name")]
        public string name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column("phoneNumber")]
        public string phoneNumber { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Column("datetime")]
        public string datetime { get; set; }

        /// <summary>
        /// 通话时长
        /// </summary>
        [Column("duration")]
        public string dutation { get; set; }

        /// <summary>
        /// 呼叫类型
        /// </summary>
        [Column("type")]
        public string type { get; set; }
    }
}
