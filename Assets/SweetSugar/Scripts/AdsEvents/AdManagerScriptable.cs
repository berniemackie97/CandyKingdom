using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
//    [CreateAssetMenu(fileName = "AdManagerScriptable", menuName = "AdManagerScriptable", order = 1)]
    public class AdManagerScriptable : ScriptableObject
    {
        public List<AdEvents> adsEvents = new List<AdEvents>();
        public string admobUIDAndroid;
        public string admobUIDIOS;
        public string admobRewardedUIDAndroid;
        public string admobRewardedUIDIOS;
    }
}