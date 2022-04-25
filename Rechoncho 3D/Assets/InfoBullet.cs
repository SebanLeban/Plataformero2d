using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Physics.IgnoreCollision(this.GetComponent<Collider>(), GetComponent<Collider>());

        else if (collision.gameObject.tag == "Player")
        {
            //do damage
            Destroy(gameObject);
        }

        else
        {
            Debug.Log("se destruyo");
            Destroy(gameObject);
        }
    }
}
