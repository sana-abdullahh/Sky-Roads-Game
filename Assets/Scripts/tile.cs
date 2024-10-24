using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Ground groundManager;

    void Start()
    {
        groundManager = GameObject.FindObjectOfType<Ground>();
    }

    // When the player exits the trigger, recycle the tile
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Recycle the tile when the player exits its trigger
            groundManager.RecycleTile(gameObject);
        }
    }
}
