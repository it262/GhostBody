using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateStairs : MonoBehaviour
{
  [SerializeField] Transform floorPos;
  [SerializeField] int height;
  [SerializeField] GameObject stair;


  public void Generate()
  {
    if (stair == null)
    {
      Debug.LogError("Please set prefebs.");
      return;
    }

    if (floorPos == null)
      floorPos = transform;

    for (int i = this.transform.childCount - 1; i >= 0; --i)
    {
      DestroyImmediate(this.transform.GetChild(i).gameObject);
    }

    Quaternion q = transform.rotation;

    transform.eulerAngles = Vector3.zero;

    this.transform.position = floorPos.position + new Vector3(0, 1f, 0);
    GameObject obj = Instantiate(stair, transform.position, Quaternion.identity);
    obj.transform.parent = this.transform;
    Vector3 pre = obj.transform.position;

    for (int i=1; i<height; i++)
    {
      obj = Instantiate(stair, pre + new Vector3(0,2,-2), Quaternion.identity);
      obj.transform.parent = this.transform;
      pre = obj.transform.position;
    }

    transform.rotation = q;
  }
}

[CustomEditor(typeof(GenerateStairs))]
public class GenerateStairsEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    GenerateStairs generateStage = target as GenerateStairs;
    if (GUILayout.Button("Generate"))
    {
      generateStage.Generate();
    }
  }
}