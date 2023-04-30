using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseKeys : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Key Start Locations - Only Move First Location Horizontally")]
    [SerializeField] private GameObject StartLoc1;
    [SerializeField] private GameObject StartLoc2;
    [SerializeField] private GameObject StartLoc3;

    [Header("Key End Locations - Only Move First Location Horizontally")]
    [SerializeField] private GameObject EndLoc1;
    [SerializeField] private GameObject EndLoc2;
    [SerializeField] private GameObject EndLoc3;

    [Header("Keys")]
    [SerializeField] private MoveSprite1Direction Key1;
    [SerializeField] private MoveSprite1Direction Key2;
    [SerializeField] private MoveSprite1Direction Key3;

    [SerializeField] private GameObject UIImage;

    [Header("Door")]
    [SerializeField] private MoveSprite1Direction Door;

    [Header("WaitTime")]
    [SerializeField] private float WaitTimeAfterKeyIsPlaced;
    private float WaitTimer = 0;

    private bool bInside = false;

    private void Start()
    {
        Key1.GetComponent<SpriteRenderer>().enabled = false;
        Key2.GetComponent<SpriteRenderer>().enabled = false;
        Key3.GetComponent<SpriteRenderer>().enabled = false;

        StartLoc2.transform.position = new Vector3(StartLoc1.transform.position.x, StartLoc2.transform.position.y, StartLoc2.transform.position.z);
        StartLoc3.transform.position = new Vector3(StartLoc1.transform.position.x, StartLoc3.transform.position.y, StartLoc3.transform.position.z);

        EndLoc2.transform.position = new Vector3(EndLoc1.transform.position.x, EndLoc2.transform.position.y, EndLoc2.transform.position.z);
        EndLoc3.transform.position = new Vector3(EndLoc1.transform.position.x, EndLoc3.transform.position.y, EndLoc3.transform.position.z);

        Key1.transform.position = StartLoc1.transform.position;
        Key2.transform.position = StartLoc2.transform.position;
        Key3.transform.position = StartLoc3.transform.position;

        UIImage.SetActive(false);
    }

    private void Update()
    {
        if(bInside)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                player.GetComponent<V2PlayerMovement>().DisableMovement();

                Key1.GetComponent<SpriteRenderer>().enabled = true;
                Key2.GetComponent<SpriteRenderer>().enabled = true;
                Key3.GetComponent<SpriteRenderer>().enabled = true;

                Key1.ActivateObject();
                Key2.ActivateObject();
                Key3.ActivateObject();
            }
        }

        if(Key1.IsAtPosition() == true && Key2.IsAtPosition() == true && Key3.IsAtPosition() == true)
        {
            if (WaitTimer >= WaitTimeAfterKeyIsPlaced)
            {
                Key1.gameObject.SetActive(false);
                Key2.gameObject.SetActive(false);
                Key3.gameObject.SetActive(false);
                Door.ActivateObject();
            }
            else
            {
                WaitTimer += Time.deltaTime;
            }
        }

        if (Door.IsAtPosition())
        {
            player.GetComponent<V2PlayerMovement>().EnableMovement();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.GetComponent<Player>().CanEnterBossRoom())
            {
                bInside = true;
                UIImage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<Player>().CanEnterBossRoom())
            {
                bInside = false;
                UIImage.SetActive(false);
            }
        }
    }
}