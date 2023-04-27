using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockKey : MonoBehaviour
{
    [Header("Key To Unlock")]
    [SerializeField] private bool Key1 = false;
    [SerializeField] private bool Key2 = false;
    [SerializeField] private bool Key3 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(Key1 == true)
            {
                collision.GetComponent<Player>().UnlockKey1();
            }
            if (Key2 == true)
            {
                collision.GetComponent<Player>().UnlockKey2();
            }
            if (Key3 == true)
            {
                collision.GetComponent<Player>().UnlockKey3();
            }

            gameObject.SetActive(false);
        }
    }


}
