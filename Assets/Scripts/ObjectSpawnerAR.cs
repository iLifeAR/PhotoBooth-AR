using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]

public class ObjectSpawnerAR : MonoBehaviour
{
    ARRaycastManager raycastManager;
    public List<ARRaycastHit> _Hits;
    // Start is called before the first frame update
    void Start()
    {
        raycastManager= GetComponent<ARRaycastManager>();
    }


    public void trySpawnObject(Vector2 POS)
    {
        if (raycastManager.Raycast(POS, _Hits))
        {
            Debug.Log("RaycastHit");
            if (_Hits[0].trackable is ARPlane plane)
            {
                Debug.Log("Plane Hit");
            }
            else
            {
                Debug.Log("Hit:" + _Hits[0].hitType);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
