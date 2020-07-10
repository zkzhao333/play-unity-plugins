using UnityEditor;
using UnityEngine;
[InitializeOnLoad]
public static class CheckmarkMenuItemForServer {

    private const string MENU_NAME = "TrivialKart/BuildOptions/Build with server";

    private static bool enabled_;
    /// Called on load thanks to the InitializeOnLoad attribute
    static CheckmarkMenuItemForServer() {
        CheckmarkMenuItemForServer.enabled_ = EditorPrefs.GetBool(CheckmarkMenuItemForServer.MENU_NAME, false);

        /// Delaying until first editor tick so that the menu
        /// will be populated before setting check state, and
        /// re-apply correct action
        EditorApplication.delayCall += () => {
            PerformAction(CheckmarkMenuItemForServer.enabled_);
        };
    }

    [MenuItem(CheckmarkMenuItemForServer.MENU_NAME)]
    private static void ToggleAction() {

        /// Toggling action
        PerformAction( !CheckmarkMenuItemForServer.enabled_);
    }

    public static void PerformAction(bool enabled) {

        /// Set checkmark on menu item
        Menu.SetChecked(CheckmarkMenuItemForServer.MENU_NAME, enabled);
        /// Saving editor state
        EditorPrefs.SetBool(CheckmarkMenuItemForServer.MENU_NAME, enabled);

        CheckmarkMenuItemForServer.enabled_ = enabled;
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