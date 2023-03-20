using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int Health;
    public int Damage;
    public float DeathTime;
    public Animator Ani;
    public float Hurttime;
    public bool isHurt;
    public Collider2D BodyColl;
    public Collider2D AttackColl;
    public bool isAttack;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Health <= 0)
        {
            Ani.SetBool("Death", true);
            BodyColl.enabled = false;
            AttackColl.enabled = false;
            Invoke("KillEnemy", DeathTime);
        }
    }

    void KillEnemy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        if (isAttack == false)
        {
            isHurt = true;       
            Ani.SetTrigger("Hurt");
            StartCoroutine(EndHurt());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(Damage);
            }
        }
    }

    IEnumerator EndHurt()
    {
        yield return new WaitForSeconds(Hurttime);
        isHurt = false;
    }


}
