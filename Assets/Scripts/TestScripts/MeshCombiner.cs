using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MeshCombiner : MonoBehaviour
{

#if UNITY_EDITOR


  public GameObject generatedObject = null;

  [ContextMenu("Export")]
  void Init()
  {
    MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(true);
    Collider[] colliders = GetComponentsInChildren<Collider>(true);
    Dictionary<Material, List<CombineInstance>> combineMeshInstanceDictionary = new Dictionary<Material, List<CombineInstance>>();

    foreach (var mesh in meshFilters)
    {

      var mat = mesh.GetComponent<Renderer>().sharedMaterial;

      if (mat == null)
        continue;

      if (!combineMeshInstanceDictionary.ContainsKey(mat))
      {
        combineMeshInstanceDictionary.Add(mat, new List<CombineInstance>());
      }
      var instance = combineMeshInstanceDictionary[mat];
      var cmesh = new CombineInstance();
      cmesh.transform = mesh.transform.localToWorldMatrix;
      cmesh.mesh = ((MeshFilter)mesh).sharedMesh;
      instance.Add(cmesh);
    }

    

    gameObject.SetActive(false);
    gameObject.tag = "EditorOnly";


    if (generatedObject == null)
      generatedObject = new GameObject(name);

    foreach (var item in generatedObject.GetComponentsInChildren<Transform>())
    {
      if (item == generatedObject.transform)
        continue;

      DestroyImmediate(item.gameObject);
    }

    generatedObject.isStatic = true;

    foreach (var dic in combineMeshInstanceDictionary)
    {

      var newObject = new GameObject(dic.Key.name);
      newObject.isStatic = true;

      var meshrenderer = newObject.AddComponent<MeshRenderer>();
      var meshfilter = newObject.AddComponent<MeshFilter>();

      meshrenderer.material = dic.Key;
      var mesh = new Mesh();
      mesh.CombineMeshes(dic.Value.ToArray());
      Unwrapping.GenerateSecondaryUVSet(mesh);
      meshfilter.sharedMesh = mesh;
      newObject.transform.parent = generatedObject.transform;

      Debug.Log(Application.loadedLevelName);
      /*
      System.IO.Directory.CreateDirectory("Assets/" + Application.loadedLevelName + "/" + name);
      AssetDatabase.CreateAsset(mesh, "Assets/" + Application.loadedLevelName + "/" + name + "/" + dic.Key.name + ".asset");
      */
    }

    var collidersObject = new GameObject("colliders");
    collidersObject.transform.parent = generatedObject.transform;

    foreach (var collider in colliders)
    {
      var c = collider.gameObject;
      var g = Instantiate(c, c.transform.position, c.transform.rotation);
      g.transform.localScale = c.transform.lossyScale;
      g.transform.parent = collidersObject.transform;
    }
  }

  void OnEnable()
  {
    if (generatedObject != null)
    {
      generatedObject.SetActive(false);
    }
  }

  void OnDisable()
  {
    if (generatedObject != null)
    {
      generatedObject.SetActive(true);
    }
  }
#endif
}