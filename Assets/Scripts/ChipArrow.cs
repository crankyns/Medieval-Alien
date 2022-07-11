using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipArrow : MonoBehaviour
{

    Vector3 startPos;

    float t = 0;
    void Start()
    {
        startPos = transform.position;
    }


    void Update()
    {
        transform.Rotate(new Vector3(15 * Time.deltaTime, 0, 0));
        t += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, startPos + Vector3.down * 0.5f, Mathf.PingPong(t, 1));
    }
}
