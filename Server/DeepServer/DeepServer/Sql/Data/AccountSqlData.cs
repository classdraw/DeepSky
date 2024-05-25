using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DeepServer.Sql.Data
{
    public class AccountSqlData :BaseSqlData
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string QQ { get; set; }
        public string WX { get; set; }
        public string MobilePhone { get; set; }
        public int LoginType { get; set; }
        public string UID { get; set; }
        public string Role1 { get; set; }
        public string Role2 { get; set; }
        public string Role3 { get; set; }

        protected override void Reset()
        {
            this.ID = 0;
            this.LoginName = string.Empty;
            this.Password = string.Empty;
            this.QQ = string.Empty;
            this.WX = string.Empty;
            this.MobilePhone = string.Empty;
            this.LoginType = 0;
            this.UID = string.Empty;
            this.Role1 = string.Empty;
            this.Role2 = string.Empty;
            this.Role3 = string.Empty;
        }

        public override bool IsEmpty()
        {
            return this.ID == 0;
        }

        public override void SetSqlData(MySqlDataReader reader)
        {
            this.ID = reader.GetInt32("id");
            this.LoginName = reader.GetString("loginName");
            this.Password = reader.GetString("password");
            this.QQ = reader.GetString("qq");
            this.WX = reader.GetString("wx");
            this.MobilePhone = reader.GetString("mobilePhone");
            this.LoginType = reader.GetInt32("loginType");
            this.UID = reader.GetString("uid");
            this.Role1 = reader.GetString("role1");
            this.Role2 = reader.GetString("role2");
            this.Role3 = reader.GetString("role3");
        }
    }
}
