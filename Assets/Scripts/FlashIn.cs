using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashIn : MonoBehaviour
{
    Vector3 bigScale;
    Vector3 smallScale;
    public float lerpSpeed = 20.0f;
    public bool play = false;
    // Start is called before the first frame update
    void Start()
    {
        play = false;
        smallScale = transform.localScale;
        bigScale = smallScale * 100.0f;
        transform.localScale = bigScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (play == true)   
            transform.localScale = Vector3.Lerp(transform.localScale, smallScale, lerpSpeed * Time.unscaledDeltaTime);
    }
}
