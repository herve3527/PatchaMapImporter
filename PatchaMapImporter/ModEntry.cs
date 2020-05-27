namespace PatchaMapImporter
{
    using Tools;
    using UnityEngine;
    using UnityModManagerNet;

    /// <summary>
    /// UnityModManager Mod Entry
    /// </summary>
    static class ModEntry
    {
        /// <summary>
        /// Load function
        /// </summary>
        /// <param name="modEntry"></param>
        /// <returns></returns>
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            Log.Write("Loading Module");
            new GameObject().AddComponent<PatchaMapImporter>();
            return true;
        }
    }
}
