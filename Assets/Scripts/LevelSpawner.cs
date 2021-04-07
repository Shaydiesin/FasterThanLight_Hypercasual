using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] chunks;
    float nextSpawnPoint;
    public float[] chunkLength;
    public Transform[] StartPoints;
    public Transform[] EndPoints;
    public int totalChunks;
    public Transform playerTranform;
    public List<GameObject> spawnedChunks = new List<GameObject>();

    private void Start()
    {
        for (int i=0;i<chunks.Length; i++)
        {
            chunkLength[i] = EndPoints[i].position.z - StartPoints[i].position.z;            
        }

        nextSpawnPoint = chunkLength[0];
        spawnChunk(Random.Range(0,10));
        spawnChunk(Random.Range(0, 10));
        /*spawnedChunks.Add(GameObject.Find("CorridorChunk"));*/
    }
    public void spawnChunk(int chunkIndex)
    {
        GameObject chunk= Instantiate(chunks[chunkIndex], transform.forward * nextSpawnPoint, transform.rotation);
        spawnedChunks.Add(chunk);
        nextSpawnPoint= nextSpawnPoint+chunkLength[chunkIndex];
    }

    private void Update()
    {
        if (playerTranform.position.z > spawnedChunks[0].transform.Find("End").position.z)
        {
            Destroy(spawnedChunks[0],2f);
            spawnedChunks.RemoveAt(0);
            spawnChunk(Random.Range(0, 10));
        }
        
    }
}
