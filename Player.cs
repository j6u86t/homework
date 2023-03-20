using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;//移動速度
    public float jump;//跳躍力
    public Rigidbody2D RB;//獲取鋼體
    public Animator Ani;
    public LayerMask Floor;
    public Collider2D Coll;

    public bool isRoll;
    /*public float rollspeed;
    public float rolltime;
    public Transform Playerspace;*/

    private bool isFloor;

    void Start()
    {
       
    }

    void FixedUpdate()
    {
        Movement();
        SwitchAni();
        onFloor();
        Jumping();
        print(isRoll);
    }

    void Movement()
    {
        float HorizontalMove = Input.GetAxisRaw("Horizontal");

        if(GameObject.Find("PlayerAttack").GetComponent<Attack>().isRoll == false)
        {
            if (isRoll == false)
            {
                RB.velocity = new Vector2(HorizontalMove * speed * Time.deltaTime, RB.velocity.y);
                Ani.SetFloat("Moveing", Mathf.Abs(HorizontalMove));
            }

            if (HorizontalMove != 0 && isRoll == false)
            {
                transform.localScale = new Vector3(HorizontalMove * 5, 5, 5);
            }
        }    
    }

    /*void Rolling()
    {
        if (Input.GetKeyDown("c") && GameObject.Find("PlayerAttack").GetComponent<Attack>().isAttack == false)
        {
            StartCoroutine(Roll());
        }
    }

    IEnumerator Roll()
    {
        isRoll = true;
        Ani.SetTrigger("Roll");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        if (Playerspace.localScale.x > 0)
        {
            for (int i = 0; i < 10; i++)
            {
                RB.AddForce(Vector3.right * rollspeed);
                yield return new WaitForSeconds(rolltime);
            }

        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                RB.AddForce(Vector3.left * rollspeed);
                yield return new WaitForSeconds(rolltime);
            }
        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        isRoll = false;
    }*/

    void onFloor()
    {
        isFloor = Coll.IsTouchingLayers(LayerMask.GetMask("Floor"));
    }

    void Jumping()
    {
        if (Input.GetButton("Jump"))
        {
            if (isFloor)
            {
                RB.velocity = new Vector2(RB.velocity.x, jump * Time.deltaTime);
                Ani.SetBool("Jumping", true);
            }
        }
    }


    void SwitchAni()
    {
        Ani.SetBool("Idleing", false);
         
        if (Ani.GetBool("Jumping"))
        {
            if (RB.velocity.y < 0)
            {
                Ani.SetBool("Jumping", false);
                Ani.SetBool("Falling", true);
            }
        }
        else if (Coll.IsTouchingLayers(LayerMask.GetMask("Floor")))
        {
             Ani.SetBool("Falling", false);
             Ani.SetBool("Idleing", true);
        }
        
    }

 

}    
