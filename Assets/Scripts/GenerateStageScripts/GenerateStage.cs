using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateStage : MonoBehaviour
{
  //[SerializeField] int x, y;
  //[SerializeField] float width, height;
  [SerializeField, Header("Floor(0)")] GameObject floor;
  [SerializeField, Header("Wall(1)")] GameObject wall;
  [SerializeField, Header("Door(2)")] GameObject door;

  [SerializeField,TextArea(1,10)] string map;

  List<GameObject> prefabs;

  public void Generate2()
  {
    if(floor == null || wall == null || door == null)
    {
      Debug.LogError("Please set prefebs.");
      return;
    }

    List<GameObject> prefabs = new List<GameObject>{floor,wall,door};

    for (int i = this.transform.childCount - 1; i >= 0; --i)
    {
      DestroyImmediate(this.transform.GetChild(i).gameObject);
    }

    var sr = new StringReader(map);
    string line;
    int maxX = 0;
    int maxY = 0;
    while ((line = sr.ReadLine()) != null)
    {
      maxY = line.Length > maxY ? line.Length : maxY;
      maxX++;
    }

    int[,] vs = new int[maxX,maxY];
    int x = 0,y = 0;
    sr = new StringReader(map);
    while ((line = sr.ReadLine()) != null)
    {
      var cArray = line.ToCharArray();
      y = 0;
      for(int i=0; i<maxY; i++)
      {
        if(cArray.Length <= i || cArray[i] == ' ' || cArray[i] == '9') {
          vs[x, y] = -1;
        }
        else
        {
          vs[x, y] = (int)(cArray[i] - '0');
          
        }
        Debug.Log(vs[x, y]);
        y++;
      }
      x++;
    }

    Quaternion q = transform.rotation;
    transform.eulerAngles = Vector3.zero;

    Vector3 floor_prefix = new Vector3(1,0,1);
    GameObject[,] floors = new GameObject[maxX,maxY];
    float intervalX = 0, intervalY = 0 ;
    for(int i=0; i<maxX; i++)
    {
      intervalY = 0;
      for(int j=0; j<maxY; j++)
      {
        GameObject f;
        switch (vs[i, j])
        {
          case 0:
            f = Instantiate(prefabs[0], transform.position + floor_prefix + new Vector3(i * 2, 0, (j * 2) - intervalY), Quaternion.identity);
            f.transform.parent = this.transform;
            floors[i, j] = f;
            break;
          case 1:
          case 2:
            break;
          default:
            break;
        }
      }
    }

    float thickness = 0f;
    float thickness2 = 0.03f;
    for (int i = 0; i < maxX; i++)
    {
      for (int j = 0; j < maxY; j++)
      {
        switch (vs[i, j])
        {
          case 1:
            if (i - 1 >= 0 && floors[i - 1, j] != null)
            {
              GameObject w = Instantiate(prefabs[1], transform.position, Quaternion.identity);
              w.transform.SetPositionAndRotation(floors[i - 1, j].transform.position + new Vector3(1 + thickness, 2, 0), Quaternion.identity);
              w.transform.Rotate(Vector3.up, -90f);
              w.transform.parent = this.transform;
            }
            if (i + 1 < maxX && floors[i + 1, j] != null)
            {
              GameObject w = Instantiate(prefabs[1], transform.position, Quaternion.identity);
              w.transform.SetPositionAndRotation(floors[i + 1, j].transform.position + new Vector3(-1 - thickness, 2, 0), Quaternion.identity);
              w.transform.Rotate(Vector3.up, 90f);
              w.transform.parent = this.transform;
            }
            if (j - 1 > 0 && floors[i, j - 1] != null)
            {
              GameObject w = Instantiate(prefabs[1], transform.position, Quaternion.identity);
              w.transform.SetPositionAndRotation(floors[i, j - 1].transform.position + new Vector3(0, 2, 1 + thickness), Quaternion.identity);
              w.transform.Rotate(Vector3.up, 180f);
              w.transform.parent = this.transform;
            }
            if (j + 1 < maxY && floors[i, j + 1] != null)
            {
              GameObject w = Instantiate(prefabs[1], transform.position, Quaternion.identity);
              w.transform.SetPositionAndRotation(floors[i, j + 1].transform.position + new Vector3(0, 2, -1 - thickness), Quaternion.identity);
              w.transform.parent = this.transform;
            }
              break;
          case 2:
            if (i + 1 < maxX && vs[i + 1, j] == 2)
            {
              if (j - 1 > 0 && floors[i, j - 1] != null)
              {
                GameObject d = Instantiate(prefabs[2], transform.position, Quaternion.identity);
                d.transform.SetPositionAndRotation(floors[i, j - 1].transform.position + new Vector3(2, 2, 2), Quaternion.identity);
                d.transform.parent = this.transform;
              }
              else if(j + 1 < maxY && floors[i, j + 1] != null)
              {
                GameObject d = Instantiate(prefabs[2], transform.position, Quaternion.identity);
                d.transform.SetPositionAndRotation(floors[i, j + 1].transform.position + new Vector3(0, 2,-2), Quaternion.identity);
                d.transform.Rotate(Vector3.up, 180f);
                d.transform.parent = this.transform;
              }
            }
            if (j + 1 < maxY && vs[i, j + 1] == 2)
            {
              if (i - 1 >= 0 && floors[i - 1, j] != null)
              {
                GameObject d = Instantiate(prefabs[2], transform.position, Quaternion.identity);
                d.transform.SetPositionAndRotation(floors[i-1, j].transform.position + new Vector3(2, 2,2), Quaternion.identity);
                d.transform.Rotate(Vector3.up, -90f);
                d.transform.parent = this.transform;
              }
              else if (i + 1 < maxX && floors[i + 1, j] != null)
              {
                GameObject d = Instantiate(prefabs[2], transform.position, Quaternion.identity);
                d.transform.SetPositionAndRotation(floors[i+1, j].transform.position + new Vector3(-2, 2, 0), Quaternion.identity);
                d.transform.Rotate(Vector3.up, 90f);
                d.transform.parent = this.transform;
              }
            }
            break;
          default:
            break;
        }
      }
    }

        transform.rotation = q;
  }

  /*過去作（もう使わない？）
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
  
  */
}

[CustomEditor(typeof(GenerateStage))]
public class GenerateStageEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    GenerateStage generateStage = target as GenerateStage;
    /*
    if (GUILayout.Button("Generate"))
    {
      generateStage.Generate();
    }
    */

    if (GUILayout.Button("Generate"))
    {
      generateStage.Generate2();
    }
  }
}
