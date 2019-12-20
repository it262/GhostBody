using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームオブジェクトに必須なコンポーネント（自動付与）
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombineObjects : MonoBehaviour
{
  //アタッチしてる子のゲームオブジェクト全てを統合して１つのメッシュにする
  //本番のステージは事前に統合するのでテスト用
  void Start()
  {
    MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
    CombineInstance[] combine = new CombineInstance[meshFilters.Length];

    int i = 0;
    while (i < meshFilters.Length)
    {
      combine[i].mesh = meshFilters[i].sharedMesh;
      combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
      meshFilters[i].gameObject.SetActive(false);

      i++;
    }
    transform.GetComponent<MeshFilter>().mesh = new Mesh();
    transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
    GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
    transform.gameObject.SetActive(true);
  }
}
