using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.currentMagnet;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //pulling animation
            //gets the Rigidbody2D component on the item
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            //Vector2 pointing from the item to player
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            //applies force to the item in the forceDirection with pullSpeed
            rb.AddForce(forceDirection * pullSpeed);

            collectible.Collect();
        }
    }
}
