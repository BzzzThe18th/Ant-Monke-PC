using HarmonyLib;
using System;
using System.Reflection;

namespace AntMonkePC
{
    public class HamburgerPickles
    {
        private static Harmony instance;

        public static bool IsPatched { get; private set; }
        public const string InstanceId = PluginInfo.GUID;

        internal static void ExtraPickles()
        {
            if (!IsPatched)
            {
                if (instance == null)
                {
                    instance = new Harmony(InstanceId);
                }

                instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
            }
        }

        internal static void NoPicklesPls()
        {
            if (instance != null && IsPatched)
            {
                instance.UnpatchSelf();
                IsPatched = false;
            }
        }
    }
}
