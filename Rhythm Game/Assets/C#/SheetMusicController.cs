/*
    The SheetMusicController script is attached to the sheet music controller GameObject. It generates and moves
    the music notes across the screen.

    The WriteNextBar() method randomly generates a new bar based on the current 'phase', which increases as the player's score increases.
    More difficult methods of generation are used as the phase increases.
    
    The CreateBar() method uses the generated bar to instantiate the actual music notes that the player sees on screen.
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheetMusicController : MonoBehaviour
{
    public List<List<bool>> sheet = new List<List<bool>> ();
    int barCount = 0;
    int destroyedBars = 0;
    int destroyedBeats = 0;
    int numBars {
        get { return (destroyedBars + barCount); }
    }
    int maxBarCount = 5;
    int maxWrittenBarCount = 5;
    float noteLength = 3f;
    float barLength {
        get { return noteLength * 4f + 0.5f; }
    }
    float avgNoteLength {
        get { return barLength / 4f; }
    }

    bool began = false;
    int phase = 0;
    List<float> scoreOfNextPhase = new List<float> {0f, 0f, 0f, 0f, 5f, 0f, 10f, 0f, 0f, 0f, 0f, 0f, 20f, 0f, 0f, 0f, 0f, 0f, 0f, 40f, 0f, 0f, 0f, 50f, 0f, 65f, 0f, 80f, 0f, 95f, 0f, 0f, float.PositiveInfinity};
    List<bool> isTutorialPhase = new List<bool> ();

    [SerializeField] Metronome met;
    [SerializeField] InputController input;
    [SerializeField] GameObject childPrefab;
    [SerializeField] GameObject notePrefab;
    [SerializeField] List<Sprite> musicNoteSprites;
    [SerializeField] Sprite barSprite;

    private void WriteNextBar ()
    {
        List<bool> bar = Enumerable.Repeat(false, 48).ToList();
        switch (phase)
        {
            case 0:
            {
                /*
                bar[0] = bar[3] = bar[6] = bar[9] = true;
                bar[12] = bar[15] = bar[18] = bar[21] = true;
                bar[24] = bar[27] = bar[30] = bar[33] = true;
                bar[36] = bar[39] = bar[42] = bar[45] = true;
                */
                bar[0] = bar[12] = bar[24] = bar[36] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 1:
            {
                bar[0] = bar[12] = bar[36] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 2:
            {
                bar[0] = bar[12] = bar[24] = bar[36] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 3:
            {
                bar[0] = bar[12] = bar[36] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 4: // 5 score
            {
                bar[0] = bar[12] = bar[24] = bar[36] = true;
                bar[Random.Range(0, 4) * 12] = false;
                isTutorialPhase.Add(false);
                break;
            }
            case 5:
            {
                bar[0] = bar[12] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 6: // 10 score
            {
                bar[0] = bar[12] = bar[24] = bar[36] = true;
                bar[Random.Range(0, 4) * 12] = false;
                bar[Random.Range(0, 4) * 12] = false;
                isTutorialPhase.Add(false);
                break;
            }
            case 8:
            {
                bar[0] = bar[6] = bar[24] = bar[30] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 9:
            {
                bar[0] = bar[12] = bar[18] = bar[24] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 10:
            {
                bar[0] = bar[6] = bar[24] = bar[30] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 11:
            {
                bar[0] = bar[12] = bar[18] = bar[24] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 12: // 20 score
            {
                for (int i = 0; i < 8; ++i) bar[i * 6] = true;
                bar[Random.Range(0, 4) * 12 + 6] = false;
                bar[Random.Range(0, 4) * 12 + 6] = false;
                int j = Random.Range(0, 4);
                bar[j * 12] = bar[j * 12 + 6] = false;
                isTutorialPhase.Add(false);
                break;
            }
            case 13:
            {
                bar[0] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 14:
            {
                bar[0] = bar[6] = bar[18] = bar[24] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 15:
            {
                bar[0] = bar[6] = bar[18] = bar[24] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 16:
            {
                bar[0] = bar[18] = bar[24] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 17:
            {
                bar[0] = bar[18] = bar[24] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 18:
            {
                bar[0] = bar[6] = bar[18] = bar[30] = bar[42] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 19: // 40 score
            {
                for (int i = 0; i < 8; ++i) bar[i * 6] = true;
                for (int i = 0; i < 4; ++i) bar[Random.Range (0, 8) * 6] = false;
                isTutorialPhase.Add(false);
                break;
            }
            case 21:
            {
                bar[0] = bar[12] = bar[18] = bar[24] = bar[27] = bar[30] = bar[33] = bar[36] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 22:
            {
                bar[0] = bar[12] = bar[18] = bar[24] = bar[27] = bar[30] = bar[33] = bar[36] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 23: // 50 score
            {
                for (int i = 0; i < 8; ++i) bar[i * 6] = true;
                for (int i = 0; i < 4; ++i) bar[Random.Range (0, 8) * 6] = false;
                if (Random.Range (0, 2) == 0)
                {
                    int j = Random.Range (0, 4);
                    for (int i = 0; i < 4; ++i)
                        bar[i * 3 + j * 12] = true;
                }
                isTutorialPhase.Add(false);
                break;
            }
            case 24:
            {
                bar[0] = bar[6] = bar[12] = bar[18] = bar[21] = bar[24] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 25: // 65 score
            {
                for (int l = 0; l < 8; ++l) bar[l * 6] = true;
                bar[Random.Range (0, 4) * 12 + 6] = false;
                int i = Random.Range (0, 4);
                bar[i * 12] = bar[i * 12 + 6] = false;
                int j = Random.Range (0, 4);
                for (int k = 0; k < 4; ++k)
                    bar[k * 3 + j * 12] = true;
                if (Random.Range (0, 2) == 0)
                    bar[3 + j * 12] = false;
                isTutorialPhase.Add(false);
                break;
            }
            case 26:
            {
                bar[0] = bar[3] = bar[6] = bar[9] = bar[12] = bar[15] = bar[18] = bar[24] = bar[27] = bar[30] = bar[36] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 27: // 80 score
            {
                for (int l = 0; l < 8; ++l) bar[l * 6] = true;
                bar[Random.Range (0, 4) * 12 + 6] = false;
                int i = Random.Range (0, 4);
                bar[i * 12] = bar[i * 12 + 6] = false;
                int j = Random.Range (0, 4);
                for (int k = 0; k < 4; ++k)
                    bar[k * 3 + j * 12] = true;
                if (Random.Range (0, 2) == 0)
                    bar[9 + j * 12] = false;
                isTutorialPhase.Add(false);
                break;
            }
            case 28:
            {
                bar[0] = bar[6] = bar[12] = bar[24] = bar[27] = bar[33] = bar[36] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 29: // 95 score
            {
                for (int l = 0; l < 8; ++l) bar[l * 6] = true;
                bar[Random.Range (0, 4) * 12 + 6] = false;
                int i = Random.Range (0, 4);
                bar[i * 12] = bar[i * 12 + 6] = false;
                int j = Random.Range (0, 4);
                for (int k = 0; k < 4; ++k)
                    bar[k * 3 + j * 12] = true;
                if (Random.Range (0, 2) == 0)
                    bar[9 + j * 12] = false;
                isTutorialPhase.Add(false);
                break;
            }
            case 30:
            {
                bar[0] = bar[3] = bar[6] = bar[9] = true;
                bar[12] = bar[18] = bar[21] = true;
                bar[24] = bar[27] = bar[33] = true;
                bar[36] = bar[39] = bar[42] = true;
                isTutorialPhase.Add(true);
                break;
            }
            case 31:
            {
                bar[0] = bar[3] = bar[6] = bar[9] = true;
                bar[12] = bar[18] = bar[21] = true;
                bar[24] = bar[27] = bar[33] = true;
                bar[36] = bar[39] = bar[42] = true;
                isTutorialPhase.Add(false);
                break;
            }
            case 32: // 120 score
            {
                for (int i = 0; i < 8; ++i) bar[i * 6] = true;
                for (int i = 0; i < 4; ++i) bar[Random.Range (0, 8) * 6] = false;
                if (Random.Range (0, 2) == 0)
                {
                    int j = Random.Range (0, 4);
                    for (int i = 0; i < 4; ++i)
                        bar[i * 3 + j * 12] = true;
                    bar[3 * Random.Range (0, 4) + j * 12] = false;
                }
                isTutorialPhase.Add(false);
                break;
            }
            default:
            {
                isTutorialPhase.Add(false);
                break;
            }
        }

        if (met.score >= scoreOfNextPhase[phase])
            ++phase;

        sheet.Add (bar);
    }

    public void Begin ()
    {
        began = true;
        barCount = 0;
        destroyedBars = 0;
        destroyedBeats = 0;
        for (phase = 0; phase < scoreOfNextPhase.Count; ++phase)
            if (met.score <= scoreOfNextPhase[phase])
                break;
        print ("phase: " + phase); //debug
    }

    public void Stop ()
    {
        began = false;
        phase = 0;
        isTutorialPhase = new List<bool> ();
    }

    void Update()
    {
        if (began)
        {
            if (sheet.Count < maxWrittenBarCount)
            {
                WriteNextBar ();
            }
            if (barCount < maxBarCount)
            {
                CreateBar ();
            }
            Move ();
        }
    }

    private void CreateBar ()
    {
        
        Instantiate (childPrefab, transform.position + new Vector3 (barLength * numBars - noteLength, 0f, 0f), Quaternion.identity, transform).GetComponent<SpriteRenderer> ().sprite = barSprite;

        List<bool> bar = sheet[barCount];
        int i = 0;
        
        while (i < 4)
        {
            GameObject musicNote = Instantiate (notePrefab, transform.position + new Vector3 (barLength * (barCount + destroyedBars) + noteLength * i, 0f, 0f), Quaternion.identity, transform);

            SheetMusicChild smc = musicNote.GetComponent<SheetMusicChild> ();
            smc.controller = this;

            MusicNote mn = musicNote.GetComponent<MusicNote> ();
            mn.met = met;
            mn.beatNum = numBars * 4 + i;
            if (isTutorialPhase[0])
                mn.tutorialNote = true;


            SpriteRenderer rend = musicNote.GetComponent<SpriteRenderer> ();
            IEnumerable<bool> beat = bar;
            int index = 0;
            if (i < 1) // whole note
            {
                beat = bar.Skip (0).Take (48);
                index = IndexOfWholeBeat (beat);
                if (index != 0)
                {
                    rend.sprite = musicNoteSprites[index];
                    smc.len = 4;
                    i += 4;
                    for (int j = 0; j < 4; ++j)
                        input.notes.Add (mn);
                }
            }
            if (i < 2) // 3/4 note
            {
                beat = bar.Skip (12 * i).Take (36);
                index = IndexOf3QuartersBeat (beat);
                if (index != 0)
                {
                    rend.sprite = musicNoteSprites[index];
                    smc.len = 3;
                    i += 3;
                    for (int j = 0; j < 3; ++j)
                        input.notes.Add (mn);
                }
            }
            if (index == 0 && i < 3) // half note
            {
                beat = bar.Skip (12 * i).Take (24);
                index = IndexOfHalfBeat (beat);
                if (index != 0)
                {
                    rend.sprite = musicNoteSprites[index];
                    smc.len = 2;
                    i += 2;
                    for (int j = 2; j < 4; ++j)
                        input.notes.Add (mn);
                }
            }
            if (index == 0) // quarter note
            {
                beat = bar.Skip (12 * i).Take (12);
                index = IndexOfQuarterBeat (beat);
                rend.sprite = musicNoteSprites[index];
                smc.len = 1;
                ++i;
                input.notes.Add (mn);
            }

            musicNote.transform.position += new Vector3 (noteLength * BeatOffsetPosition (index), 0f, 0f);
            mn.notes = beat;
        }

        isTutorialPhase.RemoveAt (0);
        
        ++barCount;
    }

    private void Move ()
    {
        transform.position = new Vector3 (-met.note * avgNoteLength - 3f, 0f, 0f);
    }

    // inform this script that a beat was destroyed, so that it can create new bars
    public void BeatDestroyed (int len)
    {
        destroyedBeats += len;
        if (destroyedBeats >= 4)
        {
            destroyedBeats -= 4;
            --barCount;
            ++destroyedBars;
            sheet.RemoveAt (0);
        }
    }


    private int IndexOfQuarterBeat (IEnumerable<bool> beat)
    {
        if (beat.SequenceEqual (new List<bool> {true, false, false, true, false, false, true, false, false, true, false, false}))
            return 1; // sixteenth note
        else if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, true, false, false, true, false, false}))
            return 2; // 1 &a
        else if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, false, false, false, true, false, false})) 
            return 3; // 1 a
        else if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, true, false, false, false, false, false}))
            return 4; // 1 &
        else if (beat.SequenceEqual (new List<bool> {true, false, false, true, false, false, false, false, false, true, false, false}))
            return 5; // 1e a
        else if (beat.SequenceEqual (new List<bool> {true, false, false, true, false, false, false, false, false, false, false, false}))
            return 6; // 1e
        else if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, false, false, false, false, false, false}))
            return 12; // quarter note
        else if (beat.SequenceEqual (new List<bool> {false, false, false, false, false, false, false, false, false, false, false, false}))
            return 13; // quarter rest
        else if (beat.SequenceEqual (new List<bool> {false, false, false, false, false, false, true, false, false, false, false, false}))
            return 14; // eighth rest and note
        else if (beat.SequenceEqual (new List<bool> {false, false, false, false, false, false, false, false, false, true, false, false}))
            return 19; // a
        else if (beat.SequenceEqual (new List<bool> {false, false, false, true, false, false, false, false, false, false, false, false}))
            return 20; // e
        else if (beat.SequenceEqual (new List<bool> {false, false, false, true, false, false, true, false, false, false, false, false}))
            return 21; // e&
        else if (beat.SequenceEqual (new List<bool> {false, false, false, true, false, false, false, false, false, true, false, false}))
            return 22; // ea
        else if (beat.SequenceEqual (new List<bool> {false, false, false, false, false, false, true, false, false, true, false, false}))
            return 23; // &a
        else if (beat.SequenceEqual (new List<bool> {true, false, false, true, false, false, true, false, false, false, false, false}))
            return 24; // 1e&
        else if (beat.SequenceEqual (new List<bool> {false, false, false, true, false, false, true, false, false, true, false, false}))
            return 25; // e&a

        return 0;
    }

    private int IndexOfHalfBeat (IEnumerable<bool> beat)
    {
        if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, false, false, false, false, false, false,
                                                false, false, false, false, false, false, false, false, false, false, false, false}))
            return 7; // half note
        else if (beat.SequenceEqual (new List<bool> {false, false, false, false, false, false, false, false, false, false, false, false,
                                                     false, false, false, false, false, false, false, false, false, false, false, false}))
            return 8; // half rest
        else if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, false, false, false, false, false, false,
                                                     false, false, false, false, false, false, true, false, false, false, false, false}))
            return 15; // dotted quarter note and eighth note
        else if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, true, false, false, false, false, false,
                                                     true, false, false, false, false, false, true, false, false, false, false, false}))
            return 16; // 1 & 2 &
        else if (beat.SequenceEqual (new List<bool> {false, false, false, false, false, false, true, false, false, false, false, false,
                                                     true, false, false, false, false, false, true, false, false, false, false, false}))
            return 17; // & 2 &
        else if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, false, false, false, false, false, false,
                                                     false, false, false, false, false, false, true, false, false, true, false, false}))
            return 18; // 1 (2) &a
        
        return 0;
    }

    private int IndexOf3QuartersBeat (IEnumerable<bool> beat)
    {
        if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, false, false, false, false, false, false,
                                                false, false, false, false, false, false, false, false, false, false, false, false,
                                                false, false, false, false, false, false, false, false, false, false, false, false}))
            return 9; // dotted half note
        
        return 0;
    }

    private int IndexOfWholeBeat (IEnumerable<bool> beat)
    {
        if (beat.SequenceEqual (new List<bool> {true, false, false, false, false, false, false, false, false, false, false, false,
                                                false, false, false, false, false, false, false, false, false, false, false, false,
                                                false, false, false, false, false, false, false, false, false, false, false, false,
                                                false, false, false, false, false, false, false, false, false, false, false, false}))
            return 10; // whole note
        else if (beat.SequenceEqual (new List<bool> {false, false, false, false, false, false, false, false, false, false, false, false,
                                                     false, false, false, false, false, false, false, false, false, false, false, false,
                                                     false, false, false, false, false, false, false, false, false, false, false, false,
                                                     false, false, false, false, false, false, false, false, false, false, false, false}))
            return 11; // whole rest
        
        return 0;
    }

    private int BeatOffsetPosition (int beat)
    {
        if (beat >= 15 && beat <= 18)
            return 1;

        return 0;
    }



}
