using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMaster : MonoBehaviour
{

    [Header("Doodle")]
    public Toggle DoodleToggle;
    public Button DoodlePanelButton;
    Coroutine DoodlePanelCloseRoutine;


    [Header("OfflineUI")]
    public Toggle SelectionPanelToggle;
    public RectTransform SelectionPanel;

    [Space(15), Header("Audio")]
    public Toggle AudioToggle;
    public Button AudioPanelButton;
    public Button LanguageSelectionToggle;

    [System.Serializable]
    public struct AudioIcons
    {
        public Sprite MuteIcon;
        public Sprite PauseIcon;
    }
    public AudioIcons _AudioIconSprites;

    public enum AudioOptions
    {
        None, Mute, Pause
    }
    public AudioOptions Type;
    public AudioSource _BGM;
    public AudioSource _IntroVO;



    Coroutine AudioPanelCloseRoutine;


    // Start is called before the first frame update
    void Start()
    {
        if (Type == AudioOptions.None)
        {
            Debug.LogError("Select Audio Type");
        }


        SelectionPanelToggle.onValueChanged.AddListener(delegate { ToggleOfflinePanel();});
    }

    public void ToggleOfflinePanel()
    {
        if (SelectionPanelToggle.isOn)
        {
            SelectionPanel.DOAnchorPosX(135, 0.75f).SetEase(Ease.OutBack);
        }
        else
        {
            SelectionPanel.DOAnchorPosX(-155, 0.75f).SetEase(Ease.InBack);
        }
    }

    public void CloseApplication()
    {
        Application.Quit();
        Debug.Log("Applicaton Closed");
    }
}

