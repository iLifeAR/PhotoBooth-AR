using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMaster : MonoBehaviour
{

    [Header("OfflineUI")]
    public Toggle SelectionPanelToggle;
    public RectTransform SelectionPanel;
    public RectTransform OptionsParent;

    public Grid spawner;

    // Start is called before the first frame update
    void Start()
    {
        //SelectionPanelToggle.onValueChanged.AddListener(ToggleOfflinePanel);
    }

    public void ToggleOfflinePanel(Boolean SHOW)
    {
        if (SHOW)
        {
            SelectionPanel.DOAnchorPosY(170, 0.5f);
        }
        else
        {
            SelectionPanel.DOAnchorPosY(-170, 0.5f);
        }
    }

    public void ResetObjectToSpawn()
    {
        foreach (Transform item in OptionsParent)
        {
            if(item.TryGetComponent(out Toggle t))
            {
                if (t.isOn)
                {
                    spawner.ChangeSpawnIndex(item.GetSiblingIndex());
                    break;
                }
            }
        }
    }

    public void CloseApplication()
    {
        Application.Quit();
        Debug.Log("Applicaton Closed");
    }
}

