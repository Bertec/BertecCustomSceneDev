// Build helper for the custom scene.
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0051 // Remove unused private members


class Autobuilder
{
    public static readonly string[] scenes = {
    "Assets/Scenes/Startup/StartupScene.unity",
    "Assets/Scenes/StaticScene/StaticScene.unity",
    "Assets/Scenes/VisualFlow/VisualFlow.unity",
    "Assets/Scenes/ClosedRoom/ClosedRoom.unity",
    };

    static private string outputFolder()
    {
        return Path.GetFullPath(Path.Combine(Application.dataPath, "..", "BuildOutput"));
    }

    static private string windowsPath()
    {
        return Path.GetFullPath(Path.Combine(Application.dataPath, "..", "BuildOutput"));
    }

    static void SwitchToAndroid()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
    }
    static void SwitchToWindows()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Build/Build for HMD")]
    static void InstallBuild() // called from the Unity Build Script during the install build process
    {
        SwitchToAndroid();
        Bertec.BuildServices.BuildProject(scenes, outputFolder());
    }

    [MenuItem("Build/Build for Immersive Dome")]
    static void Win64Build()
    {
        SwitchToWindows();
        Bertec.BuildServices.BuildProject(scenes, windowsPath(), UnityEngine.Application.productName); // this needs to also match the package name
    }
}

