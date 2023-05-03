using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float BoostSpeed;
    private float NormalSpeed;
    private bool hit;
    public int direction;

    private Animator anim;
    private BoxCollider2D boxCollider;
    public Transform playerTransform;
    public GameObject player;

    public Stats stats;

    public GameObject DMG_Text;
    public TextMesh dmgTextMesh;

    public AudioSource hitAudio;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<Stats>();

        if (playerTransform.localScale.x < 0)
        {
            speed = -speed;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            speed = speed;
        }
        boxCollider = GetComponent<BoxCollider2D>();

        NormalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "PyromaniacEnemy")
        {
            collision.gameObject.GetComponent<EvilWizard>().TakeDMG(stats.dmg);
            hitAudio.Play();
            Destroy(this.gameObject);
            dmgTextMesh.text = stats.dmg.ToString();
            Instantiate(DMG_Text, new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z), Quaternion.identity);
        }
        else if (collision.gameObject.tag == "GoblinEnemy")
        {
            collision.gameObject.GetComponent<Goblin>().TakeDMG(stats.dmg);
            hitAudio.Play();
            Destroy(this.gameObject);
            dmgTextMesh.text = stats.dmg.ToString();
            Instantiate(DMG_Text, new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z), Quaternion.identity);
        }
        else if (collision.gameObject.tag == "DrillFlyEnemy")
        {
            collision.gameObject.GetComponent<DrillFlyEnemy>().TakeDamage(stats.dmg);
            hitAudio.Play();
            Destroy(this.gameObject);
            dmgTextMesh.text = stats.dmg.ToString();
            Instantiate(DMG_Text, new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z), Quaternion.identity);
        }
        else if (collision.gameObject.tag == "MushroomEnemy")
        {
            collision.gameObject.GetComponent<Mushroom>().TakeDamage(stats.dmg);
            hitAudio.Play();
            Destroy(this.gameObject);
            dmgTextMesh.text = stats.dmg.ToString();
            Instantiate(DMG_Text, new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z), Quaternion.identity);
        }
        else if (collision.gameObject.tag == "FlyEnemy")
        {
            collision.gameObject.GetComponent<FlyEnemy>().TakeDamage(stats.dmg);
            hitAudio.Play();
            Destroy(this.gameObject);
            dmgTextMesh.text = stats.dmg.ToString();
            Instantiate(DMG_Text, new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z), Quaternion.identity);
        }
        else if (collision.gameObject.tag == "GroundEnemy")
        {
            collision.gameObject.GetComponent<Skeleton>().TakeDMG(stats.dmg);
            hitAudio.Play();
            Destroy(this.gameObject);
            dmgTextMesh.text = stats.dmg.ToString();
            Instantiate(DMG_Text, new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z), Quaternion.identity);
        }
        else if (collision.gameObject.tag == "Reaper_Boss")
        {
            collision.gameObject.GetComponent<Boss_Health>().TakeDMG(stats.dmg);
            hitAudio.Play();
            Destroy(this.gameObject);
            dmgTextMesh.text = stats.dmg.ToString();
            Instantiate(DMG_Text, new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z), Quaternion.identity);
        }
    }

    public void EquipWeapon()
    {
        speed = BoostSpeed;
    }

    public void UnequipWeapon()
    {
        speed = NormalSpeed;
    }
}
