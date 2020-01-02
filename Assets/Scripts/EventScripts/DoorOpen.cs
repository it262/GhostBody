using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorEvent()
    {
        if (!isOpen)
        {
            transform.Rotate(0, 90, 0);
            isOpen = true;
        }
        else
        {
            transform.Rotate(0, -90, 0);
            isOpen = false;
        }
    }
}
