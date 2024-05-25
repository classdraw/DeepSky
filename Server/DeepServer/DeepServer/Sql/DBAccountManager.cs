using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepServer.Utils;
using MySql.Data.MySqlClient;
using DeepServer.Sql.Data;
using DeepServer.App;

namespace DeepServer.Sql
{
    public class DBAccountManager:Singleton<DBAccountManager>
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sqlData"></param>
        /// <returns></returns>
        public int Add(AccountSqlData sqlData) {
            MySqlConnection conn = MySqlConnectionPool.GetConnection();
            try
            {
                conn.Open();
                string dataStr = "'" + sqlData.LoginName + "','" + sqlData.Password + "','" + sqlData.QQ + "','" + sqlData.WX + "','" + sqlData.MobilePhone + "'," + sqlData.LoginType
                    + ",'" + sqlData.UID + "','" + sqlData.Role1 + "','" + sqlData.Role2 + "','" + sqlData.Role3 + "'";
                string sql = "insert into AccountTable(loginName,password,qq,wx,mobilePhone,loginType,uid,role1,role2,role3) values(" + dataStr + ");";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();//返回值是数据库中受影响的数据的行数


                string sql1 = "select id from AccountTable where loginName='" + sqlData.LoginName + "' and password='" + sqlData.Password + "';";
                cmd = new MySqlCommand(sql1, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                int result = 0;
                while (reader.Read())
                {

                    result = reader.GetInt32("id");
                }

                MySqlConnectionPool.ReleaseConnection(conn);
                return result;

            }
            catch (Exception ex)
            {
                MySqlConnectionPool.ReleaseConnection(conn);
                MyServer.LogError(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 是否存在用户名
        /// </summary>
        public bool Exists(string loginName)
        {
            MySqlConnection conn = MySqlConnectionPool.GetConnection();
            try
            {
                conn.Open();
                string sql = "select count(*) as count from AccountTable where loginName='" + loginName + "';";

                var cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                int result = 0;

                while (reader.Read())
                {

                    result = int.Parse(reader.GetValue(0).ToString());
                }
                MySqlConnectionPool.ReleaseConnection(conn);
                return result > 0;
            }
            catch (Exception ex)
            {
                MySqlConnectionPool.ReleaseConnection(conn);
                return false;
            }
        }


        /// <summary>
        /// 是否存在用户名和密码
        /// </summary>
        public bool Exists(string loginName, string password)
        {
            MySqlConnection conn = MySqlConnectionPool.GetConnection();
            try
            {
                conn.Open();
                string sql = "select count(*) as count from AccountTable where loginName='" + loginName + "' and password='" + password + "';";

                var cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                int result = 0;

                while (reader.Read())
                {

                    result = int.Parse(reader.GetValue(0).ToString());
                }
                MySqlConnectionPool.ReleaseConnection(conn);
                return result > 0;
            }
            catch (Exception ex)
            {
                MySqlConnectionPool.ReleaseConnection(conn);
                return false;
            }
        }


        /// <summary>
        /// 根据用户名和密码查询
        /// </summary>
        public AccountSqlData FindByNameAndPwd(string name, string password)
        {

            MySqlConnection conn = MySqlConnectionPool.GetConnection();

            try
            {

                conn.Open();
                string sql = "select * from AccountTable where loginName='" + name + "' and password='" + password + "';";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                AccountSqlData am = null;
                while (reader.Read())
                {
                    am = GetAccountModelByReader(reader);
                }


                MySqlConnectionPool.ReleaseConnection(conn);
                return am;
            }
            catch (Exception e)
            {
                MySqlConnectionPool.ReleaseConnection(conn);
                return null;
            }


        }


        /// <summary>
        /// 根据uid查询
        /// </summary>
        public AccountSqlData FindByUid(string uid)
        {
            MySqlConnection conn = MySqlConnectionPool.GetConnection();

            try
            {
                conn.Open();
                string sql = "select * from AccountTable where uid='" + uid + "'; ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                AccountSqlData am = null;
                while (reader.Read())
                {
                    am = GetAccountModelByReader(reader);
                }
                MySqlConnectionPool.ReleaseConnection(conn);
                return am;
            }
            catch (Exception e)
            {
                MySqlConnectionPool.ReleaseConnection(conn);
                return null;
            }

        }

        /// <summary>
        /// 找寻用户名相同的
        /// </summary>
        public AccountSqlData FindByName(string name)
        {
            MySqlConnection conn = MySqlConnectionPool.GetConnection();

            try
            {
                conn.Open();
                string sql = "select * from AccountTable where loginName='" + name + "'; ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                AccountSqlData am = null;
                while (reader.Read())
                {
                    am = GetAccountModelByReader(reader);
                }

                MySqlConnectionPool.ReleaseConnection(conn);
                return am;
            }
            catch (Exception e)
            {
                MySqlConnectionPool.ReleaseConnection(conn);
                return null;
            }

        }


        public int Update(AccountSqlData sqlData)
        {
            MySqlConnection conn = MySqlConnectionPool.GetConnection();
            try
            {
                conn.Open();
                string sql = "update  AccountTable set loginName='" + sqlData.LoginName + "',password='" + sqlData.Password + "',qq='" + sqlData.QQ + "',wx='" + sqlData.WX
                    + "',mobilePhone='" + sqlData.MobilePhone + "',loginType=" + sqlData.LoginType + ",role1='" + sqlData.Role1 + "',role2='" + sqlData.Role2 + "',role3='" + sqlData.Role3
                    + "' where uid='" + sqlData.UID + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                int result = cmd.ExecuteNonQuery();
                MySqlConnectionPool.ReleaseConnection(conn);
                return result;
            }
            catch (Exception e)
            {
                MySqlConnectionPool.ReleaseConnection(conn);
                return 0;
            }
        }

        /// <summary>
        /// 数据组装
        /// </summary>
        private AccountSqlData GetAccountModelByReader(MySqlDataReader reader)
        {

            AccountSqlData am = new AccountSqlData();
            am.SetSqlData(reader);
            return am;
        }
    }
}
