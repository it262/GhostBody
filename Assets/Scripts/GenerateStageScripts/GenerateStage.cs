using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateStage : MonoBehaviour
{
  [SerializeField] int x, y;
  [SerializeField] float width, height;
  [SerializeField] GameObject wall, floor;


  public void Generate()
  {
    if (wall == null || floor == null)
    {
      Debug.LogError("Please set prefebs.");
      return;
    }

    for (int i = this.transform.childCount - 1; i >= 0; --i)
    {
      DestroyImmediate(this.transform.GetChild(i).gameObject);
    }

    Quaternion q = transform.rotation;

    transform.eulerAngles = Vector3.zero;

    for (int i = 0; i <= x; i++)
    {
      if (i == 0 || i == x)
      {
        for (int j = 0; j < y; j++)
        {

          GameObject obj = Instantiate(wall, transform.position + new Vector3(i * width, 0, j * height), Quaternion.identity);
          obj.transform.parent = this.transform;
          obj.transform.Translate(0, 0, 1);
          if (i == 0)
          {
            obj.transform.Rotate(Vector3.up, 90f);
            if (j == 0)
            {
              GameObject obj2 = Instantiate(wall, transform.position + new Vector3(0, 0, j * height), Quaternion.identity);
              obj2.transform.parent = this.transform;
              obj2.transform.Translate(1, 0, 0);
            }
            if (j == y - 1)
            {
              GameObject obj2 = Instantiate(wall, transform.position + new Vector3(i * width, 0, j * height), Quaternion.identity);
              obj2.transform.parent = this.transform;
              obj2.transform.Translate(1, 0, 2);
              obj2.transform.Rotate(Vector3.up, 180f);
            }
          }
          if (i == x)
          {
            obj.transform.Rotate(Vector3.up, -90f);
          }
        }

      }
      else
      {
        GameObject right = Instantiate(wall, transform.position + new Vector3(i * width, 0, 0), Quaternion.identity);
        GameObject left = Instantiate(wall, transform.position + new Vector3(i * width, 0, y * width), Quaternion.identity);
        //right.transform.Rotate(Vector3.up, 90f);

        right.transform.parent = this.transform;
        left.transform.parent = this.transform;
        right.transform.Translate(1, 0, 0);
        left.transform.Translate(1, 0, 0);
        left.transform.Rotate(Vector3.up, 180f);
      }
    }

    Vector3 pre = new Vector3(1.05f, -2.1f, 1.05f);

    for (int i = 0; i < x; i++)
    {
      for (int j = 0; j < y; j++)
      {
        GameObject obj = Instantiate(floor, transform.position + pre + new Vector3(i * 2, 0, j * 2), Quaternion.identity);
        obj.transform.parent = this.transform;
      }
    }

    transform.rotation = q;
  }
}

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
