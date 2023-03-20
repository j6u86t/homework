using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public float Deathtime;
    public float Hurttime;
    public Animator Ani;
    public SpriteRenderer SR;

    private Color Originalcolor;

    // Start is called before the first frame update
    void Start()
    {
        Originalcolor = SR.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int Damage)
    {
        Health -= Damage;
        if (Health > 0)
        {
            GetHit();
        }


        if (Health <= 0)
        {
            Ani.SetTrigger("Death");
            Invoke("Killplayer", Deathtime);
        }
    }

    void Killplayer()
    {
        Destroy(gameObject);
    }

    void GetHit()
    {
        SR.color = Color.red;
        Invoke("ResetColor", Hurttime);
    }

    void ResetColor()
    {
        SR.color = Originalcolor;
    }
}
