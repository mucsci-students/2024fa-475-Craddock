/*
    The Metronome class keeps track of the current beat, where 0 is the first beat, 1 is the second beat, etc.
    The 'note' variable stores the current beat.

    It also contains the player's current score, and it increases the tempo as the score increases.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class Metronome : MonoBehaviour
{
    public float tempo = 60f; // in bpm
    public float score = 0f;
    public float timeBetweenBeats {
        get { return 60f / tempo; }
    }
    public float beatsPerSecond {
        get { return tempo / 60f; }
    }
    public float note { get; private set; }

    float delayFromStart = 8f;
    [SerializeField] float timeOfSongStart = -4f;
    [SerializeField] SongPlayer player;
    bool began = false;
    
    public void Begin ()
    {
        began = true;
        note = -delayFromStart;
    }

    public void Stop ()
    {
        began = false;
    }

    void Update()
    {
        if (began)
        {
            note += beatsPerSecond * Time.deltaTime;
            if (Mathf.Round (score * 10f) / 50f + 50f > tempo)
                player.UpdateTempo (++tempo);
            if (!player.began && note > timeOfSongStart)
                player.Begin ();
        }
    }

}
