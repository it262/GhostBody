using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateStage))]
public class GenerateStageEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    GenerateStage generateStage = target as GenerateStage;
    if (GUILayout.Button("Generate"))
    {
      generateStage.Generate();
    }
  }
}
