using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlane : MonoBehaviour
{
    public float XDIR;
    public float YDIR;
    public Vector2 ScaleBounds;

    public ModelSwitcher _ModelSwitcher;

    bool IsAugmenting;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void Augment()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        IsAugmenting= true;
        _ModelSwitcher.ShowBoothMenu();
        StartCoroutine(Scale());

    }

    public void ResetGround()
    {
        if (IsAugmenting) return;

        transform.localEulerAngles= Vector3.zero;

    }

    IEnumerator Scale()
    {
        Vector2 T1 = new Vector2();
        Vector2 T2 = new Vector2();
        float StartScale = 0;


        while (true)
        {
            if (IsAugmenting)
            {

                if (Input.touchCount == 2)
                {
                    Touch _Touch1 = Input.GetTouch(0);
                    Touch _Touch2 = Input.GetTouch(1);

                    if (_Touch1.phase == TouchPhase.Began || _Touch2.phase == TouchPhase.Began)
                    {
                        T1 = _Touch1.position;
                        T2 = _Touch2.position;
                        StartScale = transform.localScale.x;
                    }
                    else if (_Touch1.phase == TouchPhase.Moved || _Touch2.phase == TouchPhase.Moved)
                    {
                        float ScaleDIR = 0;
                        if (Vector2.Distance(T1, T2) < Vector2.Distance(_Touch1.position, _Touch2.position))
                        {
                            ScaleDIR = 1;
                        }
                        else
                        {
                            ScaleDIR = -1;

                        }

                        float ToScale = 0;
                        if (GetScaleAmount(T1, _Touch1.position) > GetScaleAmount(T2, _Touch2.position))
                        {
                            ToScale = GetScaleAmount(T1, _Touch1.position) * ScaleDIR;
                            ToScale += StartScale;
                        }
                        else if (GetScaleAmount(T1, _Touch1.position) < GetScaleAmount(T2, _Touch2.position))
                        {
                            ToScale = GetScaleAmount(T1, _Touch1.position) * ScaleDIR;
                            ToScale += StartScale;
                        }
                        ToScale = Mathf.Clamp(ToScale, ScaleBounds.x, ScaleBounds.y);
                        transform.localScale = ToScale * Vector3.one;
                    }


                }
            }

            yield return new WaitForEndOfFrame();
        }

    }
    float GetScaleAmount(Vector2 StartPOS, Vector2 EndPOS)
    {
        float ScaleAmount = 0;
        float DIstance = Vector2.Distance(EndPOS, StartPOS);
        float Percent = (DIstance * 100) / Screen.width;
        ScaleAmount = ((ScaleBounds.y - ScaleBounds.x) * Percent) / 100;

        return ScaleAmount;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && !IsAugmenting)
        {
            Touch screenTouch = Input.GetTouch(0);

            if (screenTouch.phase == TouchPhase.Moved)
            {
                transform.Rotate(screenTouch.deltaPosition.y * 0.25f * XDIR,  0f,-screenTouch.deltaPosition.x * 0.25f * YDIR, Space.World);
            }

            if (screenTouch.phase == TouchPhase.Ended)
            {
                //isActive = false;
            }
        }
    }
}
