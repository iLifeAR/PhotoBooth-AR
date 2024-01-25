using DG.Tweening;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScreenShot : MonoBehaviour
{
    [SerializeField] RectTransform PanelUI;
    [SerializeField] Grid Grid;
    Image BT;

    // Start is called before the first frame update
    void Start()
    {
        BT = GetComponent<Image>();
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        BT.enabled = false;
        PanelUI.gameObject.SetActive(false);
        Grid.ToggleGridVisibility(false);

        yield return new WaitForSeconds(0.25f);
        string Filename = System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png";

        ScreenCapture.CaptureScreenshot(Filename, 2);
        string filePath = Path.Combine(Application.persistentDataPath, Filename);
        while (!File.Exists(filePath))
        {
            yield return null;

        }


        BT.enabled = true;
        PanelUI.gameObject.SetActive(true);
        Grid.ToggleGridVisibility(true);

        string GallaryPath=string.Empty;
        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(File.ReadAllBytes(filePath), "Photobooth", System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png", (success, path) => GallaryPath=path);

        if (string.IsNullOrEmpty(GallaryPath))
        {
            Debug.LogError("Not Saved");
        }
        else
        {
            Debug.Log("Saved at Gallary");

        }


        new NativeShare().AddFile(GallaryPath)
    .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
    .Share();

        // To avoid memory leaks
    }



    public void ShowButton()
    {
        transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutElastic).SetDelay(0.5f);
    }

    public void Capture()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
