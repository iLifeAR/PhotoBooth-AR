using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]

public class ObjectSpawnerAR : MonoBehaviour
{
    ARRaycastManager raycastManager;
    public List<ARRaycastHit> _Hits=new List<ARRaycastHit>();
    public PlayerInput input;
    public GameObject[] Prefabs;
    [SerializeField] int SpawnIndex=0;
    [SerializeField] GameObject SpawnedModel;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }


    public void trySpawnObject(InputAction.CallbackContext context)
    {
        Debug.Log("Plane");
        if (raycastManager.Raycast(context.ReadValue<Vector2>(), _Hits) && !SpawnedModel)
        {
            if (_Hits[0].trackable is ARPlane plane)
            {
                Debug.Log("Plane Hit");
                Vector3 SpawnPosition = _Hits[0].pose.position;
                SpawnedModel = Instantiate(Prefabs[SpawnIndex], SpawnPosition, Quaternion.identity);
                SpawnedModel.transform.LookAt(Camera.main.transform.position);
            }
            else
            {
                Debug.Log("Hit:" + _Hits[0].hitType);
            }
        }
    }

    public void ChangeSpawnIndex(int NewIndex)
    {
        SpawnIndex = NewIndex;
        if (SpawnedModel) Destroy(SpawnedModel);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
