using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform Bullet;
    public KeyCode Trigger = KeyCode.Mouse0;
    public float Velcoity = 100;
    public float shotLim = 4;
    public float timeLim = 1;
    private Rigidbody2D rgd;
    //public AudioSource sht;
    private float fromLast = 0;
    private float shots = 0;
    //private Vector2 xVector;
    // Use this for initialization
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        //sht = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Trigger))
        {
            if (fromLast < timeLim && shots < shotLim)
            {
                Transform bul = Instantiate(Bullet, rgd.transform.position, transform.rotation);
                Rigidbody2D tst = bul.GetComponent<Rigidbody2D>();
                Physics2D.IgnoreCollision(bul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                tst.velocity = new Vector2(Velcoity, 0);
                if (fromLast < timeLim)
                {
                    fromLast += Time.deltaTime;
                    shots++;
                }
                else
                {
                    shots = 0;
                    fromLast = Time.deltaTime;
                }
            }
        }
        else
        {
            fromLast += Time.deltaTime;
            if (fromLast > timeLim)
            {
                fromLast = 0;
                shots = 0;
            }
        }
    }
}
