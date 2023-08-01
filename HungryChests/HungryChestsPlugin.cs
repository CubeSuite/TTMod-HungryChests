using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using HungryChests.Patches;
using UnityEngine;

namespace HungryChests
{
    // TODO Review this file and update to your own requirements.

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class HungryChestsPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.equinox.HungryChests";
        private const string PluginName = "HungryChests";
        private const string VersionString = "1.0.0";

        private const string VoidableItemsKey = "VoidableItems";
        public static ConfigEntry<string> VoidableItems;

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        private void Awake() {
            VoidableItems = Config.Bind("General", VoidableItemsKey, "Limestone,Plantmatter,Kindlevine Extract,Shiverthorn Extract", new ConfigDescription("Items that will be auto-voided from the last slot in a chest"));

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");
            Harmony.PatchAll();
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loaded.");
            Log = Logger;

            Harmony.PatchAll(typeof(InserterPatch));
        }
    }
}
