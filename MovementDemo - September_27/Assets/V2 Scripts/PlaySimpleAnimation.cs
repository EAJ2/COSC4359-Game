using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySimpleAnimation : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float AnimTime;
    private bool bAnimPlaying = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9) && bAnimPlaying == false)
        {
            bAnimPlaying = true;
            StartCoroutine(PlayAnim());
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && bAnimPlaying == false)
        {
            bAnimPlaying = true;
            StartCoroutine(PlayHealAnim());
        }
    }

    private IEnumerator PlayAnim()
    {
        anim.SetBool("Play", true);
        yield return new WaitForSeconds(AnimTime);
        bAnimPlaying = false;
        anim.SetBool("Play", false);
    }

    private IEnumerator PlayHealAnim()
    {
        anim.SetBool("Heal", true);
        yield return new WaitForSeconds(AnimTime);
        bAnimPlaying = false;
        anim.SetBool("Heal", false);
    }
}
