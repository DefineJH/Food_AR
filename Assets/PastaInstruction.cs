using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PastaInstruction : MonoBehaviour
{
    public GameObject m_MoveRightBtn;
    public GameObject m_MoveLeftBtn;
    public GameObject m_InstructionPanel;
    public GameObject m_Instruction;

    float m_MoveOffset = 0.0f;
    enum InfoPanelState
    {
        Moving,
        Left,
        Right,
    }
    InfoPanelState m_State;
    // Start is called before the first frame update
    void Start()
    {
        m_MoveRightBtn.SetActive(false);
        m_MoveOffset = m_Instruction.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        HandInfo curDetectHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        if(curDetectHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_RIGHT && m_State == InfoPanelState.Right)
        {
            MoveLeft();
        }
        else if(curDetectHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_LEFT && m_State == InfoPanelState.Left)
        {
            MoveRight();
        }
    }
    public void MoveRight()
    {
        StartCoroutine(MoveRight_Animation());
    }
    public void MoveLeft()
    {
        StartCoroutine(MoveLeft_Animation());
    }
    IEnumerator MoveLeft_Animation()
    {
        RectTransform rt = m_InstructionPanel.GetComponent<RectTransform>();
        float to = rt.offsetMax.x - m_MoveOffset;
        m_State = InfoPanelState.Moving;
        while(rt.offsetMax.x >= to)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x - 5, rt.offsetMax.y);
            rt.offsetMin = new Vector2(rt.offsetMin.x - 5, rt.offsetMin.y);
            yield return null;
        }
        m_MoveLeftBtn.SetActive(false);
        m_MoveRightBtn.SetActive(true);
        m_State = InfoPanelState.Left;
        yield break;
    }

    IEnumerator MoveRight_Animation()
    {
        RectTransform rt = m_InstructionPanel.GetComponent<RectTransform>();
        float to = rt.offsetMax.x + m_MoveOffset;
        m_State = InfoPanelState.Moving;
        while (rt.offsetMax.x <= to)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x + 5, rt.offsetMax.y);
            rt.offsetMin = new Vector2(rt.offsetMin.x + 5, rt.offsetMin.y);
            yield return null;
        }
        m_MoveLeftBtn.SetActive(true);
        m_MoveRightBtn.SetActive(false);
        m_State = InfoPanelState.Right;

        yield break;
    }
}
