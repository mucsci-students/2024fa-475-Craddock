using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetMusicChild : MonoBehaviour
{
    float deleteSpot = -20f;
    public int len;
    public SheetMusicController controller; // null if this is something other than a music note

    void Update()
    {
        if (transform.position.x < deleteSpot || transform.position.y > 9f)
            RemoveThyself ();
    }

    private void RemoveThyself ()
    {
        if (controller)
            controller.BeatDestroyed (len);
            
        Destroy (gameObject);
    }
}
