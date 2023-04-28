using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomV2 : MonoBehaviour
{
    [SerializeField] private BossEnemy bossEnemy;
    [SerializeField] private GameObject Canvas;

    [SerializeField] private string LevelName;

    [SerializeField] private Player player;

    private bool bBossDead = false;

    private void Start()
    {
        if(player == null)
        {
            Debug.Log("Missing player in the BossRoomV2 object");
        }
        if (LevelName == "")
        {
            Debug.Log("Write the level name in the BossRoomV2 Object");
        }
        if(bossEnemy == null)
        {
            Debug.Log("Missing Boss enemy in the BossRoomV2 Script!");
        }
        if(Canvas == null)
        {
            Debug.Log("Missing SceneSwitch in the BossRoomV2 Script!");
        }
        else
        {
            Canvas.SetActive(false);
        }
    }

    private void Update()
    {
        if(bBossDead)
        {
            Canvas.SetActive(true);
        }
    }

    public void CompleteLevel()
    {
        player.SavePlayer();
        SceneManager.LoadScene(LevelName);
    }

}
