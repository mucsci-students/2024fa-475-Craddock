using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideButton : MonoBehaviour
{
    public bool hidden = false;
    [SerializeField] TextMeshProUGUI text;

    void Start ()
    {
        if (hidden)
        {
            text.alpha = 0f;
            hidden = false;
        }
    }

    public void Toggle ()
    {
        if (hidden)
        {
            text.alpha = 0f;
            hidden = false;
        }
        else
        {
            text.alpha = 255f;
            hidden = true;
        }
    }
}
