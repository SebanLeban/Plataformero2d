using UnityEngine;

public class ControlBotSpecial : MonoBehaviour
{
    private int hp;
    public GameObject Player;

    void Start()
    {
        hp = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Agregar tag de lo que lo mata

        if (collision.gameObject.CompareTag(""))
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
