using UnityEditor;
using UnityEngine;
using Editor;

[CustomEditor(typeof(BombExplosionSounds))]
public class CustomInspectorBombExplosion : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        BombExplosionSounds script = (BombExplosionSounds)target;
        base.OnInspectorGUI();
        GUILayout.Space(10);
        if (GUILayout.Button("Generate random pitch"))
        {
            Debug.Log($"{script.PitchRange.GetRandomValue(script.Random)}");
        }
    }
}
