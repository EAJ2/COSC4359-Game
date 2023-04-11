using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class TextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public GameObject mytext;

    private void Start()
    {
        mytext.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mytext.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mytext.SetActive(false);
    }
}
