using System;
using System.Collections.Generic;
using DeepServer.App;

namespace DeepServer.Model
{
    /// <summary>
    /// 所有登录玩家的缓存器
    /// </summary>
    public class AccountModule:BaseModule
    {
        private Dictionary<MyClient, AccountData> m_kClients = new Dictionary<MyClient, AccountData>();
        private Dictionary<string, AccountData> m_kAccounts = new Dictionary<string, AccountData>();

        public void AddAccount(AccountData ad) {
            if (!m_kAccounts.ContainsKey(ad.UID)) {
                m_kAccounts.Add(ad.UID,ad);
            }

            if (!m_kClients.ContainsKey(ad.Client)) {
                m_kClients.Add(ad.Client,ad);
            }
        }

        public void RemoveAccount(string uid)
        {
            MyClient client = null;
            if (m_kAccounts.ContainsKey(uid))
            {
                client = m_kAccounts[uid].Client;
                m_kAccounts.Remove(uid);
            }

            if (client != null && m_kClients.ContainsKey(client))
            {
                m_kClients.Remove(client);
            }
        }

        public bool HasAccount(string uid)
        {
            return m_kAccounts.ContainsKey(uid);
        }

        public AccountData GetAccount(string uid)
        {
            if (m_kAccounts.ContainsKey(uid))
            {
                return m_kAccounts[uid];
            }
            return null;
        }
        protected override void SelfRelease() {

        }
    }
}
