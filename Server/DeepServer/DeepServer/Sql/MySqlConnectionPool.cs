using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DeepServer.Sql
{
    public class MySqlConnectionPool
    {
        private static object _locker = new object();
        private static List<MySqlConnection> _conns = new List<MySqlConnection>();
        private static string _connectStr = "server=127.0.0.1;port=3306;database=really;user=root;password=root;";
        public static MySqlConnection GetConnection()
        {
            lock (_locker)
            {
                if (_conns != null && _conns.Count > 0)
                {
                    for (int i = 0; i < _conns.Count; i++)
                    {
                        if (_conns[i].State == System.Data.ConnectionState.Closed)
                        {
                            return _conns[i];
                        }
                    }
                }

                var conn = new MySqlConnection(_connectStr);
                _conns.Add(conn);
                return conn;
            }

        }

        public static void ReleaseConnection(MySqlConnection conn)
        {
            lock (_locker)
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
