using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    public Vector3 CameraRotation;
    public RectTransform BoothMenu;
    public RectTransform AugmentUIGroup;


    bool istracked;

    // Start is called before the first frame update
    async void Start()
    {
        foreach (ModelPair item in Models)
        {
            item._Toggle.onValueChanged.AddListener(delegate { ToggleActivation(item._Toggle, item.Model); });
        }   


        await Task.Delay(3000);
        Camera.main.transform.localEulerAngles= CameraRotation;
    }


    

    public void ToggleActivation(Toggle T,GameObject G)
    {
        if (istracked) return;


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

    public void OnTrack()
    {
        foreach (ModelPair item in Models)
        {
            if (item._Toggle.isOn)
            {
                item.Model.GetComponent<AudioSource>().Play();
                break;
            }
        }
    }

    public void OnLost()
    {
        foreach (ModelPair item in Models)
        {
            item.Model.GetComponent<AudioSource>().Stop();
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

        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "Photobooth Captures",System.DateTime.Now.ToShortDateString()+  ".png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
        Debug.Log("Permission result: " + permission);

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetText("Share Picture").SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }

    public void ShowBoothMenu()
    {
        BoothMenu.DOAnchorPosY(85,0.5f);
        AugmentUIGroup.gameObject.SetActive(false);
    }


    // Update is called o
    // nce per frame
    void Update()
    {
        Camera.main.transform.localEulerAngles= CameraRotation;

    }
}
