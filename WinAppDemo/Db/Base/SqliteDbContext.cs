using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAppDemo.Db.Model;

namespace WinAppDemo.Db.Base
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext() : base("sqlite_connection_string")
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<WxAccount> WxAccounts { get; set; }
        public DbSet<WxMessage> WxMessages { get; set; }
        public DbSet<WxFriend> WxFriends { get; set; }
        public DbSet<WxSns> WxSns { get; set; }
        public DbSet<WxNewFriend> WxNewFriend { get; set; }
    }
}
