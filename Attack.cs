using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator Ani;
    public PolygonCollider2D Coll;

    public int Damage;
    public float AttackTime;
    public bool isAttack;

    public float rollspeed;
    public float rolltime;
    public Rigidbody2D RB;
    public Transform Player;
    public bool isRoll;

    void Start()
    {
        isAttack = false;
        isRoll = false;
    }

    void Update()
    {
        PlayerAttack();
        Rolling();
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown("z") && isRoll == false)
        {
            isAttack = true;
            Coll.enabled = true;
            Ani.SetTrigger("Attack");
            StartCoroutine(DisableHitBox());
        }
    }

    IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(AttackTime);
        Coll.enabled = false;
        isAttack = false;
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }

    void Rolling()
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

        if (Player.localScale.x > 0)
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
    }
}
