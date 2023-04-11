using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(Disable());
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }

    public IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.5f);
        this.enabled = false;
    }
}
