using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class CheckmarkMenuItemForServer
{
    private const string MENU_NAME = "TrivialKart/BuildOptions/Build with server";

    private static bool _enabled;

    // Called on load thanks to the InitializeOnLoad attribute
    static CheckmarkMenuItemForServer()
    {
        _enabled = EditorPrefs.GetBool(MENU_NAME, false);

        // Delaying until first editor tick so that the menu
        // will be populated before setting check state, and
        // re-apply correct action
        EditorApplication.delayCall += () => { PerformAction(_enabled); };
    }

    [MenuItem(MENU_NAME)]
    private static void ToggleAction()
    {
        // Toggling action
        PerformAction(!_enabled);
    }

    private static void PerformAction(bool enabled)
    {
        // Set checkmark on menu item
        Menu.SetChecked(MENU_NAME, enabled);
        // Saving editor state
        EditorPrefs.SetBool(MENU_NAME, enabled);

        _enabled = enabled;
        if (enabled)
        {
            Debug.Log(enabled);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ONLINE");
        }
        else
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "OFFLINE");
        }
    }
}
