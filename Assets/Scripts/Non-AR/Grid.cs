using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] float rotationSensitivity;
    [SerializeField] Vector2 ScaleBounds;

    [SerializeField] bool canEdit;
    GameObject SpawnedObject;
    [SerializeField] GameObject[] Prefabs;

    SpriteRenderer spriteRenderer;
    public int spawnIndex;

    Vector2 TouchStartPOS;
    Vector2 T1 = new Vector2();
    Vector2 T2 = new Vector2();
    float StartScale = 0;


    // Start is called before the first frame update
    void Start()
    {
        canEdit = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleEdit(bool Toggle)
    {
        canEdit = Toggle;
    }
    #region Spawn And Translation
    GameObject TrySpawn(Vector2 ScreenPOS)
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenPOS);
        RaycastHit hit;
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Grid")) && !isOverUI)
        {
            GameObject Object = Instantiate(Prefabs[spawnIndex], hit.point, quaternion.identity);
            Object.transform.up = hit.normal;
            Object.transform.parent = transform;
            return Object;

        }
        return null;

    }

    void TryGetChangePosition(Vector2 ScreenPOS)
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenPOS);
        RaycastHit hit;
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Grid")) && !isOverUI)
        {
            SpawnedObject.transform.position = hit.point;
            SpawnedObject.transform.up = hit.normal;
        }
    }
    #endregion

    float GetScaleAmount(Vector2 StartPOS, Vector2 EndPOS)
    {
        float ScaleAmount = 0;
        float DIstance = Vector2.Distance(EndPOS, StartPOS);
        float Percent = (DIstance * 100) / Screen.width;
        ScaleAmount = ((ScaleBounds.y - ScaleBounds.x) * Percent) / 100;

        return ScaleAmount;
    }

    public void ToggleGridVisibility(Boolean SHOW)
    {
        spriteRenderer.enabled= SHOW;
    }

    public void ChangeSpawnIndex(int Index)
    {
        if (SpawnedObject)
        {
            Destroy(SpawnedObject);
        }
        spawnIndex = Index;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TouchStartPOS = touch.position;

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (canEdit)
                {
                    transform.Rotate(touch.deltaPosition.y * 0.25f * rotationSensitivity, 0f, -touch.deltaPosition.x * 0.25f * rotationSensitivity, Space.World);
                }
                else if (SpawnedObject)
                {
                    SpawnedObject.transform.Rotate(0, -touch.deltaPosition.x * 0.25f * rotationSensitivity, 0f, Space.Self);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (canEdit) return;
                Debug.Log(Vector2.Distance(TouchStartPOS, touch.position));
                if (Vector2.Distance(TouchStartPOS, touch.position) < 10)
                {
                    if (!SpawnedObject)
                        SpawnedObject = TrySpawn(touch.position);
                    else
                        TryGetChangePosition(touch.position);
                }
            }

        }
        else if (Input.touchCount == 2 && SpawnedObject)
        {
            Touch _Touch1 = Input.GetTouch(0);
            Touch _Touch2 = Input.GetTouch(1);

            if (_Touch1.phase == TouchPhase.Began || _Touch2.phase == TouchPhase.Began)
            {
                T1 = _Touch1.position;
                T2 = _Touch2.position;
                StartScale = SpawnedObject.transform.localScale.x;
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
                SpawnedObject.transform.localScale = ToScale * Vector3.one;
            }

        }

    }
}
