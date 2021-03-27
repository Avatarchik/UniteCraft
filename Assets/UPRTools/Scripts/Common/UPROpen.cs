using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

namespace UPRProfiler.Common
{
    public class UPROpen
    {
        /***
         * subjectName is the name of subject
         * groupName is the name of group，subset of subject in upr report
         * chartType now only support "line"
         * data is the customizedData need to display, limited with type float, double, int, long etc...
         * example:
         *    UPROpen.SendCustomizedData("SceneSubject", "LoadSceneGroup", "line", new Dictionary<string, string){{"LoadSceneTime","0.56"}, {"LoadSceneAsset","0.23"}};        
         */
        private static StringBuilder builder = new StringBuilder();
        public static void SendCustomizedData(String subjectName, String groupName, String chartType, Dictionary<string, string> data)
        {
            Profiler.BeginSample("Profiler.UPRCustomizeData");
            chartType = "line";
            if (!NetworkServer.isConnected)
                return;
#if UNITY_2018_2_OR_NEWER
            builder.Clear();
#else
            builder = new StringBuilder();
#endif

            builder.Append(subjectName);
            builder.Append("|");
            builder.Append(groupName);
            builder.Append("|");
            builder.Append(chartType);
            builder.Append("|");
            builder.Append("{");
            foreach (var item in data)
            {
                builder.AppendFormat(" \"{0}\":\"{1}\",", item.Key, item.Value);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("}");
            Debug.Log(builder.ToString());
            UPRMessage sample = new UPRMessage
            {
                rawBytes = Encoding.ASCII.GetBytes(builder.ToString()),
                type = 3
            };
            NetworkServer.SendMessage(sample);
            Profiler.EndSample();
        }
    }
}