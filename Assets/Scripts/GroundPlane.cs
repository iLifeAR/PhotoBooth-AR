using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlane : MonoBehaviour
{
    public float XDIR;
    public float YDIR;

    bool IsAugmenting;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void Augment()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        IsAugmenting= true;
    }

    public void ResetGround()
    {
        if (IsAugmenting) return;

        transform.localEulerAngles= Vector3.zero;

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && !IsAugmenting)
        {
            Touch screenTouch = Input.GetTouch(0);

            if (screenTouch.phase == TouchPhase.Moved)
            {
                transform.Rotate(screenTouch.deltaPosition.y * 0.25f * XDIR,  0f,-screenTouch.deltaPosition.x * 0.25f * YDIR, Space.World);
            }

            if (screenTouch.phase == TouchPhase.Ended)
            {
                //isActive = false;
            }
        }
    }
}
