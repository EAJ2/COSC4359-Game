using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAbility : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float FallSpeed;
    private SpriteRenderer sr;

    private Vector2 MousePosition;
    private Vector2 WorldPosition;

    private bool bEquipped = false;

    [SerializeField] private float CooldownTime;
    private float CooldownTimer;

    [SerializeField] private float DurationTime;
    private float DurationTimer = 0;

    [SerializeField] private List<GameObject> Arrows;
    [SerializeField] private List<Transform> Positions;
    [SerializeField] private GameObject FallLocation;

    private float NumberOfArrows = 0;

    private bool bActivated = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        NumberOfArrows = Arrows.Count;
        for(int i = 0; i < NumberOfArrows; i++)
        {
            Arrows[i].transform.position = Positions[i].position;
        }

        for (int i = 0; i < NumberOfArrows; i++)
        {
            Arrows[i].SetActive(false);
        }

        CooldownTimer = CooldownTime;
    }

    private void Update()
    {
        if(bEquipped)
        {
            if(!bActivated)
            {
                CooldownTimer += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Alpha9) && !bActivated && CooldownTimer >= CooldownTime)
            {
                sr.enabled = true;
                MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                WorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);
                transform.position = Vector3.MoveTowards(transform.position, WorldPosition, Speed * Time.deltaTime);
            }
            if (!Input.GetKey(KeyCode.Alpha9) && !bActivated)
            {
                if(CooldownTimer >= CooldownTime)
                {
                    if(Input.GetKeyUp(KeyCode.Alpha9))
                    {
                        Debug.Log("Key Let Go!");
                        sr.enabled = false;
                        for(int i = 0; i < NumberOfArrows; i++)
                        {
                            Arrows[i].SetActive(true);
                        }
                        bActivated = true;
                        CooldownTimer = 0;
                    }
                }
            }
            if(bActivated)
            {
                if(DurationTimer >= DurationTime)
                {
                    DurationTimer = 0;
                    bActivated = false;
                    for (int i = 0; i < NumberOfArrows; i++)
                    {
                        Arrows[i].SetActive(false);
                    }
                }
                else
                {
                    DurationTimer += Time.deltaTime;
                    for (int i = 0; i < NumberOfArrows; i++)
                    {
                        Arrows[i].transform.position = Vector3.MoveTowards(Arrows[i].transform.position, new Vector3(Arrows[i].transform.position.x, FallLocation.transform.position.y, Arrows[i].transform.position.z), FallSpeed * Time.deltaTime);
                        if (Arrows[i].transform.position.y == FallLocation.transform.position.y)
                        {
                            Arrows[i].transform.position = Positions[i].position;
                        }
                    }
                }
            }
        }
    }

    public void SetEquipped()
    {
        bEquipped = true;
    }

    public void Unequip()
    {
        bEquipped = false;
    }

    public void ResetArrow(int x)
    {
        Arrows[x].transform.position = Positions[x].position;
    }
}
