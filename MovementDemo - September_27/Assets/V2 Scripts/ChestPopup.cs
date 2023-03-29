using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPopup : MonoBehaviour
{
    public TextMesh text;
    public byte alpha = 255;
    public string type;
    public SpriteRenderer sr;


    void Awake()
    {
        if (type == "Text")
        {
            text = this.GetComponent<TextMesh>();
        }
        else
        {
            sr = this.GetComponent<SpriteRenderer>();
        }

        InvokeRepeating("Fade", 0f, 0.1f);
    }

    public void Fade()
    {
        alpha -= 15;

        if (type == "Text")
        {
            text.color = new Color32(255, 255, 255, alpha);
        }
        else
        {
            sr.color = new Color32(255, 255, 255, alpha);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.040f, transform.position.z);

        if (alpha <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
