using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TapRegister : MonoBehaviour
{
    public InputActionAsset _InputMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        Debug.Log("Tapped:" +context.ReadValue<Vector2>());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
