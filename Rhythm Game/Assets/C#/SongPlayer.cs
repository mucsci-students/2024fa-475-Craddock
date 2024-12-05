using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class SongPlayer : MonoBehaviour
{
    public bool began = false;
    [SerializeField] MidiFilePlayer player;
    [SerializeField] Metronome met;
    string song = "All Night Long";

    // from chatGPT
    void Start ()
    {
        player.OnEventStartPlayMidi.AddListener (SetCustomTempo);
    }

    // from chatGPT
    void SetCustomTempo (string midiName)
    {
        player.MPTK_Tempo = met.tempo;
    }

    public void Begin ()
    {
        began = true;
        print (song);
        player.MPTK_MidiName = song;
        player.MPTK_Play();
    }

    public void Stop ()
    {
        began = false;
        player.MPTK_Stop();
    }

    public void UpdateTempo (float newTempo)
    {
        player.MPTK_Tempo = newTempo;
    }

    public void SetSong (int selection)
    {
        switch (selection)
        {
            case 1:
                song = "Bobby Blue Band - A Touch Of Blues";
                break;
            case 2:
                song = "Christopher Cross - Sailing";
                break;
            case 3:
                song = "Dark Cloud 2 - Dark Chronicle";
                break;
            case 4:
                song = "Louis Armstrong - What A Wonderful World";
                break;
            case 5:
                song = "Mike Oldfield - Moonlight_Shadow";
                break;
            case 6:
                song = "Tetris2";
                break;
            case 7:
                song = "Van Morrison - Wild Nights";
                break;
            default:
                song = "All Night Long";
                break;
        }
    }
}
