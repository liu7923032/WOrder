using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api;
using cn.jpush.api.device;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;

namespace WOrder.Extension
{
    public class JPushHelper
    {
        private readonly JPushClient client;
        public JPushHelper()
        {
            client = new JPushClient("6c9ba9e57db14ea5d3c248a6", "2e333c3f084915142a9c5b76");
        }
        /// <summary>
        /// 指定aliasIds
        /// </summary>
        /// <param name="content"></param>
        /// <param name="aliasIds"></param>
        /// <returns></returns>
        public async Task<bool> PushToAlias(string title, string content, List<string> aliasIds)
        {

            List<string> aliasList = new List<string>();
            foreach (var item in aliasIds)
            {
                AliasDeviceListResult aliasDevice = client.getAliasDeviceList(item, "android");
                if (aliasDevice.registration_ids.Count > 0)
                {
                    aliasList.Add(item);
                }
            }
            if (aliasList.Count == 0)
            {
                return await Task.FromResult(true);
            }

            PushPayload pushPayload = CreatePayload(title, content, null, aliasList.ToArray());

            MessageResult response = client.SendPush(pushPayload);
            return await Task.FromResult(response.isResultOK());

        }



        private PushPayload CreatePayload(string title, string content, string[] tags = null, string[] alias = null)
        {
            var result = new PushPayload()
            {
                platform = Platform.android(),
                notification = Notification.android(content, title)
            };
            if (tags != null && tags.Length > 0)
            {
                result.audience = Audience.s_tag_and(tags);
            }

            if (alias != null && alias.Length > 0)
            {
                result.audience = Audience.s_alias(alias);
            }
            return result;

        }

    }
}
