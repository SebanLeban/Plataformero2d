using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPlayer : MonoBehaviour
{

    public Transform teleportLocation;
    public Rigidbody player;

    void Update()
    {
        if (AllowActions.equippedTP && (Input.GetKeyDown(KeyCode.LeftControl)))
        {
            player.transform.position = new Vector3(teleportLocation.position.x, teleportLocation.position.y, 0);
            
        }
    }
}
