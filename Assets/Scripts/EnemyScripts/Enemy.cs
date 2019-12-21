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
    System.Random r1 = new System.Random();
    int rand= 0;
    int exrand = 4;
    Vector3 eye = new Vector3();
    Vector3 eyelong = new Vector3(1.0f, 1.2f, 1.0f);
    Vector3 eyedir = new Vector3(0,0,0);
    

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


    }

    void Update()
    {
        if (target != null)
        {
            //視界にターゲットが入った場合
            eyedir.x = (float)Math.Cos(enemy.transform.rotation.y);
            eyedir.y = (float)Math.Sin(enemy.transform.rotation.y);
            eye = enemy.transform.position + eyedir;
            if (target.transform.position.x < eye.x+1.2 && target.transform.position.x > eye.x - 1.2 && 
                target.transform.position.y < eye.y + 1.2 && target.transform.position.y > eye.y - 1.2  )
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

}