using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockGear : MonoBehaviour
{
    [Header("Gear To Unlock")]
    [SerializeField] private bool Head = false;
    [SerializeField] private bool Chest = false;
    [SerializeField] private bool Legs = false;
    [SerializeField] private bool Shoes = false;
    [SerializeField] private bool Weapon = false;
    [SerializeField] private bool Artifact = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Head)
            {
                collision.GetComponent<Player>().UnlockHead();
            }
            if (Chest)
            {
                collision.GetComponent<Player>().UnlockChest();
            }
            if (Legs)
            {
                collision.GetComponent<Player>().UnlockLegs();
            }
            if (Shoes)
            {
                collision.GetComponent<Player>().UnlockShoes();
            }
            if (Weapon)
            {
                collision.GetComponent<Player>().UnlockWeapon();
            }
            if (Artifact)
            {
                collision.GetComponent<Player>().UnlockArtifact();
            }

            gameObject.SetActive(false);
        }
    }
}
