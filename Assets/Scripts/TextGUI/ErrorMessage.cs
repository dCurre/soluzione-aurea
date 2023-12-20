using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{

    public TextMeshProUGUI errorMessage;
    private bool shown = false;
    void Update()
    {
        if (shown)
        {
            StartCoroutine(wait(4));
        }

        errorMessage.enabled = shown;
    }
    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        shown = false;
    }

    public void setTextAndShow(string text)
    {
        errorMessage.SetText(text);
        shown = true;
    }
}
