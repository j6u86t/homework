using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public float speed;
    public float waiting;
    public Transform[] movePos;
    public Rigidbody2D RB;

    private int i = 0;
    private float originalspeed;
    private float waittime;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        waittime = waiting;
        originalspeed = speed;
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        //防止角色在死亡動畫時下落
        if (Health <= 0)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        if (isHurt == false && GameObject.Find("Vision").GetComponent<GoblinVision>().isFollow == false)
        {
            if (transform.position.x < movePos[i].position.x)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (System.Math.Abs(transform.position.x - movePos[i].position.x) <= 0.1f)
            {
                speed = 0;
                if (waiting > 0)
                {
                    Ani.SetBool("Run", false);
                    waiting -= Time.deltaTime;
                }
                else
                {
                    Ani.SetBool("Run", true);
                    if (i == 0)
                    {
                        i = 1;
                    }
                    else
                    {
                        i = 0;
                    }

                    if (transform.position.x > movePos[i].position.x)
                    {
                        transform.localScale = new Vector3(-4, 4, 4);
                    }
                    else
                    {
                        transform.localScale = new Vector3(4, 4, 4);
                    }                    
                    waiting = waittime;
                    speed = originalspeed;
                }
            }
            else
            {
                if (transform.position.x > movePos[i].position.x)
                {
                    transform.localScale = new Vector3(-4, 4, 4);
                }
                else
                {
                    transform.localScale = new Vector3(4, 4, 4);
                }
                Ani.SetBool("Run", true);
                waiting = waittime;
                speed = originalspeed;
            }           
        }
    }
}
