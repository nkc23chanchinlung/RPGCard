using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteGenerator))]
public class SpriteGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpriteGenerator generator = (SpriteGenerator)target;

        GUILayout.Space(10);

        if (GUILayout.Button("スプライト生成"))
        {
            generator.CreateSpriteObject();
        }
    }
}