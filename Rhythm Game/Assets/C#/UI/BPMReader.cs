/*
    Displays the current BPM for the player.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BPMReader : MonoBehaviour
{
    [SerializeField] Metronome met;
    [SerializeField] TextMeshProUGUI text;

    void Update ()
    {
        text.text = met.tempo + " bpm";
    }
}
