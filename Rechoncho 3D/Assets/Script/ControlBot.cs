using UnityEngine;

public class ControlBot : MonoBehaviour
{
    public Rigidbody player;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            player.velocity = Vector3.zero;
            player.angularVelocity = Vector3.zero;
            player.AddForce(player.transform.up * 5, ForceMode.Impulse);
            Desaparecer();
        }
    }

    private void Desaparecer()
    {
        Destroy(gameObject);
    }
}
