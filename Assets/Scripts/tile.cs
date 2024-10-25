using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
     Ground groundManager;

    void Start()
    {
        groundManager = GameObject.FindObjectOfType<Ground>();
    }

    // When the player exits the trigger, recycle the tile
    private void OnTriggerExit(Collider other)
    {
            groundManager.SpawnTileSet();
            // Recycle the tile when the player exits its trigger
           // groundManager.RecycleTile(gameObject);
            Destroy(gameObject, 2);
    }
}
