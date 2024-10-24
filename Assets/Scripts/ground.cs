using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject tilePrefab; // Prefab of the ground tile
    public GameObject redTilePrefab; // Prefab for red object
    public GameObject greenTilePrefab; // Prefab for green object
    public int initialTileCount = 15; // Number of initial tiles to spawn

    private Vector3 nextSpawnPoint; // The position for the next set of tiles
    private Queue<GameObject> tileQueue = new Queue<GameObject>(); // Pool for ground tiles
    private Queue<GameObject> redGreenTileQueue = new Queue<GameObject>(); // Pool for red and green tiles
    private System.Random random = new System.Random(); // Random number generator

    // Lanes based on x positions
    private float[] lanePositions = { -5f, 0f, 5f };

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the ground tiles
        for (int i = 0; i < initialTileCount; i++)
        {
            SpawnTileSet(); // Spawn three adjacent tiles (one for each lane)
        }
    }

    // Spawns a set of three adjacent tiles (one for each lane)
    void SpawnTileSet()
    {
        for (int i = 0; i < lanePositions.Length; i++)
        {
            SpawnTileOrColored(lanePositions[i]);
        }

        // Update the next spawn point along the Z-axis for the next row of tiles
        nextSpawnPoint += new Vector3(0f, 0f, tilePrefab.transform.localScale.z);
    }

    // Spawns either a ground tile or a colored tile on a specific lane
    void SpawnTileOrColored(float laneXPosition)
    {
        GameObject newTile;

        // 30% chance to spawn a red or green tile instead of a ground tile
        float chance = random.Next(0, 100); 
        if (chance < 5f) // 30% chance to spawn a colored tile
        {
            // Spawn a red or green tile at the specified lane
            newTile = SpawnColoredTile(laneXPosition);
        }
        else
        {
            // Spawn a normal ground tile at the specified lane
            newTile = SpawnGroundTile(laneXPosition);
        }
        
        // Place the tile at the next Z position and at the correct lane (X position)
        newTile.transform.position = new Vector3(laneXPosition, nextSpawnPoint.y, nextSpawnPoint.z);
    }

    // Spawns a ground tile on a specific lane
    GameObject SpawnGroundTile(float laneXPosition)
    {
        GameObject newTile;

        // Reuse ground tiles from the pool if available
        if (tileQueue.Count > 0)
        {
            newTile = tileQueue.Dequeue();
            newTile.SetActive(true); // Reactivate the tile
        }
        else
        {
            // Instantiate a new ground tile if no reusable ones are available
            newTile = Instantiate(tilePrefab, nextSpawnPoint, Quaternion.identity);
        }

        return newTile;
    }

    // Spawns a colored tile (red or green) on a specific lane
    GameObject SpawnColoredTile(float laneXPosition)
    {
        GameObject newTile;
        GameObject tileToSpawnPrefab = random.Next(0, 2) == 0 ? redTilePrefab : greenTilePrefab; // Randomly choose red or green tile

        // Reuse colored tiles from the pool if available
        if (redGreenTileQueue.Count > 0)
        {
            newTile = redGreenTileQueue.Dequeue();
            newTile.SetActive(true); // Reactivate the tile
        }
        else
        {
            // Instantiate a new colored tile if no reusable ones are available
            newTile = Instantiate(tileToSpawnPrefab, nextSpawnPoint, Quaternion.identity);
        }

        // Ensure the tile has the correct width (since it's one lane wide)
        newTile.transform.localScale = new Vector3(0.5f, newTile.transform.localScale.y, newTile.transform.localScale.z); 

        return newTile;
    }

    // Recycle ground tiles back into the pool
    public void RecycleTile(GameObject tile)
    {
        tile.SetActive(false);
        tileQueue.Enqueue(tile); // Add the tile to the queue for reuse
    }

    // Recycle red/green tiles back into the pool
    public void RecycleColoredTile(GameObject tile)
    {
        tile.SetActive(false);
        redGreenTileQueue.Enqueue(tile); // Add the tile to the queue for reuse
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously spawn new sets of tiles as needed
        if (Vector3.Distance(Camera.main.transform.position, nextSpawnPoint) < 20f)
        {
            SpawnTileSet(); // Spawn a new row of three adjacent tiles
        }
    }
}
