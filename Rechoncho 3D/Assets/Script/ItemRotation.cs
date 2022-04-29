using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    public int speed;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
