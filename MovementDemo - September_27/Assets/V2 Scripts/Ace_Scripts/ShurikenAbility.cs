using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenAbility : MonoBehaviour
{
    [SerializeField] private float Damage;

    [SerializeField] private float CooldownTime;
    private float CooldownTimer;
    [SerializeField] private float DurationTime;
    private float DurationTimer;

    private bool bActivated = false;
    private bool bEquipped = false;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<Revolve>().enabled = false;
        gameObject.GetComponent<Rotate>().enabled = false;
        CooldownTimer = CooldownTime;
    }

    private void Update()
    {
        if(bEquipped == true)
        {
            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                if(CooldownTimer >= CooldownTime)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<CircleCollider2D>().enabled = true;
                    gameObject.GetComponent<Revolve>().enabled = true;
                    gameObject.GetComponent<Rotate>().enabled = true;
                    bActivated = true;
                    CooldownTimer = 0;
                }
            }
            if(bActivated)
            {
                DurationTimer += Time.deltaTime;
                if(DurationTimer >= DurationTime)
                {
                    bActivated = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    gameObject.GetComponent<Revolve>().enabled = false;
                    gameObject.GetComponent<Rotate>().enabled = false;
                    DurationTimer = 0;
                }
            }
            else
            {
                CooldownTimer += Time.deltaTime;
            }
        }
    }

    public void SetEquipped()
    {
        bEquipped = true;
    }

    public void Unequip()
    {
        bEquipped = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GroundEnemy")
        {
            collision.gameObject.GetComponent<Skeleton>().TakeDMG(Damage);
        }
        if (collision.gameObject.tag == "PyromaniacEnemy")
        {
            collision.gameObject.GetComponent<EvilWizard>().TakeDMG((int)Damage);
        }
        if (collision.gameObject.tag == "GoblinEnemy")
        {
            collision.gameObject.GetComponent<Goblin>().TakeDMG((int)Damage);
        }
        if (collision.gameObject.tag == "BatEnemy")
        {
            collision.gameObject.GetComponent<Bat>().TakeDMG((int)Damage);
        }
        if (collision.gameObject.tag == "Reaper_Boss")
        {
            collision.gameObject.GetComponent<Boss_Health>().TakeDMG((int)Damage);
        }
    }
}
