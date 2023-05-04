using UnityEngine;

public class SuperSpeed : MonoBehaviour
{
    private V2PlayerMovement pm;
    private Player player;

    [Header("Time Parameters")]
    [SerializeField] private float CooldownTime;
    private float CooldownTimer = 0;
    [SerializeField] private float DurationTime;
    private float DurationTimer = 0;

    private bool bActivated = false;
    private bool bEquipped = false;

    private void Start()
    {
        pm = GetComponentInParent<V2PlayerMovement>();
        player = GetComponentInParent<Player>();
        CooldownTimer = CooldownTime;
    }

    private void Update()
    {
        if (bEquipped)
        {
            player.SetAbility3Status();
            if (CooldownTimer >= CooldownTime)
            {
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    bActivated = true;
                    CooldownTimer = 0;
                    pm.ActivateAbility3();
                }
            }
            if (bActivated)
            {
                DurationTimer += Time.deltaTime;
                if (DurationTimer >= DurationTime)
                {
                    bActivated = false;
                    pm.DeactivateAbility3();
                    DurationTimer = 0;
                }
            }
            else
            {
                CooldownTimer += Time.deltaTime;
            }
        }
    }

    public bool IsReady()
    {
        return (CooldownTimer >= CooldownTime);
    }

    public void EquipAbility3()
    {
        bEquipped = true;
    }

    public void UnequipAbility3()
    {
        bEquipped = false;
    }
}