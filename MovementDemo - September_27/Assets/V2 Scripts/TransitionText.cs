using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionText : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DisplayText());
    }

    public IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}
