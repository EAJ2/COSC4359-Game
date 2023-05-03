using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class ConsumableInventory : MonoBehaviour
{
    public GameObject healthPotion;
    public GameObject staminaPotion;
    public GameObject manaPotion;

    public RectTransform currentItem_Pos;
    public RectTransform previousItem_Pos;
    public RectTransform nextItem_Pos;
    public RectTransform HUD;

    public int index = 0;
    public int twoLengthCounter = 0;
    private GameObject p1;
    private GameObject p2;

    [SerializeField]public Dictionary<GameObject, int> consInv = new Dictionary<GameObject, int>();
    public int invLength;

    public Stats stats;
    public V2Health healthScript;
    public V2PlayerMovement movementScript;

    public Text selectedCountText;
    public Text previousCountText;
    public Text nextCountText;

    public GameObject HP_effect;
    public GameObject SP_effect;
    public GameObject MP_effect;

    // Start is called before the first frame update
    void Start()
    {
        consInv.Add(healthPotion, 0);
        consInv.Add(manaPotion, 0);
        consInv.Add(staminaPotion, 0);
        InventoryCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
       if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            consInv[healthPotion] += 1;
            consInv[staminaPotion] += 1;
            consInv[manaPotion] += 1;
            InventoryCheck();
            //Debug.Log("Length" + InventoryLength().ToString());
        }
        
       if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) // forward
       {
            index += 1;
            
            if (index < 0)
            {
                index = consInv.Count - 1;
            }
            else if (index >= consInv.Count)
            {
                index = 0;
            }


            InventoryCheck();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) // backward
        {
            index -= 1;
            
            if (index < 0)
            {
                index = consInv.Count - 1;
            }
            else if (index >= consInv.Count)
            {
                index = 0;
            }

            InventoryCheck();
        }

        ParentSet();

        Consumable();
    }

    public void InventoryCheck()
    {
        if (InventoryLength() == 0)
        {
            ClearDisplay();
            selectedCountText.text = "";
            previousCountText.text = "";
            nextCountText.text = "";
        }
        else if (InventoryLength() == 1 /*&& IconVisible(GetOneIndex().tag) == false*/)
        {
            ClearDisplay();
            Instantiate(GetOneIndex(), new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
            GameObject.FindGameObjectWithTag(GetOneIndex().tag).transform.SetParent(currentItem_Pos);
            selectedCountText.text = "x" + consInv[GetOneIndex()].ToString();
            previousCountText.text = "";
            nextCountText.text = "";
        }
        else if (InventoryLength() == 2)
        {
            ClearDisplay();
            if (twoLengthCounter % 2 == 0)
            {
                GetTwoIndex(out p1, out p2);
            }
            else
            {
                GetTwoIndexReverse(out p1, out p2);
            }
            Instantiate(p1, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
            GameObject.FindGameObjectWithTag(p1.tag).transform.SetParent(currentItem_Pos);

            Instantiate(p2, new Vector3(previousItem_Pos.transform.position.x, previousItem_Pos.transform.position.y, previousItem_Pos.transform.position.z), Quaternion.identity);
            Instantiate(p2, new Vector3(nextItem_Pos.transform.position.x, nextItem_Pos.transform.position.y, nextItem_Pos.transform.position.z), Quaternion.identity);

            GameObject[] p2Icons;
            p2Icons = GameObject.FindGameObjectsWithTag(p2.tag);
            for (int i = 0; i < p2Icons.Length; i++)
            {
                p2Icons[i].transform.SetParent(HUD);
            }
            twoLengthCounter++;
            selectedCountText.text = "x" + consInv[p1].ToString();
            previousCountText.text = "x" + consInv[p2].ToString();
            nextCountText.text = "x" + consInv[p2].ToString();
        }
        else if (index == 0 && InventoryLength() > 2)
        {
            ClearDisplay();
            if (consInv.ElementAt(index).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
                selectedCountText.text = "x" + consInv[consInv.ElementAt(index).Key].ToString();
            }

            if (consInv.ElementAt(index + 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(index + 1).Key, new Vector3(nextItem_Pos.transform.position.x, nextItem_Pos.transform.position.y, nextItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index + 1).Key.tag).transform.SetParent(nextItem_Pos);
                nextCountText.text = "x" + consInv[consInv.ElementAt(index + 1).Key].ToString();
            }

            if (consInv.ElementAt(consInv.Count - 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(consInv.Count - 1).Key, new Vector3(previousItem_Pos.transform.position.x, previousItem_Pos.transform.position.y, previousItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(consInv.Count - 1).Key.tag).transform.SetParent(previousItem_Pos);
                previousCountText.text = "x" + consInv[consInv.ElementAt(consInv.Count - 1).Key].ToString();
            }
        }
        else if (index == consInv.Count - 1 && InventoryLength() > 2)
        {
            ClearDisplay();
            if (consInv.ElementAt(index).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
                selectedCountText.text = "x" + consInv[consInv.ElementAt(index).Key].ToString();
            }

            if (consInv.ElementAt(0).Value > 0)
            {
                Instantiate(consInv.ElementAt(0).Key, new Vector3(nextItem_Pos.transform.position.x, nextItem_Pos.transform.position.y, nextItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(0).Key.tag).transform.SetParent(nextItem_Pos);
                nextCountText.text = "x" + consInv[consInv.ElementAt(0).Key].ToString();
            }

            if (consInv.ElementAt(index - 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(index - 1).Key, new Vector3(previousItem_Pos.transform.position.x, previousItem_Pos.transform.position.y, previousItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index - 1).Key.tag).transform.SetParent(previousItem_Pos);
                previousCountText.text = "x" + consInv[consInv.ElementAt(index - 1).Key].ToString();
            }
        }
        else if (InventoryLength() > 2)
        {
            ClearDisplay();
            if (consInv.ElementAt(index).Value > 0)
            {
                Instantiate(consInv.ElementAt(index).Key, new Vector3(currentItem_Pos.transform.position.x, currentItem_Pos.transform.position.y, currentItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index).Key.tag).transform.SetParent(currentItem_Pos);
                selectedCountText.text = "x" + consInv[consInv.ElementAt(index).Key].ToString();
            }

            if (consInv.ElementAt(index + 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(index + 1).Key, new Vector3(nextItem_Pos.transform.position.x, nextItem_Pos.transform.position.y, nextItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index + 1).Key.tag).transform.SetParent(nextItem_Pos);
                nextCountText.text = "x" + consInv[consInv.ElementAt(index + 1).Key].ToString();
            }

            if (consInv.ElementAt(index - 1).Value > 0)
            {
                Instantiate(consInv.ElementAt(index - 1).Key, new Vector3(previousItem_Pos.transform.position.x, previousItem_Pos.transform.position.y, previousItem_Pos.transform.position.z), Quaternion.identity);
                GameObject.FindGameObjectWithTag(consInv.ElementAt(index - 1).Key.tag).transform.SetParent(previousItem_Pos);
                previousCountText.text = "x" + consInv[consInv.ElementAt(index - 1).Key].ToString();
            }
        }
    }

    public void ParentSet() 
    {
        try
        {
            GameObject.FindGameObjectWithTag("HealthPotionIcon").transform.SetParent(HUD);
            GameObject.FindGameObjectWithTag("StaminaPotionIcon").transform.SetParent(HUD);
            GameObject.FindGameObjectWithTag("ManaPotionIcon").transform.SetParent(HUD);
        } 
        catch (NullReferenceException ex)
        {
            
        }
        
        try
        {
            GameObject.FindGameObjectWithTag("StaminaPotionIcon").transform.SetParent(HUD);
        }
        catch (NullReferenceException ex)
        {

        }

        try
        {
            GameObject.FindGameObjectWithTag("ManaPotionIcon").transform.SetParent(HUD);
        }
        catch (NullReferenceException ex)
        {

        }

        try
        {
            GameObject.FindGameObjectWithTag("ManaPotionIcon").transform.SetParent(HUD);
        }
        catch (NullReferenceException ex)
        {

        }
    } 

    public void ClearDisplay()
    {
        GameObject[] hpIcons = GameObject.FindGameObjectsWithTag("HealthPotionIcon");
        for (int i = 0; i < hpIcons.Length; i++)
        {
            Destroy(hpIcons[i]);
        }

        GameObject[] stamIcons = GameObject.FindGameObjectsWithTag("StaminaPotionIcon");
        for (int i = 0; i < stamIcons.Length; i++)
        {
            Destroy(stamIcons[i]);
        }

        GameObject[] manaIcons = GameObject.FindGameObjectsWithTag("ManaPotionIcon");
        for (int i = 0; i < manaIcons.Length; i++)
        {
            Destroy(manaIcons[i]);
        }
    }

    public void Consumable()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (consInv.ElementAt(index).Key.CompareTag("HealthPotionIcon") && consInv.ElementAt(index).Value > 0 && InventoryLength() > 2)
            {
                Instantiate(HP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                healthScript.CurrentHealth += 30;
                consInv[consInv.ElementAt(index).Key]--;
            }
            else if (consInv.ElementAt(index).Key.CompareTag("StaminaPotionIcon") && consInv.ElementAt(index).Value > 0 && InventoryLength() > 2)
            {
                Instantiate(SP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                movementScript.stamina += 20;
                consInv[consInv.ElementAt(index).Key]--;
            }
            else if (consInv.ElementAt(index).Key.CompareTag("ManaPotionIcon") && consInv.ElementAt(index).Value > 0 && InventoryLength() > 2)
            {
                Instantiate(MP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                stats.mana += 10;
                consInv[consInv.ElementAt(index).Key]--;
            }
            else if (InventoryLength() == 1)
            {
                try
                {
                    GameObject potion = GetOneIndex();
                    if (potion.CompareTag("HealthPotionIcon"))
                    {
                        Instantiate(HP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                        healthScript.CurrentHealth += 30;
                        consInv[healthPotion]--;
                    }

                    if (potion.CompareTag("StaminaPotionIcon"))
                    {
                        Instantiate(SP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                        movementScript.stamina += 20;
                        consInv[staminaPotion]--;
                    }

                    if (potion.CompareTag("ManaPotionIcon"))
                    {
                        Instantiate(MP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                        stats.mana += 10;
                        consInv[manaPotion]--;
                    }
                } 
                catch (NullReferenceException ex)
                {

                }
            }
            else if (InventoryLength() == 2)
            {
                try
                {
                    GameObject potion = p1;
                    if (potion.CompareTag("HealthPotionIcon"))
                    {
                        Instantiate(HP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                        healthScript.CurrentHealth += 30;
                        consInv[healthPotion]--;
                    }

                    if (potion.CompareTag("StaminaPotionIcon"))
                    {
                        Instantiate(SP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                        movementScript.stamina += 20;
                        consInv[staminaPotion]--;
                    }

                    if (potion.CompareTag("ManaPotionIcon"))
                    {
                        Instantiate(MP_effect, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2, this.gameObject.transform.position.z), Quaternion.identity);
                        stats.mana += 10;
                        consInv[manaPotion]--;
                    }
                }
                catch (NullReferenceException ex)
                {

                }
                ClearDisplay();
            }
            InventoryCheck();
        }
    }

    public int InventoryLength()
    {
        int counter = 0;
        foreach (KeyValuePair<GameObject, int> potion in consInv)
        {
            if (potion.Value > 0)
            {
                counter += 1;
            }
        }
        return counter;
    }

    public GameObject GetOneIndex()
    {
        if (InventoryLength() == 1)
        {
            foreach (KeyValuePair<GameObject, int> potion in consInv)
            {
                if (potion.Value > 0)
                {
                    return potion.Key;
                }
            }
        }

        return null;
    }

    public void GetTwoIndex(out GameObject p1, out GameObject p2)
    {
        p1 = null;
        p2 = null;
        foreach (KeyValuePair<GameObject, int> potion in consInv)
        {
            if (potion.Value > 0 && p1 == null)
            {
                p1 = potion.Key;
            }
        }

        foreach (KeyValuePair<GameObject, int> potion in consInv)
        {
            if (potion.Value > 0 && p2 == null && p1 != potion.Key)
            {
                p2 = potion.Key;
            }
        }

    }

    public void GetTwoIndexReverse(out GameObject p1, out GameObject p2)
    {
        p1 = null;
        p2 = null;
        foreach (KeyValuePair<GameObject, int> potion in consInv)
        {
            if (potion.Value > 0 && p2 == null)
            {
                p2 = potion.Key;
            }
        }

        foreach (KeyValuePair<GameObject, int> potion in consInv)
        {
            if (potion.Value > 0 && p1 == null && p2 != potion.Key)
            {
                p1 = potion.Key;
            }
        }

    }

    public bool IconVisible(string tag)
    {
        GameObject icon = GameObject.FindGameObjectWithTag(tag);
        if (icon == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void buyHealthPotion()
    {
        consInv[healthPotion] += 1;
        InventoryCheck();
    }
    public void buyManaPotion()
    {
        consInv[manaPotion] += 1;
        InventoryCheck();
    }
    public void buyStaminaPotion()
    {
        consInv[staminaPotion] += 1;
        InventoryCheck();
    }
}
