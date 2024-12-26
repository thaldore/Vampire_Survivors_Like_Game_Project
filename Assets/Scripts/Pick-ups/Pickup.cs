using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) // if it gets too close the player
        {
            Destroy(gameObject);
        }

    }
}
