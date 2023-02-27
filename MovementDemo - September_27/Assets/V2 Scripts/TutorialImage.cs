using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImage : MonoBehaviour
{
    [SerializeField] private GameObject Image;
    [SerializeField] private GameObject StartCollider;
    [SerializeField] private GameObject EndCollider;
    private bool bEntered = false;


    private void Awake()
    {
        Image.SetActive(false);
        transform.position = StartCollider.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(bEntered == false)
            {
                Image.SetActive(true);
                transform.position = EndCollider.transform.position;
                bEntered = true;
            }
            else
            {
                Image.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
