using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootForce = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);
        }
    }
}
