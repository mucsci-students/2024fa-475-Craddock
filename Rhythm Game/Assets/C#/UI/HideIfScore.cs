/*
    A script that changes an object's visibility depending on whether the player's score is > 0.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfScore : MonoBehaviour
{

    [SerializeField] Metronome met;
    public bool hideIfScore = true;

    public void Begin ()
    {
        if (met.score > 0)
            gameObject.SetActive (!hideIfScore);
        else
            gameObject.SetActive (hideIfScore);
    }
}
