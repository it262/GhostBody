using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEvent : MonoBehaviour
{

    // Start is called before the first frame update
    bool isClick;
    void Start()
    {
        this.GetComponent<Canvas>().enabled = false;
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTextEvent()
    {
        if (!isClick)
        {
            this.GetComponentInChildren<Text>().text = "これは四角です。もう一度クリックするとUIが消えます。";
            this.GetComponent<Canvas>().enabled = true;
            isClick = true;
        }
        else
        {
            this.GetComponent<Canvas>().enabled = false;
            isClick = false;
        }
      
    }
}
