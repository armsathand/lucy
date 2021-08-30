using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hlth = 3;

    	void OnCollisionEnter2D(Collision2D col) {
            if(col.gameObject.tag == "Projectile") {
                hlth--;
                Destroy(col.gameObject);
            }
            if (hlth == 0) {
                Destroy(transform.gameObject);
            }
        }
}
