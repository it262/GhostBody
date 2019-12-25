using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{
    GameObject button1;
    GameObject button2;
    GameObject button3;

    UpButton script1;
    UpButton script2;
    UpButton script3;

    bool is1 = true;
    bool is2 = false;
    bool is3 = false;


    // Start is called before the first frame update
    public void Check()
    {
        button1 = GameObject.Find("ButtonUp");
        button2 = GameObject.Find("ButtonUp2");
        button3 = GameObject.Find("ButtonUp3");

        script1 = button1.GetComponent<UpButton>();
        script2 = button2.GetComponent<UpButton>();
        script3 = button3.GetComponent<UpButton>();
        Debug.Log(script1.number);

        switch (script1.number)
        {
         
            case 1:
                is1 = true;
                break;
            case 2:
                is1 = false;
                break;
            case 3:
                is1 = false;
                break;
            default:
 
                break;

        }

        switch (script2.number)
        {
          
            case 1:
                is2 = false;
                break;
            case 2:
                is2 = true;
                break;
            case 3:
                is2 = false;
                break;
            default:
         
                break;

        }

        switch (script3.number)
        {
         
             
            case 1:
                is3 = false; ;
                break;
            case 2:
                is3 = false;
                break;
            case 3:
                is3 = true; ;
                break;
            default:

                break;

        }

        if(is1 && is2 && is3)
        {
            Debug.Log("answer is collect");
        }


    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
