using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMoveScript : MonoBehaviour
{
  CharacterController controller;
  [SerializeField] float moveSpeed, rotateSpeed;

  const float GRAVITY = 9.8f;

  // Start is called before the first frame update
  void Start()
  {
    controller = GetComponent<CharacterController>();
  }

  // Update is called once per frame
  void Update()
  {
    //CharacterControllerの移動方向のベクトル
    Vector3 move;

    //上方向ベクトルを軸に入力に応じてrotateSpeedで回転
    transform.Rotate(transform.up * Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
    //前方向ベクトルへ入力に応じてmoveSpeedで移動
    move = transform.forward * Input.GetAxis("Vertical") * moveSpeed;
    //接地していなかったら落ちる
    if (!controller.isGrounded)
      move.y -= GRAVITY;

    controller.Move(move * Time.deltaTime);
  }
}
