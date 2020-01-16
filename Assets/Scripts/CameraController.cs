using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] Transform startPosition;

  void Start()
  {
    Camera.main.transform.position = startPosition.position;
  }
  //カメラは常にプレイヤーの方向を向く（現段階）
  void Update()
  {
    Camera.main.transform.LookAt(transform.position);
  }

  //衝突したColliderの親（カメラの位置）へカメラを移動させる
  private void OnTriggerEnter(Collider other)
  {
    Camera.main.transform.position = other.transform.parent.transform.position;
  }

  public void SetCameraPosition(Transform t)
  {
    Camera.main.transform.position = t.position;
  }
}
