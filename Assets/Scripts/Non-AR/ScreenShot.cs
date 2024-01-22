using DG.Tweening;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScreenShot : MonoBehaviour
{
    [SerializeField] RectTransform PanelUI;
    [SerializeField] Grid Grid;
    Button BT;

    // Start is called before the first frame update
    void Start()
    {
        BT = GetComponent<Button>();
    }

    IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");

        BT.enabled = false;
        PanelUI.gameObject.SetActive(false);
        Grid.ToggleGridVisibility(false);
        ScreenCapture.CaptureScreenshot(filePath, 2);
        string GallaryPath = string.Empty;
        BT.enabled = true;
        PanelUI.gameObject.SetActive(true);
        Grid.ToggleGridVisibility(true);
        Debug.Log(filePath);
        new NativeShare().AddFile(filePath)
            .SetSubject("Subject goes here").SetText("Hello world!").SetUrl("https://github.com/yasirkula/UnityNativeShare")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        byte[] FileBytes = File.ReadAllBytes(filePath);
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(FileBytes, "Photobooth", System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png", (success, path) => GallaryPath = path);
        if (string.IsNullOrEmpty(GallaryPath))
        {
            Debug.LogError("Not Saved");
        }

    }



    public void ShowButton()
    {
        transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutElastic).SetDelay(0.5f);
    }

    public void Capture()
    {
        StartCoroutine(TakeScreenShot());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
