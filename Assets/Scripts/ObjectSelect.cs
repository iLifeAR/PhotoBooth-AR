using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectSelect : MonoBehaviour
{
  
    [SerializeField]
    private GameObject[] targets;
   
    public void SelectObject(int i)
    {
        targets[i].SetActive(true);
    }
}
