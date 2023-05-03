using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject BossHUD;
    public GameObject BossConvo;
    public GameObject player;
    public Animator bossAnim;
    public GameObject Door;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Door.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && BossConvo.activeSelf == true)
        {
            BossHUD.SetActive(true);
            BossConvo.SetActive(false);
            player.GetComponent<V2PlayerMovement>().EnableMovement();
            player.GetComponent<V2PlayerCombat>().enabled = true;
            bossAnim.SetBool("Intro", false);
            this.gameObject.SetActive(false);
            this.enabled = false;
            Time.timeScale = 1f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<V2PlayerMovement>().DisableMovement();
            //Door.SetActive(true);
            player.GetComponent<Animator>().SetBool("isMoving", false);
            player.GetComponent<Animator>().SetBool("isGrounded", true);
            player.GetComponent<V2PlayerCombat>().enabled = false;
            BossConvo.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    
}
