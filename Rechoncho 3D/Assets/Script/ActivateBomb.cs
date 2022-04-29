using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBomb : MonoBehaviour
{
    public Transform spawnBombOn;
    public Rigidbody bomb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(bomb, spawnBombOn.position, spawnBombOn.rotation);
        }
    }
}
