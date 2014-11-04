/*Copyright 2011 Arash Norouzi

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

namespace NewsApp.Notifications.NotUser
{
    //public class NotificationPayload : INotificationPayload
    //{
    //    public NotificationPayload(string deviceToken)
    //    {
    //        DeviceToken = deviceToken;
    //        Alert = new NotificationAlert();
    //        CustomItems = new Dictionary<string, object[]>();
    //    }

    //    public NotificationPayload(string deviceToken, string alert)
    //    {
    //        DeviceToken = deviceToken;
    //        Alert = new NotificationAlert {Body = alert};
    //        CustomItems = new Dictionary<string, object[]>();
    //    }

    //    public NotificationPayload(string deviceToken, string alert, int badge)
    //    {
    //        DeviceToken = deviceToken;
    //        Alert = new NotificationAlert {Body = alert};
    //        Badge = badge;
    //        CustomItems = new Dictionary<string, object[]>();
    //    }

    //    public NotificationPayload(string deviceToken, string alert, int badge, string sound)
    //    {
    //        DeviceToken = deviceToken;
    //        Alert = new NotificationAlert {Body = alert};
    //        Badge = badge;
    //        Sound = sound;
    //        CustomItems = new Dictionary<string, object[]>();
    //    }

    //    public NotificationAlert Alert { get; set; }

    //    public string DeviceToken { get; set; }

    //    public int? Badge { get; set; }

    //    public string Sound { get; set; }

    //    public int PayloadId { get; set; }

    //    public Dictionary<string, object[]> CustomItems { get; private set; }

    //    public void AddCustom(string key, params object[] values)
    //    {
    //        if (values != null)
    //            CustomItems.Add(key, values);
    //    }

    //    public string ToJson()
    //    {
    //        var json = new JObject();

    //        var aps = new JObject();

    //        if (!Alert.IsEmpty)
    //        {
    //            if (!string.IsNullOrEmpty(Alert.Body)
    //                && string.IsNullOrEmpty(Alert.LocalizedKey)
    //                && string.IsNullOrEmpty(Alert.ActionLocalizedKey)
    //                && (Alert.LocalizedArgs == null || Alert.LocalizedArgs.Count <= 0))
    //            {
    //                aps["alert"] = new JValue(Alert.Body);
    //            }
    //            else
    //            {
    //                var jsonAlert = new JObject();

    //                if (!string.IsNullOrEmpty(Alert.LocalizedKey))
    //                    jsonAlert["loc-key"] = new JValue(Alert.LocalizedKey);

    //                if (Alert.LocalizedArgs != null && Alert.LocalizedArgs.Count > 0)
    //                    jsonAlert["loc-args"] = new JArray(Alert.LocalizedArgs.ToArray());

    //                if (!string.IsNullOrEmpty(Alert.Body))
    //                    jsonAlert["body"] = new JValue(Alert.Body);

    //                if (!string.IsNullOrEmpty(Alert.ActionLocalizedKey))
    //                    jsonAlert["action-loc-key"] = new JValue(Alert.ActionLocalizedKey);

    //                aps["alert"] = jsonAlert;
    //            }
    //        }

    //        if (Badge.HasValue)
    //            aps["badge"] = new JValue(Badge.Value);

    //        if (!string.IsNullOrEmpty(Sound))
    //            aps["sound"] = new JValue(Sound);


    //        json["aps"] = aps;

    //        foreach (string key in CustomItems.Keys)
    //        {
    //            if (CustomItems[key].Length == 1)
    //                json[key] = new JValue(CustomItems[key][0]);
    //            else if (CustomItems[key].Length > 1)
    //                json[key] = new JArray(CustomItems[key]);
    //        }

    //        string rawString = json.ToString(Formatting.None, null);

    //        var encodedString = new StringBuilder();
    //        foreach (char c in rawString)
    //        {
    //            if (c < 32 || c > 127)
    //                encodedString.Append("\\u" + String.Format("{0:x4}", Convert.ToUInt32(c)));
    //            else
    //                encodedString.Append(c);
    //        }
    //        return rawString; // encodedString.ToString();
    //    }

    //    public override string ToString()
    //    {
    //        return ToJson();
    //    }
    //}
}