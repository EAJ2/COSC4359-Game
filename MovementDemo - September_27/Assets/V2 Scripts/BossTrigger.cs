using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject BossHUD;
    public GameObject BossConvo;
    public GameObject player;
    public Animator bossAnim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && BossConvo.active == true)
        {
            BossHUD.SetActive(true);
            BossConvo.SetActive(false);
            player.GetComponent<V2PlayerMovement>().enabled = true;
            player.GetComponent<V2PlayerCombat>().enabled = true;
            bossAnim.SetBool("Intro", false);
            this.gameObject.SetActive(false);
            this.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<V2PlayerMovement>().enabled = false;
            player.GetComponent<Animator>().SetBool("isMoving", false);
            player.GetComponent<Animator>().SetBool("isGrounded", true);
            player.GetComponent<V2PlayerCombat>().enabled = false;
            BossConvo.SetActive(true);
        }
    }


    
}
