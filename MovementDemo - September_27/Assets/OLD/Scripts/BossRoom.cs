using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [Header("Enemies")]
    public List<AntEnemy> Ants;
    public List<DrillFlyEnemy> DrillFlys;
    public List<FlyEnemy> Flies;
    public List<GroundEnemy> GroundEnemies;
    public List<HorseEnemy> HorseEnemies;
    public List<SnakeEnemy> SnakeEnemies;
    public List<BossEnemy> BossEnemies;

    [Header("Camera Zooms")]
    public List<CameraZoom> Zooms;
    [SerializeField] CameraZoom RoomZoom;
    [SerializeField] CameraController cam;

    private bool bIsDoorClosed = false;

    private float fAnts = 0;
    private float fDrillFlys = 0;
    private float fFlies = 0;
    private float fGroundEnemies = 0;
    private float fHorseEnemies = 0;
    private float fSnakeEnemies = 0;
    private float fBossEnemies = 0;

    private float AntsDead = 0;
    private float DrillFliesDead = 0;
    private float FliesDead = 0;
    private float GroundEnemiesDead = 0;
    private float HorseEnemiesDead = 0;
    private float SnakeEnemiesDead = 0;
    private float BossEnemiesDead = 0;

    private float TotalEnemies = 0;
    private float TotalEnemiesKilled = 0;
    private float fExits = 0;

    private bool Deactivate = false;
    private bool bEntranceRemainsOpen = false;
    private bool bExit1Pan = false;
    private bool bExit2Pan = false;

    private float waitTimer = 0;
    private float waitTime = 2f;

    private float CompletedWaitTime = 2.5f;
    private float CompletedWaitTimer = 0f;

    [Header("Doors & Exits")]
    [SerializeField] MoveSprite2Directions Door;

    //Door 0 = Exit
    //Door 1 = Prize
    public List<MoveSprite1Direction> Exits;
    private BoxCollider2D col;
    [SerializeField] private ExoV3Movement player;
    [SerializeField] private ChestReward CR;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        RoomZoom.GetComponent<BoxCollider2D>().enabled = false;
        Zooms[1].GetComponent<BoxCollider2D>().enabled = false;
        Zooms[2].GetComponent<BoxCollider2D>().enabled = false;

        if (Exits != null)
        {
            foreach(MoveSprite1Direction door in Exits)
            {
                fExits++;
            }
        }

        if(Ants != null)
        {
            foreach(AntEnemy ant in Ants)
            {
                fAnts++;
            }
        }
        if(DrillFlys != null)
        {
            foreach (DrillFlyEnemy drillsflies in DrillFlys)
            {
                fDrillFlys++;
            }
        }
        if(Flies != null)
        {
            foreach (FlyEnemy fly in Flies)
            {
                fFlies++;
            }
        }
        if(GroundEnemies != null)
        {
            foreach (GroundEnemy groundenemy in GroundEnemies)
            {
                fGroundEnemies++;
            }
        }
        if(HorseEnemies != null)
        {
            foreach (HorseEnemy horseenemy in HorseEnemies)
            {
                fHorseEnemies++;
            }
        }
        if(SnakeEnemies != null)
        {
            foreach (SnakeEnemy snake in SnakeEnemies)
            {
                fSnakeEnemies++;
            }
        }
        if (BossEnemies != null)
        {
            foreach (BossEnemy boss in BossEnemies)
            {
                fBossEnemies++;
            }
        }

        TotalEnemies = fAnts + fDrillFlys + fFlies + fGroundEnemies + fHorseEnemies + fSnakeEnemies + fBossEnemies;
    }

    private void Update()
    {
        if(TotalEnemies == TotalEnemiesKilled)
        {
            CompletedWaitTimer += Time.deltaTime;
            if(CompletedWaitTimer >= CompletedWaitTime)
            {
                Deactivate = true;
            }
        }

        if (Zooms[0].InsideCameraZoom() && !bIsDoorClosed && !Deactivate)
        {
            Door.Activate();
            player.DisableFullMovement();
        }
        if (Door.IsAtRightDownPosition() && !bIsDoorClosed && !Deactivate)
        {
            bIsDoorClosed = true;
            Zooms[0].GetComponent<BoxCollider2D>().enabled = false;
            RoomZoom.GetComponent<BoxCollider2D>().enabled = true;
            player.EnableFullMovement();

            EnableMovementOnEnemies();
        }

        if(Deactivate && bIsDoorClosed && !bEntranceRemainsOpen)
        {
            RoomZoom.GetComponent<BoxCollider2D>().enabled = false;
            Zooms[0].PanToPoint();
            player.DisableFullMovement();
            Door.Switch();
        }
        if(Door.IsAtLeftUpPosition() && Deactivate && !bEntranceRemainsOpen)
        {
            bEntranceRemainsOpen = true;
            bIsDoorClosed = false;
            Door.enabled = false;
        }


        if(Exits[0] != null && !bExit1Pan && bEntranceRemainsOpen)
        {
            Zooms[1].PanToPoint();
            Exits[0].ActivateObject();
        }
        if(Exits[0] != null && Exits[0].IsAtPosition())
        {
            bExit1Pan = true;
            Exits[0].enabled = false;
        }


        if(Exits[1] != null && !bExit2Pan && bExit1Pan)
        {
            Zooms[2].PanToPoint();
            Exits[1].ActivateObject();
            CR.GetComponent<MoveSprite1Direction>().ActivateObject();
        }
        if (Exits[1] != null && Exits[1].IsAtPosition() && !bExit2Pan)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= waitTime)
            {
                bExit2Pan = true;
                Exits[1].enabled = false;
                RoomZoom.GetComponent<BoxCollider2D>().enabled = true;
                player.EnableFullMovement();
                cam.EnableFollowPlayer();
            }
        }

        /*if (TotalEnemies == TotalEnemiesKilled && !Deactivate && (Exits != null))
        {
            Exits[0].GetComponent<MoveSprite1Direction>().ActivateObject();
            Door.Switch();
            if(fExits == 2)
            {
                Exits[1].GetComponent<MoveSprite1Direction>().ActivateObject();
            }
            col.enabled = false;
            Deactivate = true;
        }*/

        if (Ants != null)
        {
            foreach (AntEnemy ant in Ants)
            {
                if (ant.GetComponent<AntEnemy>().IsDead())
                {
                    AntKilled();
                    Ants.Remove(ant);
                }
            }
        }

        if (DrillFlys != null)
        {
            foreach (DrillFlyEnemy drillsflies in DrillFlys)
            {
                if(drillsflies.GetComponent<DrillFlyEnemy>().IsDead())
                {
                    DrillKilled();
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
                    FliesKilled();
                    Flies.Remove(fly);
                }
            }
        }

        if (GroundEnemies != null)
        {
            foreach (GroundEnemy groundenemy in GroundEnemies)
            {
                if(groundenemy.GetComponent<GroundEnemy>().IsDead())
                {
                    GroundEnemyKilled();
                    GroundEnemies.Remove(groundenemy);
                }
            }
        }

        if (HorseEnemies != null)
        {
            foreach (HorseEnemy horseenemy in HorseEnemies)
            {
                if(horseenemy.GetComponent<HorseEnemy>().IsDead())
                {
                    HorseEnemiesKilled();
                    HorseEnemies.Remove(horseenemy);
                }
            }
        }

        if (SnakeEnemies != null)
        {
            foreach (SnakeEnemy snake in SnakeEnemies)
            {
                if(snake.GetComponent<SnakeEnemy>().IsDead())
                {
                    SnakesKilled();
                    SnakeEnemies.Remove(snake);
                }
            }
        }

        if(BossEnemies != null)
        {
            foreach (BossEnemy boss in BossEnemies)
            {
                if(boss.GetComponent<BossEnemy>().IsDead())
                {
                    BossKilled();
                    BossEnemies.Remove(boss);
                }
            }
        }

        if(!Deactivate)
        {
            TotalEnemiesKilled = AntsDead + DrillFliesDead + FliesDead + GroundEnemiesDead + HorseEnemiesDead + SnakeEnemiesDead + BossEnemiesDead;
        }
    }

    public void AntKilled()
    {
        AntsDead++;
    }

    public void DrillKilled()
    {
        DrillFliesDead++;
    }

    public void FliesKilled()
    {
        FliesDead++;
    }

    public void GroundEnemyKilled()
    {
        GroundEnemiesDead++;
    }

    public void HorseEnemiesKilled()
    {
        HorseEnemiesDead++;
    }

    public void SnakesKilled()
    {
        SnakeEnemiesDead++;
    }

    public void BossKilled()
    {
        BossEnemiesDead++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

        }
    }

    public bool IsDoorClosed()
    {
        return Door.IsAtRightDownPosition();
    }

    public bool IsDoorOpen()
    {
        return Door.IsAtLeftUpPosition();
    }

    private void EnableMovementOnEnemies()
    {
        if (Ants != null)
        {
            foreach (AntEnemy ant in Ants)
            {
                ant.EnableMove();
            }
        }

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

        if (GroundEnemies != null)
        {
            foreach (GroundEnemy groundenemy in GroundEnemies)
            {
                groundenemy.EnableMove();
            }
        }

        if (HorseEnemies != null)
        {
            foreach (HorseEnemy horseenemy in HorseEnemies)
            {
                horseenemy.EnableMove();
            }
        }

        if (SnakeEnemies != null)
        {
            foreach (SnakeEnemy snake in SnakeEnemies)
            {
                snake.EnableMove();
            }
        }

        if(BossEnemies != null)
        {
            foreach(BossEnemy boss in BossEnemies)
            {
                boss.EnableMove();
            }
        }
    }
}
