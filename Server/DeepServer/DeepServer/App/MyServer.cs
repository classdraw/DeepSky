using System;
using System.Collections.Generic;
using Photon.SocketServer;
using log4net;
using log4net.Config;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using System.IO;

namespace DeepServer.App
{
    public class MyServer : ApplicationBase
    {
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new MyClient(initRequest);
        }

        protected override void Setup()
        {
            this.InitLogging();
            MyServer.Log("服务器启动！！！");

            var global = Global.GetInstance();
        }

        protected override void TearDown()
        {
            MyServer.Log("服务器关闭！！！");        }



        private static int MinLogLevel = 0;
        private static readonly ILogger LogObj = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public static void Log(object obj)
        {
            if (MinLogLevel <= 1 && obj != null)
            {
                LogObj.Info(obj.ToString());
            }
        }


        public static void LogWarn(object obj)
        {
            if (MinLogLevel <= 2 && obj != null)
            {
                LogObj.Warn(obj.ToString());
            }
        }

        public static void LogError(object obj)
        {
            if (MinLogLevel <= 3 && obj != null)
            {
                LogObj.Error(obj.ToString());
            }
        }


        protected virtual void InitLogging()
        {
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = "" + this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
        }
    }
}
