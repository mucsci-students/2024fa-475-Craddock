using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    public IEnumerable<bool> notes;
    public Metronome met;
    public int beatNum;
    public List<float> beatTimes = new List<float> ();
    List<float> audioBeatTimes;
    public bool tutorialNote = false;
    int currTutorialNote = 0;

    float timeOfLastHit = float.NegativeInfinity;
    float scoreOfLastHit = 1f;
    [SerializeField] SpriteRenderer rend;

    [SerializeField] Color beatColor;
    [SerializeField] Color hitColor;
    [SerializeField] Color missColor;
    [SerializeField] Color tutorialHitColor;
    [SerializeField] Color tutorialColor;
    [SerializeField] AudioSource src;
    [SerializeField] ParticleSystem hitSpray;

    [SerializeField] List<AudioClip> hiBongos;
    [SerializeField] List<AudioClip> loBongos;
    [SerializeField] List<AudioClip> thumpBongos;

    bool coroutineRunning = false;

    void Start ()
    {
        gameObject.name = "note: " + beatNum + (tutorialNote ? " tutorial" : "");
        int i = 0;
        foreach (bool n in notes)
        {
            if (n)
            {
                beatTimes.Add (beatNum + i / 12f);
            }
            ++i;
        }
        audioBeatTimes = new List<float> (beatTimes);
    }

    void Update ()
    {
        // tutorial note hit
        if (tutorialNote)
        {
            if (beatTimes.Count > currTutorialNote && met.note > beatTimes[currTutorialNote])
            {
                TutorialHit ();
                ++currTutorialNote;
            }
        }

        // adjust color
        Color b = new Color (0f, 0f, 0f);
        foreach (float t in beatTimes)
        {
            b += beatColor / Mathf.Pow (7.5f * met.tempo, Mathf.Abs (met.note - t));
            if (audioBeatTimes.Any () && audioBeatTimes[0] < met.note)
            {
                src.Play ();
                audioBeatTimes.RemoveAt (0);
            }
        }

        Color h = new Color (0f, 0f, 0f);
        float fadeAmt = 1f - (met.note - timeOfLastHit) / 0.5f;
        
        if (fadeAmt > 0)
            h = ((tutorialNote ? tutorialHitColor : hitColor) * (scoreOfLastHit + 1f) / 2f + missColor * (1f - scoreOfLastHit) / 2f) * fadeAmt;
        rend.color = (tutorialNote ? tutorialColor : Color.black) + 2 * b / (beatTimes.Count + 1) / (tutorialNote ? 1f : 1f) + h;

    }

    public void Hit (float offsetTime)
    {
        if (tutorialNote)
            return;

        timeOfLastHit = met.note;
        scoreOfLastHit = Mathf.Clamp (1.1f - 12f * MinDistToBeat (offsetTime), -1f, 1f);
        met.score += scoreOfLastHit;
        if (met.score < 0f)
            met.score = 0f;
        if(scoreOfLastHit == 1)
        {
            hitSpray.Play ();
        }
        if (scoreOfLastHit < 0)
        {
            if (!coroutineRunning)
                StartCoroutine (Rattle (-scoreOfLastHit));
            src.PlayOneShot (thumpBongos[Random.Range (0, thumpBongos.Count)]);
        }
        else
        {
            if (met.note - (int) met.note < 1f / 12f)
                src.PlayOneShot (hiBongos[Random.Range (0, hiBongos.Count)]);
            else
                src.PlayOneShot (loBongos[Random.Range (0, loBongos.Count)]);
        }
    }

    float MinDistToBeat (float hit)
    {
        float min = float.PositiveInfinity;
        foreach (float t in beatTimes)
        {
            float dist = Mathf.Abs (t - hit);
            if (dist < min)
                min = dist;
            else
                break;
        }
        return min;
    }

    public void TutorialHit ()
    {
        timeOfLastHit = met.note;
        //scoreOfLastHit = 1;
        if (met.note - (int) met.note < 1f / 12f)
            src.PlayOneShot (hiBongos[Random.Range (0, hiBongos.Count)]);
        else
            src.PlayOneShot (loBongos[Random.Range (0, loBongos.Count)]);
    }

    IEnumerator Rattle (float dist)
    {
        coroutineRunning = true;
        Vector3 originalPos = transform.localPosition;
        for (int i = 0; i < 5; ++i)
        {
            transform.localPosition = originalPos + new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f)) * dist / 20f;
            yield return new WaitForSeconds (0.03f);
        }
        transform.localPosition = originalPos;
        coroutineRunning = false;
    }


}
