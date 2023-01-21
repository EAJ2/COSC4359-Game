using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTutorialTrigger : MonoBehaviour
{
    public TutorialDisplays td;

    private void Awake()
    {
        td.GetComponent<TutorialDisplays>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            td.EnableDashImage();
            gameObject.SetActive(false);
        }
    }
}
