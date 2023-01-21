using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroToPlay : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string SceneNameToTravel = "SampleScene";


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Travel());
    }

    private IEnumerator Travel()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(SceneNameToTravel);
    }
}
