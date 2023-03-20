using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            if (transform.position != player.position)
            {
                Vector3 playerplace = player.position;
                transform.position = Vector3.Lerp(transform.position, playerplace, smoothing);
            }
        }
    }
}
