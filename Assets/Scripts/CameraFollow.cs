using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 diff;
    // Start is called before the first frame update
    void Start()
    {
        diff = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.position + diff;
        newPos.x = 0;
        transform.position = newPos;
    }
}
