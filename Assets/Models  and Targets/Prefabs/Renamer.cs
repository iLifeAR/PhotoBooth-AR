using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class Renamer : MonoBehaviour
{

    [SerializeField] string Input;

    // Start is called before the first frame update
    private void OnDisable()
    {
        string Name=gameObject.name;
        Name=Name.Replace(Input,"");
        gameObject.name = Name;
    }
}
