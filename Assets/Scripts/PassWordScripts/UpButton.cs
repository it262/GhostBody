using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpButton : MonoBehaviour
{
    public GameObject numberImage;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    int counter = 0;
    public int number = 1;
 

    // Start is called before the first frame update
    public void changeSprite()
    {
        counter++;
        number = counter % 3 + 1;
        switch (number)
        {
            case 1:
                numberImage.GetComponent<Image>().sprite = sprite1;
                break;
            case 2:
                numberImage.GetComponent<Image>().sprite = sprite2;
                break;
            case 3:
                numberImage.GetComponent<Image>().sprite = sprite3;
                break;
            default:               
                break;

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
