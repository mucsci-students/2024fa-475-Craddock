using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float offset = 0f; // 0.3 is good for laptop
    [SerializeField] Metronome met;
    [SerializeField] List<FlyUp> flyUps;
    [SerializeField] SongPlayer player;
    [SerializeField] SheetMusicController controller;
    [SerializeField] Reset reset;

    public List<MusicNote> notes;
    int currIndex {
        get { return (int) (met.note + offset + 1f / 12f); }
    }
    bool began = false;


    public void Begin ()
    {
        began = true;
    }

    public void Stop ()
    {
        began = false;
        player.Stop ();
        controller.Stop ();
        met.Stop ();
        reset.HideIfNoScore ();
        foreach (FlyUp f in flyUps)
            f.Fly (false);
        notes = new List<MusicNote> ();
    }

    void Update()
    {
        if (!began)
            return;

        bool screenTap = false;
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began && t.position.y < Screen.height * 0.85f && t.position.y > Screen.height * 0.055f)
            {
                screenTap = true;
                break;
            }
            else if ((t.phase == TouchPhase.Began) && t.position.y < Screen.height * 0.055f)
            {
                Stop ();
            }
        }
        if (Input.GetMouseButtonDown (0) && Input.mousePosition.y < Screen.height * 0.055f)
            Stop ();
        if (currIndex >= 0 && (Input.GetKeyDown (KeyCode.Space) || screenTap))
        {
            notes[currIndex].Hit (met.note + offset);
        }
    }
}
