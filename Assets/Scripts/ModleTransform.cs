using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ModleTransform : MonoBehaviour
{
    float speed = 2f;

    float initialDistance;

    Vector3 initialScale;
    Vector2 initialMove;


    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {


        if (Input.touchCount == 2)
        {
            var touchZero=Input.GetTouch(0);
            var touchOne=Input.GetTouch(1);

            if(touchZero.phase==TouchPhase.Ended||touchZero.phase==TouchPhase.Canceled
                ||touchOne.phase==TouchPhase.Ended||touchOne.phase==TouchPhase.Canceled) 
            {
                return;
            }

            if(touchZero.phase==TouchPhase.Began && touchOne.phase==TouchPhase.Began)
            {
                initialDistance=Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = gameObject.transform.localScale;
                Debug.Log("Intial Distance :" + initialDistance + "GameObject Name:" + gameObject.name);
            }
            else 
            {
                var CurrentDistance=Vector2.Distance(touchZero.position,touchOne.position);

                var factor = CurrentDistance / initialDistance;

                gameObject.transform.localScale = initialScale * factor;
            }
        }

        
    }

    void OnMouseDrag()
    {
        float xRotation = Input.GetAxis("Mouse X") * speed;
        float yRotation = Input.GetAxis("Mouse Y") * speed;

        transform.Rotate(Vector3.down*xRotation);
        transform.Rotate(Vector3.right*yRotation);
    }
}
