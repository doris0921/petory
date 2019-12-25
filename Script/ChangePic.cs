using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePic : MonoBehaviour
{
    Image myImageComponent;
    public Sprite NormalImage;
    public Sprite SadImage;
    public Sprite HappyImage;

    TimeControl timer;
    void Start() //Lets start by getting a reference to our image component.
    {
        myImageComponent = GetComponent<Image>(); //Our image component is the one attached to this gameObject.
        timer = GameObject.Find("board").GetComponent<TimeControl>();
    }

    void Update()
    {
        if (timer.fillAmount < 30)
        {
            myImageComponent.sprite = SadImage;
        }
        else if (timer.fillAmount < 60)
            myImageComponent.sprite = NormalImage;
        else
            myImageComponent.sprite = HappyImage;
    }
}

