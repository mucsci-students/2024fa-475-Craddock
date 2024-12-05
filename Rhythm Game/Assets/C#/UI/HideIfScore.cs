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
