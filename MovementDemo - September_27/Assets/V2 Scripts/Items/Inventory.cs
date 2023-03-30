using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAction { USE, EQUIP, DROP }

public class InventoryV2 : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    #region Singleton
    public static InventoryV2 instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public void AddItem(Item item)
    {
        items.Add(item);
        if(OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        if(OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

}