using UnityEngine;

public class ControlBot : MonoBehaviour
{
    private int hp;
    public GameObject Player;

    void Start()
    {
        hp = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            recibirdaño();
        }
    }
    public void recibirdaño()
    {
        hp = hp - 100;

        if (hp <= 0)
        {
            desaparecer();
        }
    }
    private void desaparecer()
    {
        Destroy(gameObject, 3);
    }
}
