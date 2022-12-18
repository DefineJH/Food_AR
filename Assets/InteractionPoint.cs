using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class InteractionPoint : MonoBehaviour
{

    RectTransform m_RectTransform;
    Image m_Image;
    CircleCollider2D m_CircleCollider;
    Canvas m_Canvas;
    RectTransform m_CanvasRect;

    public Camera cam;

    float m_curUISelectTime;
    bool m_IsSelecting;
    Collider2D m_SelectingCollider;
    public Image m_SelectImage;
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Image = GetComponent<Image>();
        m_CircleCollider = GetComponent<CircleCollider2D>();
        m_Canvas = GetComponentInParent<Canvas>();
        if (m_Canvas != null)
            m_CanvasRect = m_Canvas.gameObject.GetComponent<RectTransform>();

        m_curUISelectTime = 0f;
    }
    void Update()
    {
        if(ARSession.state == ARSessionState.SessionTracking)
        {
            FollowIndexFingertip();
        }
            if(m_IsSelecting)
            {
                m_SelectImage.fillAmount += Time.deltaTime;
                if(m_SelectImage.fillAmount >= 1.0f)
                {
                    GameManager.Instance.SelectUI(m_SelectingCollider.gameObject);
                    RefreshPointer();
                }
            }
            else
            {
                m_SelectImage.fillAmount = 0;
            }
    }

    void RefreshPointer()
    {
        m_SelectingCollider = null;
        m_IsSelecting = false;
        m_curUISelectTime = 0;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        m_Image.color = Color.red;
        m_IsSelecting = true;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(m_SelectingCollider != other)
        {
            m_SelectingCollider = other;
            m_curUISelectTime = 0;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        m_Image.color = Color.white;
        m_IsSelecting = false;
        m_curUISelectTime = 0;
    }
    void FollowIndexFingertip()
    {
        HandInfo curDetectHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        ManoClass curDetectClass = curDetectHand.gesture_info.mano_class;

        Vector3 indexFingerTip = curDetectHand.tracking_info.skeleton.joints[8];

        Vector2 ViewportPosition = cam.WorldToViewportPoint(ManoUtils.Instance.CalculateNewPosition(indexFingerTip, curDetectHand.tracking_info.depth_estimation));
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * m_CanvasRect.sizeDelta.x) - (m_CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * m_CanvasRect.sizeDelta.y) - (m_CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        m_RectTransform.anchoredPosition = WorldObject_ScreenPosition;
    }
}
