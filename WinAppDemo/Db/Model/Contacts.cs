using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDemo.Db.Model
{
  public   class Contacts
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
        /// 区域
        /// </summary>
        [Column("district")]
        public string district { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        [Column("company")]
        public string company { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("Email")]
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string remark { get; set; }
    }
}
