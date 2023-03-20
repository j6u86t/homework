using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinVision : MonoBehaviour
{
    public bool isFollow = false;
    public Transform player;
    public Transform goblin;
    public float speed;
    public float attackrange;
    public float followrange;
    public Collider2D Vision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Followplayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFollow = true;
            Vision.enabled = false;
        }
    }

    void Followplayer()
    {
        float distance = (transform.position - player.position).sqrMagnitude;

        if (isFollow == true)
        {
            if (distance < followrange)
            {
                if (distance > attackrange)
                {
                    goblin.position = Vector2.MoveTowards(goblin.position, player.position, speed * Time.deltaTime);
                }
                /*else
                {

                }*/
            }
            
            

            
        }
    }
}
