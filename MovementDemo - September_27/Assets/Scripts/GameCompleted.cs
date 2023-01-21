using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleted : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        canvas.SetActive(false);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canvas.SetActive(true);
        }
    }
}
