using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 0.5f;
    public float startPosition;
    public float endPosition;
    
    // Update is called once per frame
    void Update()
    {
        //move x position per 1second
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0); //scroll

        //check if the scroll get end position
        if (transform.position.x <= endPosition) ScrollEnd();//scroll end
    }

    void ScrollEnd()
    {
        //get back position to start position
        transform.Translate(-1 * (endPosition - startPosition), 0, 0);

        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }
}
