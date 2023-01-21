using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorialTrigger : MonoBehaviour
{
    public TutorialDisplays td;
    [SerializeField] private GameObject names;

    private void Awake()
    {
        td.GetComponent<TutorialDisplays>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            td.EnableMoveImage();
            names.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
