using UnityEngine;
using UnityEditor;

public class OnlineOfflineMenu : MonoBehaviour
{
    [MenuItem("Offline-Online/Enable online functionality")]
    static void DefineOnline()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ONLINE");
    }

    [MenuItem("Offline-Online/Enable offline functionality")]
    static void DefineOffline()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "OFFLINE");
    }
}