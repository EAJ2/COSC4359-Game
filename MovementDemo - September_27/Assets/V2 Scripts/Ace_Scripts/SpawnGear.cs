using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGear : MonoBehaviour
{
    private Goblin goblin;
    private EvilWizard wizard;
    private FlyEnemy fly;
    private DrillFlyEnemy drillFly;
    private Mushroom mushroom;
    private Skeleton skeleton;

    private string ClassName;

    [Header("Gear To Spawn")]
    [SerializeField] private bool Head = false;
    [SerializeField] private bool Chest = false;
    [SerializeField] private bool Legs = false;
    [SerializeField] private bool Shoes = false;
    [SerializeField] private bool Weapon = false;
    [SerializeField] private bool Artifact = false;

    [Header("Enemy Type")]
    [SerializeField] private bool Goblin = false;
    [SerializeField] private bool Wizard = false;
    [SerializeField] private bool Bat = false;
    [SerializeField] private bool DrillBat = false;
    [SerializeField] private bool Mushroom = false;
    [SerializeField] private bool Skeleton = false;

    [Header("Prefabs")]
    [SerializeField] private GameObject HeadPrefab;
    [SerializeField] private GameObject ChestPrefab;
    [SerializeField] private GameObject LegsPrefab;
    [SerializeField] private GameObject ShoesPrefab;
    [SerializeField] private GameObject WeaponPrefab;
    [SerializeField] private GameObject ArtifactPrefab;

    private bool bSpawned = false;

    private void Start()
    {
        if (Goblin)
        {
            goblin = GetComponent<Goblin>();
        }
        if(Wizard)
        {
            wizard = GetComponent<EvilWizard>();
        }
        if (Bat)
        {
            fly = GetComponent<FlyEnemy>();
        }
        if (DrillBat)
        {
            drillFly = GetComponent<DrillFlyEnemy>();
        }
        if(Mushroom)
        {
            mushroom = GetComponent<Mushroom>();
        }
        if(Skeleton)
        {
            skeleton = GetComponent<Skeleton>();
        }
    }

    private void Update()
    {
        if(!bSpawned)
        {
            if (Goblin)
            {
                if (goblin.IsDead())
                {
                    if(Head)
                    {
                        Instantiate(HeadPrefab, goblin.transform.position, Quaternion.identity);
                    }
                    if(Chest)
                    {
                        Instantiate(ChestPrefab, goblin.transform.position, Quaternion.identity);
                    }
                    if(Legs)
                    {
                        Instantiate(LegsPrefab, goblin.transform.position, Quaternion.identity);
                    }
                    if(Shoes)
                    {
                        Instantiate(ShoesPrefab, goblin.transform.position, Quaternion.identity);
                    }
                    if (Weapon)
                    {
                        Instantiate(WeaponPrefab, goblin.transform.position, Quaternion.identity);
                    }
                    if(Artifact)
                    {
                        Instantiate(ArtifactPrefab, goblin.transform.position, Quaternion.identity);
                    }
                    bSpawned = true;
                }
            }
            if (Wizard)
            {
                if (wizard.IsDead())
                {
                    if (Head)
                    {
                        Instantiate(HeadPrefab, wizard.transform.position, Quaternion.identity);
                    }
                    if (Chest)
                    {
                        Instantiate(ChestPrefab, wizard.transform.position, Quaternion.identity);
                    }
                    if (Legs)
                    {
                        Instantiate(LegsPrefab, wizard.transform.position, Quaternion.identity);
                    }
                    if (Shoes)
                    {
                        Instantiate(ShoesPrefab, wizard.transform.position, Quaternion.identity);
                    }
                    if (Weapon)
                    {
                        Instantiate(WeaponPrefab, wizard.transform.position, Quaternion.identity);
                    }
                    if (Artifact)
                    {
                        Instantiate(ArtifactPrefab, wizard.transform.position, Quaternion.identity);
                    }
                    bSpawned = true;
                }
            }
            if (Bat)
            {
                if (fly.IsDead())
                {
                    if (Head)
                    {
                        Instantiate(HeadPrefab, fly.transform.position, Quaternion.identity);
                    }
                    if (Chest)
                    {
                        Instantiate(ChestPrefab, fly.transform.position, Quaternion.identity);
                    }
                    if (Legs)
                    {
                        Instantiate(LegsPrefab, fly.transform.position, Quaternion.identity);
                    }
                    if (Shoes)
                    {
                        Instantiate(ShoesPrefab, fly.transform.position, Quaternion.identity);
                    }
                    if (Weapon)
                    {
                        Instantiate(WeaponPrefab, fly.transform.position, Quaternion.identity);
                    }
                    if (Artifact)
                    {
                        Instantiate(ArtifactPrefab, fly.transform.position, Quaternion.identity);
                    }
                    bSpawned = true;
                }
            }
            if (DrillBat)
            {
                if (drillFly.IsDead())
                {
                    if (Head)
                    {
                        Instantiate(HeadPrefab, drillFly.transform.position, Quaternion.identity);
                    }
                    if (Chest)
                    {
                        Instantiate(ChestPrefab, drillFly.transform.position, Quaternion.identity);
                    }
                    if (Legs)
                    {
                        Instantiate(LegsPrefab, drillFly.transform.position, Quaternion.identity);
                    }
                    if (Shoes)
                    {
                        Instantiate(ShoesPrefab, drillFly.transform.position, Quaternion.identity);
                    }
                    if (Weapon)
                    {
                        Instantiate(WeaponPrefab, drillFly.transform.position, Quaternion.identity);
                    }
                    if (Artifact)
                    {
                        Instantiate(ArtifactPrefab, drillFly.transform.position, Quaternion.identity);
                    }
                    bSpawned = true;
                }
            }
            if (Mushroom)
            {
                if (mushroom.IsDead())
                {
                    if (Head)
                    {
                        Instantiate(HeadPrefab, mushroom.transform.position, Quaternion.identity);
                    }
                    if (Chest)
                    {
                        Instantiate(ChestPrefab, mushroom.transform.position, Quaternion.identity);
                    }
                    if (Legs)
                    {
                        Instantiate(LegsPrefab, mushroom.transform.position, Quaternion.identity);
                    }
                    if (Shoes)
                    {
                        Instantiate(ShoesPrefab, mushroom.transform.position, Quaternion.identity);
                    }
                    if (Weapon)
                    {
                        Instantiate(WeaponPrefab, mushroom.transform.position, Quaternion.identity);
                    }
                    if (Artifact)
                    {
                        Instantiate(ArtifactPrefab, mushroom.transform.position, Quaternion.identity);
                    }
                    bSpawned = true;
                }
            }
            if (Skeleton)
            {
                if (skeleton.IsDead())
                {
                    if (Head)
                    {
                        Instantiate(HeadPrefab, skeleton.transform.position, Quaternion.identity);
                    }
                    if (Chest)
                    {
                        Instantiate(ChestPrefab, skeleton.transform.position, Quaternion.identity);
                    }
                    if (Legs)
                    {
                        Instantiate(LegsPrefab, skeleton.transform.position, Quaternion.identity);
                    }
                    if (Shoes)
                    {
                        Instantiate(ShoesPrefab, skeleton.transform.position, Quaternion.identity);
                    }
                    if (Weapon)
                    {
                        Instantiate(WeaponPrefab, skeleton.transform.position, Quaternion.identity);
                    }
                    if (Artifact)
                    {
                        Instantiate(ArtifactPrefab, skeleton.transform.position, Quaternion.identity);
                    }
                    bSpawned = true;
                }
            }
        }
    }


}
