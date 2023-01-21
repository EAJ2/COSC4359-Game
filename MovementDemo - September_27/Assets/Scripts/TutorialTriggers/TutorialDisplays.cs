using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplays : MonoBehaviour
{
    [SerializeField] private GameObject MoveImage;
    [SerializeField] private GameObject JumpImage;
    [SerializeField] private GameObject SpikesImage;
    [SerializeField] private GameObject AttackImage;
    [SerializeField] private GameObject DashImage;
    public ExoV3Movement player;
    

    private void Awake()
    {
        player.GetComponent<ExoV3Movement>();
        MoveImage.SetActive(false);
        JumpImage.SetActive(false);
        AttackImage.SetActive(false);
        DashImage.SetActive(false);
        SpikesImage.SetActive(false);
    }

    private IEnumerator ActivateMoveImageCoroutine()
    {
        yield return new WaitForSeconds(2);
        MoveImage.SetActive(true);
        player.EnableWalkingMovement();
    }

    public void EnableMoveImage()
    {
        StartCoroutine(ActivateMoveImageCoroutine());
        player.SetGravityScale(2f);
        player.EnableHealthBar();
    }

    public void DisableMoveImage()
    {
        MoveImage.SetActive(false);
    }

    public void EnableJumpImage()
    {
        JumpImage.SetActive(true);
        SpikesImage.SetActive(true);

        player.EnableJumpingMovement();
    }

    public void DisableJumpImage()
    {
        JumpImage.SetActive(false);
        SpikesImage.SetActive(false);
    }

    public void EnableAttackImage()
    {
        AttackImage.SetActive(true);
        player.EnableAttack();
    }

    public void DisableAttackImage()
    {
        AttackImage.SetActive(false);
    }

    public void EnableDashImage()
    {
        DashImage.SetActive(true);
        player.EnableDashMovement();
    }

    public void DisableDashImage()
    {
        DashImage.SetActive(false);
    }
}
