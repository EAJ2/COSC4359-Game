using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupTutorial : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private AntEnemy ant;
    [SerializeField] private ExoV3Movement player;
    private bool bIsDead = false;

    private void Awake()
    {
        Canvas.SetActive(false);
    }

    private void Update()
    {
        if(ant.IsDead() == true && !bIsDead)
        {
            player.DisableMovement();
            player.DisableJump();
            Canvas.SetActive(true);
            bIsDead = true;
        }
    }

    public void Confirm()
    {
        player.EnableJump();
        player.EnableMovement();
        Canvas.SetActive(false);
    }
}
