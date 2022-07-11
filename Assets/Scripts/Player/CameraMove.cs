using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Player;
    Vector3 offset;
    public float leftLim, rightLim, backLim, forwardLim;
    public float delay = 1f;
    

    private void Start()
    {
        offset = transform.position - Player.transform.position;
    }
    private void LateUpdate()
    {
        Vector3 targetPos = Player.transform.position + offset;
        Vector3 currentPos = Vector3.Lerp(transform.position, targetPos, delay * Time.deltaTime);
        transform.position = currentPos;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLim, rightLim), transform.position.y, Mathf.Clamp(transform.position.z, backLim, forwardLim));
    }
}
