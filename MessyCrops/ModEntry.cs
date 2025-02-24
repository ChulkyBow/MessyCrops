﻿using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;

namespace MessyCrops
{
    public class ModEntry : Mod, IAssetLoader
    {
        internal static ITranslationHelper i18n => helper.Translation;
        internal static IMonitor monitor;
        internal static IModHelper helper;
        internal static Harmony harmony;
        internal static string ModID;
        internal static Config config;

        public override void Entry(IModHelper helper)
        {
            Monitor.Log("Starting up...", LogLevel.Debug);

            monitor = Monitor;
            ModEntry.helper = Helper;
            harmony = new(ModManifest.UniqueID);
            ModID = ModManifest.UniqueID;
            config = helper.ReadConfig<Config>();

            helper.Events.GameLoop.ReturnedToTitle += (s, e) => CropPatch.offsets.Clear();
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;

            harmony.PatchAll();
        }

        public void OnGameLaunched(object s, GameLaunchedEventArgs ev)
        {
            config.RegisterConfig(ModManifest);
            HoeDirtPatch.Setup();
        }

        public bool CanLoad<T>(IAssetInfo asset)
        {
            return false;
        }

        public T Load<T>(IAssetInfo asset)
        {
            return default;
        }
    }
}
