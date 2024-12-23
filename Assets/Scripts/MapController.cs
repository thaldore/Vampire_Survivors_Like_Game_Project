using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;


    void Start()
    {
        pm = Object.FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        ChunkChecker();
    }

    void ChunkChecker()
    {

        if (!currentChunk)
        {
            return;
        }


        if (pm.moveDir.x > 0 && pm.moveDir.y == 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(20, 0, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(20, 0, 0);  //Right
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-20, 0, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-20, 0, 0);    //Left
                SpawnChunk();
            }
        }
        else if (pm.moveDir.y > 0 && pm.moveDir.x == 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, 20, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0, 20, 0); //Up
                SpawnChunk();
            }
        }
        else if (pm.moveDir.y < 0 && pm.moveDir.x == 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, -20, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0, -20, 0);    //Down
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(20, 20, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(20, 20, 0);   //Right up
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(20, -20, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(20, -20, 0);  //Right down
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-20, 20, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-20, 20, 0);  //Left up
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-20, -20, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-20, -20, 0); //Left down
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);

    }
}
