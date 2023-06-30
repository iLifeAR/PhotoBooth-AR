using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ModelSwitcher : MonoBehaviour
{
    [System.Serializable]
    public struct ModelPair
    {
        public Toggle _Toggle;
        public GameObject Model;
    }
    public ModelPair[] Models;

    // Start is called before the first frame update
    void Start()
    {
        foreach (ModelPair item in Models)
        {
            item._Toggle.onValueChanged.AddListener(delegate { ToggleActivation(item._Toggle, item.Model); });
        }   
    }
    

    public void ToggleActivation(Toggle T,GameObject G)
    {
        foreach (ModelPair item in Models)
        {
            if (G == item.Model)
            {
                G.GetComponent<AudioSource>().Play();
                G.SetActive(true);
            }
            else
            {
                item.Model.SetActive(false);
                item.Model.GetComponent<AudioSource>().Stop();

            }

        }
    }


    public void CaptureShare()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetText("Share Picture").SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }


    // Update is called o
    // nce per frame
    void Update()
    {
        
    }
}
