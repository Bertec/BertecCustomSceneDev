using Bertec;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PassThroughViewContainer))]
internal sealed class PassThroughViewContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PassthroughEditorPreviewUI.DrawControls();
    }
}

[CustomEditor(typeof(VisualFlow))]
internal sealed class VisualFlowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PassthroughEditorPreviewUI.DrawControls();
    }
}

internal static class PassthroughEditorPreviewUI
{
    public static void DrawControls()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Passthrough Preview", EditorStyles.boldLabel);

        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox(
                "Enter Play mode to test passthrough / idle mode in the Unity Editor without taking a build.",
                MessageType.Info);
            return;
        }

        EditorGUILayout.HelpBox(
            $"Requested: {(SystemDisplayDeviceManager.PassThroughEnabled ? "On" : "Off")} | " +
            $"Active: {(SystemDisplayDeviceManager.IsPassthrough ? "On" : "Off")}",
            MessageType.None);

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Enter Idle Preview"))
            {
                PassthroughEditorPreview.SetState(true);
            }

            if (GUILayout.Button("Exit Idle Preview"))
            {
                PassthroughEditorPreview.SetState(false);
            }
        }
    }
}

internal static class PassthroughEditorPreview
{
    [MenuItem("Tools/Bertec/Passthrough Preview/Enter Idle Preview")]
    private static void EnterIdlePreview()
    {
        SetState(true);
    }

    [MenuItem("Tools/Bertec/Passthrough Preview/Enter Idle Preview", true)]
    private static bool ValidateEnterIdlePreview()
    {
        return Application.isPlaying && !SystemDisplayDeviceManager.PassThroughEnabled;
    }

    [MenuItem("Tools/Bertec/Passthrough Preview/Exit Idle Preview")]
    private static void ExitIdlePreview()
    {
        SetState(false);
    }

    [MenuItem("Tools/Bertec/Passthrough Preview/Exit Idle Preview", true)]
    private static bool ValidateExitIdlePreview()
    {
        return Application.isPlaying && (SystemDisplayDeviceManager.PassThroughEnabled || SystemDisplayDeviceManager.IsPassthrough);
    }

    public static void SetState(bool enabled)
    {
        if (SystemDisplayDeviceManager.PassThroughEnabled == enabled &&
            SystemDisplayDeviceManager.IsPassthrough == enabled)
        {
            Debug.Log($"[Passthrough Preview] Idle preview is already {(enabled ? "entered" : "exited")}.");
            return;
        }

        SystemDisplayDeviceManager.PassThroughEnabled = enabled;
        SystemDisplayDeviceManager.PassthroughChanged(enabled);

        Debug.Log($"[Passthrough Preview] {(enabled ? "Entered" : "Exited")} idle preview in Editor play mode.");
    }
}
