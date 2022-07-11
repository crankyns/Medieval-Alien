using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rig;
    float speed = 8f, speedRot = 5f;
    Animator anim;
    public GameObject PressE, cantEnter, FinishPanel, pausePan, deadPan, tutorialPan;
    public int chipAm;

    public Text amountChips;
    bool damaged;
    public Image damageImage;
    public float flashSpeed = 3f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.15f);

    public bool isDead = false;
    private void Start()
    {
        chipAm = 0;
        amountChips.text = chipAm.ToString() + "/8";
        anim = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.anyKey)
        {
            tutorialPan.SetActive(false);
        }
        if (isDead)
        {
            Cursor.visible = true;
            deadPan.SetActive(true);
        }
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        if (_rig.velocity.x == 0 && _rig.velocity.z == 0)
        {
            anim.SetBool("Fly", false);
        }
        if (Input.GetKey(KeyCode.W) && !isDead)
        {
            anim.SetBool("Fly", true);
            transform.position += Vector3.forward * speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speedRot);
        }
        if (Input.GetKey(KeyCode.A) && !isDead)
        {
            anim.SetBool("Fly", true);
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * speedRot);
        }
        if (Input.GetKey(KeyCode.S) && !isDead)
        {
            anim.SetBool("Fly", true);
            transform.position += Vector3.back * speed * Time.deltaTime;
            if(transform.rotation.y >= 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * speedRot);
            }
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * speedRot);
        }
        if (Input.GetKey(KeyCode.D) && !isDead)
        {
            anim.SetBool("Fly", true);
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * speedRot);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePan.activeInHierarchy)
            {
                Cursor.visible = false;
                pausePan.SetActive(false);
            }
            else
            {
                Cursor.visible = true;
                pausePan.SetActive(true);
            }
        }
    }
    public void GetDamage()
    {
        damaged = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Finish")
        {
            PressE.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Finish")
        {
            PressE.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item")
        {
            if (Input.GetKey(KeyCode.E))
            {
                chipAm++;
                amountChips.text = chipAm.ToString() + "/8";
                Destroy(other.gameObject);
                PressE.SetActive(false);
            }
        }
        if(other.tag == "Finish")
        {
            if (Input.GetKey(KeyCode.E))
            {
                GameFinished();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ColliderNotToGo")
        {
            cantEnter.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "ColliderNotToGo")
        {
            cantEnter.SetActive(false);
        }
    }
    void GameFinished()
    {
        Cursor.visible = true;
        FinishPanel.SetActive(true);
        PressE.SetActive(false);
    }
}
