using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{

    public TextMesh text;
    public byte alpha = 255;
    public int direction;
    public bool isCrit;

    // Start is called before the first frame update
    void Awake()
    {
        InvokeRepeating("FadeText", 0f, 0.1f);
        direction = Random.Range(0, 2);
    }

    public void FadeText()
    {
        alpha -= 15;
        if (isCrit == true)
        {
            text.color = new Color32(255, 0, 0, alpha);
        }
        else
        {
            text.color = new Color32(255, 255, 255, alpha);
        }
        
        if (direction == 0)
        {
            transform.position = new Vector3(transform.position.x + 0.040f, transform.position.y + 0.040f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 0.040f, transform.position.y + 0.040f, transform.position.z);
        } 
        
        
        if (alpha <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
