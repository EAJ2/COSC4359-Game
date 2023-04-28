using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossRoomV2 : MonoBehaviour
{
    [SerializeField] private Player player;
    private BoxCollider2D col;

    [Header("Enemies - Remember to Set the enemies to bCanMove = false and bRespawn = false")]
    public List<DrillFlyEnemy> DrillFlys;
    public List<FlyEnemy> Flies;
    public List<Skeleton> Skeletons;
    public List<Goblin> Goblins;
    public List<EvilWizard> Wizards;
    public List<Mushroom> Mushrooms;
    private int NumberOfEnemies = 0;
    private int NumberOfDead = 0;

    [Header("Door")]
    [SerializeField] MoveSprite2Directions Door;
    private bool bDoorClosed = false;

    [Header("Key")]
    [SerializeField] private UnlockKey Key;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        if(player == null)
        {
            Debug.Log("Missing Player in the MinisBossRoom");
        }
        NumberOfEnemies = DrillFlys.Count + Flies.Count + Skeletons.Count + Goblins.Count + Wizards.Count + Mushrooms.Count;
        Key.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Door.IsAtRightDownPosition() && bDoorClosed == false)
        {
            EnableMovementOnEnemies();
            Door.Deactivate();
            player.GetComponent<V2PlayerMovement>().EnableMovement();
            bDoorClosed = true;
        }

        if (NumberOfDead == NumberOfEnemies)
        {
            if(Door.IsAtLeftUpPosition() == false)
            {
                Door.Activate();
                Door.Switch();
                Key.gameObject.SetActive(true);
                col.enabled = false;
            }
            else
            {
                Door.Deactivate();
            }
        }



        //Check if the enemy is dead
        if (DrillFlys != null)
        {
            foreach (DrillFlyEnemy drillsflies in DrillFlys)
            {
                if (drillsflies.GetComponent<DrillFlyEnemy>().IsDead())
                {
                    NumberOfDead++;
                    DrillFlys.Remove(drillsflies);
                }
            }
        }
        if (Flies != null)
        {
            foreach (FlyEnemy fly in Flies)
            {
                if (fly.GetComponent<FlyEnemy>().IsDead())
                {
                    NumberOfDead++;
                    Flies.Remove(fly);
                }
            }
        }
        if (Skeletons != null)
        {
            foreach (Skeleton groundenemy in Skeletons)
            {
                if (groundenemy.GetComponent<Skeleton>().IsDead())
                {
                    NumberOfDead++;
                    Skeletons.Remove(groundenemy);
                }
            }
        }
        if (Goblins != null)
        {
            foreach (Goblin goblin in Goblins)
            {
                if (goblin.GetComponent<Goblin>().IsDead())
                {
                    NumberOfDead++;
                    Goblins.Remove(goblin);
                }
            }
        }
        if (Wizards != null)
        {
            foreach (EvilWizard wizard in Wizards)
            {
                if (wizard.GetComponent<EvilWizard>().IsDead())
                {
                    NumberOfDead++;
                    Wizards.Remove(wizard);
                }
            }
        }
        if (Mushrooms != null)
        {
            foreach (Mushroom enemy in Mushrooms)
            {
                if (enemy.GetComponent<Mushroom>().IsDead())
                {
                    NumberOfDead++;
                    Mushrooms.Remove(enemy);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (bDoorClosed == false)
            {
                collision.gameObject.GetComponent<V2PlayerMovement>().DisableMovement();
                Door.Activate();
            }
        }
    }

    private void EnableMovementOnEnemies()
    {
        if (DrillFlys != null)
        {
            foreach (DrillFlyEnemy drillsflies in DrillFlys)
            {
                drillsflies.EnableMove();
            }
        }
        if (Flies != null)
        {
            foreach (FlyEnemy fly in Flies)
            {
                fly.EnableMove();
            }
        }
        if (Skeletons != null)
        {
            foreach (Skeleton groundenemy in Skeletons)
            {
                groundenemy.EnableMove();
            }
        }
        if (Goblins != null)
        {
            foreach (Goblin goblin in Goblins)
            {
                goblin.EnableMove();
            }
        }
        if (Wizards != null)
        {
            foreach (EvilWizard wizard in Wizards)
            {
                wizard.EnableMove();
            }
        }
        if (Mushrooms!= null)
        {
            foreach (Mushroom enemy in Mushrooms)
            {
                enemy.EnableMove();
            }
        }
    }
}
