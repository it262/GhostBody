using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public NavMeshAgent enemy;
    public GameObject target;
    public GameObject loiPos; //徘徊時に目指す座標
    public Material eyematerial;

    System.Random r1 = new System.Random();
    int rand= 0;
    int exrand = 4;
    Vector3 eye = new Vector3();
   // float eyelong = 3.0f; //視線の距離
    float eyeex = 1.2f; //視線の幅
    Vector3 dir;
    Vector3 nordir1 ;
    Vector3 nordir2;
    Mesh mesh;

    Vector3[] loiteringPoints = new[]
    {
        //loiposのリスト
        new Vector3(3.5f,0.0f,4.0f),
        new Vector3(0.7f,0.0f,4.0f),
        new Vector3(-1.3f,0.0f,3.0f),
        new Vector3(-0.4f,0.0f,-0.1f),
        new Vector3(-3.8f,0.0f,1.4f)
    };

  
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        loiPos.transform.position= loiteringPoints[rand];
        Debug.Log("start");
        Debug.Log(loiteringPoints.Length);


        //視覚範囲表示メッシュ
        Vector3[] vertices = {
        new Vector3(-eyeex, 0, 0),
        new Vector3(-eyeex,  0, 2*eyeex),
        new Vector3( eyeex,  0 ,2*eyeex),
        new Vector3( eyeex, 0, 0)
        };

        int[] triangles = { 0, 1, 2, 0, 2, 3 };

        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial = eyematerial;

    }

    void Update()
    {
        if (target != null)
        {
            //視界にターゲットが入った場合
            
            if (CalcEye() )
            {
                //範囲内に入ったらプレイヤーを追いかける
                Debug.Log("mitukaru");
              enemy.destination = target.transform.position;
            }
            else
            {
                enemy.destination = loiPos.transform.position;
            }



        }
    }

    void OnTriggerEnter(Collider t)
    {
     if(t.gameObject.name == "tracepoint")
       {
            Debug.Log("hitPoint");
            exrand = rand;
            //loiPosの再抽選
            loiPos.transform.position = loiteringPoints[Lottery()] ;
        }
    }

    int Lottery()
    {  
        rand = r1.Next(0, loiteringPoints.Length);
        Debug.Log(rand);
        //連続で同じ値が出た場合の処理
        if(rand == exrand)
        {
            Debug.Log("再抽選");
            Lottery();
           
        }
        return rand;
    }

    bool CalcEye()
    {
        //視線ベクトルを取得 下を見るとｘ＝１　左を見ると　z＝１
        Vector3 targetdot = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y,
          target.transform.position.z - transform.position.z);
        if (targetdot.magnitude < eyeex * 2)
        {
            float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);
            float normDir1 = angleDir + Mathf.PI / 2;
            float normDir2 = angleDir - Mathf.PI / 2;
            float angleTar = Mathf.Atan2(targetdot.x, targetdot.z);
            float angleRote1 = angleTar - Mathf.PI / 2;
            float angleRote2 = angleTar + Mathf.PI / 2;

            dir = new Vector3(Mathf.Sin(angleDir), 0.0f, Mathf.Cos(angleDir));
            //視線ベクトルの９０度回転版
            nordir1 = new Vector3(Mathf.Sin(normDir1), 0.0f, Mathf.Cos(normDir1));
            nordir2 = new Vector3(Mathf.Sin(normDir2), 0.0f, Mathf.Cos(normDir2));


            //ベクトルを視線範囲の長さにする
            dir = dir * eyeex * 2;
            nordir1 *= eyeex;
            nordir2 *= eyeex;

            // Debug.Log(dir);
            //視線範囲内にターゲットがいるか
            //dirとの内積が正　かつその内積の値がdir以下　かつ　nordirの内積も同じ条件になる

            float dirdot = Vector3.Dot(dir, targetdot);
            float nordir1dot = Vector3.Dot(nordir1, targetdot);
            float nordir2dot = Vector3.Dot(nordir2, targetdot);

            if (dirdot > 0 && targetdot.magnitude * Mathf.Cos(angleTar) < eyeex * 2 &&
               ((nordir1dot > 0 && targetdot.magnitude * Mathf.Cos(angleRote1) < eyeex) || (nordir2dot > 0 && targetdot.magnitude * Mathf.Cos(angleRote2) < eyeex)))
            {
                Debug.Log("In!");
            }
            else
            {
                Debug.Log("out");
            }
            return true;
        }
        return false;
    }

}