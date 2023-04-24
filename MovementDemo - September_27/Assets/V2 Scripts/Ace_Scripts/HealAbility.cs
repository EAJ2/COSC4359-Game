using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private Player player;
    [SerializeField] private V2Health health;
    [SerializeField] private float HealAmount;

    [Header("Time Parameters")]
    [SerializeField] private float CooldownTime;
    private float CooldownTimer = 0;
    [SerializeField] private float DurationTime;
    private float DurationTimer = 0;

    private bool bActivated = false;
    private bool bEquipped = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if (player == null)
        {
            Debug.Log("Player serialization is missing in the Heal Ability script!");
        }
        if(health == null)
        {
            Debug.Log("Health serialization is missing in the Heal Ability script!");
        }
        CooldownTimer = CooldownTime;
    }

    private void Update()
    {
        if(bEquipped)
        {
            player.SetAbility4Status();
            if(CooldownTimer >= CooldownTime)
            {
                if(Input.GetKeyDown(KeyCode.Alpha7))
                {
                    bActivated = true;
                    CooldownTimer = 0;
                    anim.SetBool("Heal", true);
                }
            }
            if(bActivated)
            {
                DurationTimer += Time.deltaTime;
                if(DurationTimer >= DurationTime)
                {
                    bActivated = false;
                    DurationTimer = 0;
                    anim.SetBool("Heal", false);
                }
                else
                {
                    health.AddHealth(HealAmount * Time.deltaTime);
                }
            }
            else
            {
                CooldownTimer += Time.deltaTime;
            }
        }
    }

    public bool IsReady()
    {
        return (CooldownTimer >= CooldownTime);
    }

    public void Equip()
    {
        bEquipped = true;
    }

    public void Unequip()
    {
        bEquipped = false;
    }
}
