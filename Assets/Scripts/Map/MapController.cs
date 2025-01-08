using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    Vector3 playerLastPosition;



    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;


    void Start()
    {
        playerLastPosition = player.transform.position;


    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }


    void ChunkChecker()
    {

        if (!currentChunk)
        {
            return;
        }

        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunck(directionName);

        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunck("Up");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunck("Down");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunck("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunck("Left");
        }

    }

    void CheckAndSpawnChunck(string direction)
    {
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // moving horizontally more than vertically
            if(direction.y > 0.5f)
            {
                // also moving upwards
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if(direction.y < -0.5f)
            {
                // also moving downwards
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                // moving straight horizontally
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            // moving vertically more than horizontally  
            if (direction.x > 0.5f)
            {
                // also moving right
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                // also moving left
                return direction.y > 0 ? "Left Up" : "Left Down"; 
            }
            else
            {
                // moving straight vertically
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        GameObject chunkToReuse = spawnedChunks.Find(c => !c.activeSelf);

        if (chunkToReuse != null)
        {
            chunkToReuse.transform.position = spawnPosition;
            chunkToReuse.SetActive(true);
        }
        else
        {
            int rand = Random.Range(0, terrainChunks.Count);
            GameObject newChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
            spawnedChunks.Add(newChunk);
        }
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown > 0f)
            return;

        optimizerCooldown = optimizerCooldownDur;

        for (int i = 0; i < spawnedChunks.Count; i++)
        {
            GameObject chunk = spawnedChunks[i];
            if (chunk == null)
            {
                spawnedChunks.RemoveAt(i);
                i--;
                continue;
            }

            float distance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (distance > maxOpDist)
            {
                // Chunk'ý devre dýþý býrak ve havuza ekle
                chunk.SetActive(false);
            }
            else
            {
                // Chunk'ý aktif et
                chunk.SetActive(true);
            }
        }
    }
}

