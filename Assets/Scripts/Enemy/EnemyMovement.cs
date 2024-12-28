using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public EnemyScriptableObjects enemyData; // ihtiyac kalmadi
    EnemyStats enemy;
    Transform player;
    

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindAnyObjectByType<PlayerMovement>().transform; 
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
    }
}
