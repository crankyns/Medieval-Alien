using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    float speed = 0;
    Vector3 oldPos, interruptedPosPatrol;



    public Vector3 startPosPatrol, finishPosPatrol;

    public bool isFollowing = false;

    Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        oldPos = transform.position;
        startPosPatrol = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Patrol());
    }


    void Update()
    {
        speed = Mathf.Abs(Vector3.Distance(oldPos, transform.position)/Time.deltaTime);
        anim.SetFloat("Speed", speed);

        if(speed < 0.1f && Vector3.Distance(transform.position, target.position) < 2.2f)
        {
            transform.LookAt(target);
            anim.SetBool("Attack", true);
            if(!FindObjectOfType<PlayerController>().isDead)
                StartCoroutine(HitPlayer());


        }
        else
            anim.SetBool("Attack", false);

        oldPos = transform.position;

    }

    IEnumerator Patrol()
    {
        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPosPatrol, 3 * Time.deltaTime);
            transform.LookAt(finishPosPatrol);
            if(Vector3.Distance(transform.position, finishPosPatrol) < 0.1f)
            {
                Vector3 buffer = finishPosPatrol;
                finishPosPatrol = startPosPatrol;
                startPosPatrol = buffer;
                transform.LookAt(finishPosPatrol);
            }

            yield return new WaitUntil(() => isFollowing == false);
        }

    }

    IEnumerator HitPlayer()
    {
        FindObjectOfType<PlayerController>().isDead = true;
        yield return new WaitForSeconds(0.3f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Death");
    }
}
