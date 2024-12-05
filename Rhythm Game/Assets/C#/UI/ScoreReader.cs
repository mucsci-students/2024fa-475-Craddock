using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreReader : MonoBehaviour
{
    [SerializeField] Metronome met;
    [SerializeField] TextMeshProUGUI text;

    void Update ()
    {
        text.text = Mathf.Round (met.score * 10f) / 10f + " pts";
    }
}
