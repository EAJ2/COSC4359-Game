using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenManager : MonoBehaviour
{
    public Image classImage;

    public Sprite vagabondImage;
    public Sprite knightImage;
    public Sprite rangerImage;

    public GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.name == "Player")
        {
            classImage.sprite = vagabondImage;
        }
        else if (player.name == "Ranger")
        {
            classImage.sprite = rangerImage;
            classImage.GetComponent<RectTransform>().localScale = new Vector3(6f, 5f, 2f);
            classImage.GetComponent<RectTransform>().localPosition = new Vector3(-341f, 233f, 0f);
        }
        else if (player.name == "Knight")
        {
            classImage.sprite = knightImage;
            classImage.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 2.5f, 2f);
        }
        else
        {
            classImage.sprite = vagabondImage;
        }
    }

}
