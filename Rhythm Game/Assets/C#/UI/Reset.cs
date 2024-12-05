using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reset : MonoBehaviour
{
    [SerializeField] Metronome met;
    [SerializeField] FlyUp flyUp;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI text;

    public void HideIfNoScore ()
    {
        if (met.score > 0)
        {
            img.color = Color.white;
            text.text = "Reset";
        }
        else
        {
            img.color = Color.clear;
            text.text = "";
        }
    }

    public void ResetScores ()
    {
        met.score = 0f;
        met.tempo = 60f;
        flyUp.Fly (true);
    }
}
