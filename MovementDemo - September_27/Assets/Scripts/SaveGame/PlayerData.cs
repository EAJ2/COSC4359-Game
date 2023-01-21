using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public float health;
    public float[] position;

    public int minutesPlayed;
    public string minutesString;
    public int hoursPlayed;
    public string hoursString;
    public string timePlayed;

    public bool bDash;
    public bool bJump;

    public PlayerData(ExoV3Movement Player)
    {
        health = Player.currentHealth;

        bJump = Player.bCanJump;
        bDash = Player.bCanDash;

        position = new float[3];
        position[0] = Player.transform.position.x;
        position[1] = Player.transform.position.y;
        position[2] = Player.transform.position.z;

        minutesPlayed = Player.minutes;
        minutesString = minutesPlayed.ToString();

        hoursPlayed = Player.hours;
        hoursString = hoursPlayed.ToString();
        if(hoursPlayed == 0 && minutesPlayed == 0)
        {
            timePlayed = "00:00";
        }
        else if (hoursPlayed < 10)
        {
            if(minutesPlayed < 10)
            {
                timePlayed = "0" + hoursString + ":0" + minutesString;
            }
            else
            {
                timePlayed = "0" + hoursString + ":" + minutesString;
            }
        }
        else if(hoursPlayed >= 10)
        {
            if(minutesPlayed < 10)
            {
                timePlayed = hoursString + ":0" + minutesString;
            }
            else
            {
                timePlayed = hoursString + ":" + minutesString;
            }
        }
        else
        {
            timePlayed = "Nothing is working";
        }
    }
}
