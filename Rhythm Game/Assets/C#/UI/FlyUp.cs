/*
    The FlyUp script is attached to any UI elements that need to fly offscreen when the game begins, and then reappear again when it stops.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUp : MonoBehaviour
{
    [SerializeField] float delay = 0f;
    [SerializeField] float targetY1 = 0f;
    [SerializeField] float targetY2 = 0f;
    float oldTarget;

    void Start ()
    {
        oldTarget = transform.position.y;
    }
    
    public void Fly (bool up)
    {
        if (!up)
        {
            StartCoroutine ("CoroutineDown");
            print ("down " + gameObject.name); //debug
        }
        else
        {
            StartCoroutine ("CoroutineUp");
            print ("up " + gameObject.name); //debug
        }
    }

    IEnumerator CoroutineUp ()
    {
        yield return new WaitForSeconds (delay);
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 oldPos = transform.position;
        while (duration > elapsed)
        {
            elapsed += Time.deltaTime;
            float step = Mathf.SmoothStep (0f, 1f, elapsed / duration);
            transform.position = new Vector3 (oldPos.x, Mathf.Lerp (oldPos.y, targetY1, step), oldPos.z);
            yield return null;
        }
        duration = 0.5f;
        elapsed = 0f;
        oldPos = transform.position;
        while (duration > elapsed)
        {
            elapsed += Time.deltaTime;
            float step = Mathf.SmoothStep (0f, 1f, elapsed / duration);
            transform.position = new Vector3 (oldPos.x, Mathf.Lerp (oldPos.y, targetY2, step), oldPos.z);
            yield return null;
        }
    }

    IEnumerator CoroutineDown ()
    {
        yield return new WaitForSeconds (delay);
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 oldPos = transform.position;
        while (duration > elapsed)
        {
            elapsed += Time.deltaTime;
            float step = Mathf.SmoothStep (0f, 1f, elapsed / duration);
            transform.position = new Vector3 (oldPos.x, Mathf.Lerp (oldPos.y, targetY1, step), oldPos.z);
            yield return null;
        }
        duration = 0.5f;
        elapsed = 0f;
        oldPos = transform.position;
        while (duration > elapsed)
        {
            elapsed += Time.deltaTime;
            float step = Mathf.SmoothStep (0f, 1f, elapsed / duration);
            transform.position = new Vector3 (oldPos.x, Mathf.Lerp (oldPos.y, oldTarget, step), oldPos.z);
            yield return null;
        }
    }
}
