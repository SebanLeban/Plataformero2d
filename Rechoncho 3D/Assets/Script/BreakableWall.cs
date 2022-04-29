using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && AllowActions.hasGroundpounded)
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
