using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyeye : Enemy
{

    //膀娄计
    public float speed;
    public float range;

    //ю阑把计
    public float attackrange;
    public Transform player;
    public float StartAttacktime;
    public float EndAttacktime;


    //诀把计
    public Transform leftDown;
    public Transform rightUp;
    public Transform randomPlace;
    public float waitTime;
    
    private float nowTime;
    private bool isFollow;

    // Start is called before the first frame update
    public new void Start()
    {
        isFollow = false;
        nowTime = waitTime;
        base.Start();
        isAttack = false;
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();


        if (player != null && Health>0)
        {
            float distance = (transform.position - player.position).sqrMagnitude;

            if (distance < range) 
            {
                isFollow = true;

                if (transform.position.x > player.position.x)
                {
                    transform.localScale = new Vector3( -4, 4, 4);
                }
                else if(transform.position.x < player.position.x)
                {
                    transform.localScale = new Vector3(4, 4, 4);
                }

                if (distance > (attackrange - 0.5) && isHurt ==false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                if (distance < attackrange && isAttack==false)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                isFollow = false;
            }

            if (isFollow == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, randomPlace.position, speed * Time.deltaTime);

                if (transform.position.x > randomPlace.position.x)
                {
                    transform.localScale = new Vector3(-4, 4, 4);
                }
                else if (transform.position.x < randomPlace.position.x)
                {
                    transform.localScale = new Vector3(4, 4, 4);
                }

                if (Vector2.Distance(transform.position, randomPlace.position) < 0.1f) {
                    if (nowTime <= 0)
                        {
                            randomPlace.position = GetRandomPlace();
                            nowTime = waitTime;
                        }
                    else
                        {
                            nowTime -= Time.deltaTime;
                        }
                }
            }
        }
    }

    Vector2 GetRandomPlace()
    {
        Vector2 randomPosition = new Vector2(Random.Range(leftDown.position.x, rightUp.position.x), Random.Range(leftDown.position.y, rightUp.position.y));
        return randomPosition;
    }

    IEnumerator Attack()
    {
        isAttack = true;
        Ani.SetBool("Attack", true);
        yield return new WaitForSeconds(StartAttacktime);
        BodyColl.enabled = false;
        AttackColl.enabled = true;
        yield return new WaitForSeconds(EndAttacktime);
        Ani.SetBool("Attack", false);
        AttackColl.enabled = false;
        BodyColl.enabled = true;
        isAttack = false;
    }
}
