using System;
using System.Text;
using UnityEngine;
using System.Diagnostics;
using JetBrains.Annotations;

namespace XEngine.Utilities{
    public static class XLogger
    {
        static private long lastLogTime;
        public const int LEVEL_ALL = -1;
        public const int LEVEL_TEMP = 0;
        public const int LEVEL_TEST = 1;
        public const int LEVEL_DEBUG = 2;
        public const int LEVEL_LOG = 3;
        public const int LEVEL_LOGIMPORT = 3;
        public const int LEVEL_WARN = 8;

        public const int LEVEL_SERVER = 9;
        public const int LEVEL_ERROR = 10;
        public const int LEVEL_NONE = 20;
        private static int LogLevel=XLogger.LEVEL_ALL;

        public static bool IsLogNone(){
            return LogLevel<=10;
        }
    #region 打印方法
        private static void LogWithColor(string message, Color color,int level)
        {
            if (level < LogLevel)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            long nowTime = DateTime.Now.Ticks / 10000;//ms

    // #if UNITY_EDITOR
            int colorInt = ((int)(color.r * 255f) << 16) | ((int)(color.g * 255f) << 8) | (int)(color.b * 255f);
            long offsetTime = lastLogTime > 0 ? nowTime - lastLogTime : 0;

            sb.Append("<color=#")
                .Append(colorInt.ToString("X6"))
                .Append(">")
                .Append("[")
                .Append(offsetTime)
                .Append("]")
                .Append("-->")
                .Append(message)
                .Append("</color>");
    // #else
    //             sb.Append(message);
    // #endif

            lastLogTime = nowTime;
            // #if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.Log(sb.ToString());
            // #endif
        }

        //public 
        // [Conditional("DEVELOPMENT_BUILD")]
        // [Conditional("UNITY_EDITOR")]
        public static void Log(string message, int layer = 1)
        {
            LogWithColor(message, Color.green, LEVEL_LOG);
            //AddLog(message);
        }

        // [Conditional("DEVELOPMENT_BUILD")]
        // [Conditional("UNITY_EDITOR")]
        public static void LogImport(string message, int layer = 1)
        {
            // Color color = new Color(0.1f,0.8f,0.8f);
            Color color = new Color(0.8f,0.1f,0.8f);
            LogWithColor(message, color, LEVEL_LOGIMPORT);
            //AddLog(message);
        }

        // [Conditional("DEVELOPMENT_BUILD")]
        // [Conditional("UNITY_EDITOR")]
        public static void LogDebug(string message)
        {
            if (LEVEL_DEBUG < LogLevel)
            {
                return;
            }
            UnityEngine.Debug.Log(message);
        }
        // [Conditional("UNITY_EDITOR")]
        public static void LogEditorError(string message)
        {
            LogError(message);
        }

        // [Conditional("UNITY_EDITOR")]
        public static void LogEditorWarn(string message)
        {
            LogWarn(message);
        }

        public static void LogError(string message)
        {
            if (LEVEL_ERROR < LogLevel)
            {
                return;
            }
            // #if UNITY_EDITOR
            long nowTime = DateTime.Now.Ticks / 10000;//ms
            long offsetTime = lastLogTime > 0 ? nowTime - lastLogTime : 0;
            lastLogTime = nowTime;
            UnityEngine.Debug.LogError("[" + offsetTime + "] Error:" + message);
            // #endif
        }

        
        public static void LogErrorFormat(string format, params object[] args)
        {
            string errorLog = string.Format(format, args);
            LogError(errorLog);
        }

        // [Conditional("DEVELOPMENT_BUILD")]
        // [Conditional("UNITY_EDITOR")]
        public static void LogWarn(string message, int layer = 1)
        {
            LogWithColor(message, Color.yellow, LEVEL_WARN);
        }

        // [Conditional("UNITY_EDITOR")]
        public static void LogTest(string message, int layer = 1)
        {
            LogWithColor(message, Color.magenta, LEVEL_TEST);
        }

        // [Conditional("UNITY_EDITOR")]
        public static void LogTemp(string message, int layer = 1)
        {
            Color color = new Color(0.4f,0.6f,1);
            LogWithColor(message, color, LEVEL_TEMP);
        }

        //服务器日志输出
        // [Conditional("UNITY_SERVER")]
        // [Conditional("UNITY_EDITOR")]
        public static void LogServer(string message, int layer = 1)
        {
            LogWithColor("Server:"+message, Color.yellow, LEVEL_SERVER);
        } 


    #endregion


    }
}
