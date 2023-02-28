using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ConsumableInventory : MonoBehaviour
{
    public GameObject healthPotion;
    public GameObject staminaPotion;
    public GameObject manaPotion;

    public RectTransform currentItem_Pos;
    public RectTransform previousItem_Pos;
    public RectTransform nextItem_Pos;

    public int index = 0;

    [SerializeField]public Dictionary<GameObject, int> consInv = new Dictionary<GameObject, int>();

    // Start is called before the first frame update
    void Start()
    {
        consInv.Add(healthPotion, 0);
        consInv.Add(manaPotion, 0);
        consInv.Add(staminaPotion, 0);
        Debug.Log(consInv.Count);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            consInv[healthPotion] += 1;
            consInv[staminaPotion] += 1;
            consInv[manaPotion] += 1;
            InventoryCheck();
        }

       if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) // forward
       {
            Debug.Log("Mouse is Scrolling");
            index += 1;
            
            if (index < 0)
            {
                index = consInv.Count - 1;
            }
            else if (index >= consInv.Count)
            {
                index = 0;
            }
            
       }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) // backward
        {
            Debug.Log("Mouse is Scrolling");
            index -= 1;
            
            if (index < 0)
            {
                index = consInv.Count - 1;
            }
            else if (index >= consInv.Count)
            {
                index = 0;
            }
            
        }
    }

    public void InventoryCheck()
    {
        if (index == 0)
        {
            if (consInv.ElementAt(index).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
            }

            if (consInv.ElementAt(index + 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(nextItem_Pos);
            }

            if (consInv.ElementAt(consInv.Count - 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
            }
        }
        else if (index == consInv.Count - 1)
        {
            if (consInv.ElementAt(index).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
            }

            if (consInv.ElementAt(0).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
            }

            if (consInv.ElementAt(index - 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
            }
        }
    }
}
