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
    float m_InstructionUIMoveOffset = 0.0f;


    public GameObject m_MoveUpBtn;
    public GameObject m_MoveDownBtn;
    public GameObject m_TimerPanel;
    public GameObject m_TimerListPanel;
    float m_TimerUIMoveOffset = 0.0f;

    public Text m_MainInstruction;
    public Text m_CurrentProgress;
    public Text m_SubInstruction;

    int m_CurStage = 1;

    public GameObject m_TimerButton;
    /// <summary>
    /// false -> not using , true -> using
    /// </summary>
    public List<bool> m_TimerList;
    System.Action<int> onTimerFinished;

    int m_CurrentTimerTime;
    string m_TimerToolTip;

    enum InfoPanelState
    {
        Moving,
        Left,
        Right,
    }
    InfoPanelState m_InfoUIState;
    enum TimerPanelState
    {
        Moving,
        Up,
        Down,
    }
    TimerPanelState m_TimerUIState;


    // Start is called before the first frame update
    void Start()
    {
        m_TimerButton.SetActive(false);
        m_TimerList = new List<bool>() { false, false, false, false };

        m_MoveRightBtn.SetActive(false);
        m_InstructionUIMoveOffset = m_Instruction.GetComponent<RectTransform>().rect.width;

        m_MoveDownBtn.SetActive(false);
        m_TimerUIMoveOffset = m_TimerListPanel.GetComponent<RectTransform>().rect.height;

        onTimerFinished += OnTimerFinish;

        m_MainInstruction.text = "도마 인식";
        m_SubInstruction.text = "도마를 화면의 중앙에 놓고 인식시켜 주세요";
    }

    // Update is called once per frame
    void Update()
    {
        HandInfo curDetectHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        if(curDetectHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_RIGHT && m_InfoUIState == InfoPanelState.Right)
        {
            MoveLeft();
        }
        else if(curDetectHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_LEFT && m_InfoUIState == InfoPanelState.Left)
        {
            MoveRight();
        }

        if (curDetectHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_UP && m_TimerUIState == TimerPanelState.Down)
        {
            MoveUp();
        }
        else if (curDetectHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.SWIPE_DOWN && m_TimerUIState == TimerPanelState.Up)
        {
            MoveDown();
        }

        m_CurrentProgress.text = m_CurStage.ToString();
    }
    void CreateTimerButton(string toolTip, int Minute)
    {
        m_TimerButton.SetActive(true);
        m_CurrentTimerTime = Minute;
        m_TimerToolTip = toolTip;
    }
    public void ApplyTimer()
    {
        m_TimerButton.SetActive(false);
        for(int i = 0; i < m_TimerList.Count; i++)
        {
            if (m_TimerList[i] == false)
            {
                m_TimerList[i] = true;
                m_TimerListPanel.transform.GetChild(i).gameObject.GetComponent<Timer>().Initalize(m_TimerToolTip, i,m_CurrentTimerTime,onTimerFinished);
                break;
            }
        }
    }
    void OnTimerFinish(int idx)
    {
        m_TimerList[idx] = false;
        GameManager.Instance.CreateUIPopUp("타이머 종료!", "요리를 확인하세요");
    }
    public void Progress()
    {
        m_CurStage++;
        switch(m_CurStage)
        {
            case 2:
                m_MainInstruction.text = "물 끓이기";
                m_SubInstruction.text = "냄비에 물을 받고 끓여주세요.\n 약간의 소금과 오일을 첨가하세요.\n 물이 끓기 시작하면 면을 넣어주세요.\n 불을 키고 화면 중앙의 버튼을 눌러 타이머를 시작하세요 (15분) \n";
                CreateTimerButton("물 끓이기 + 면 삶기",15);
                break;
            case 3:
                m_MainInstruction.text = "토마토 손질";
                m_SubInstruction.text = "토마토를 도마위에 올린 후 \n 인식된 보조선에 맞게 잘라주세요";
                break;
            case 4:
                m_MainInstruction.text = "양파 손질";
                m_SubInstruction.text = "껍질을 벗긴 양파를 도마위에 올린 후 \n 인식된 보조선에 맞게 잘라주세요";
                break;
            case 5:
                m_MainInstruction.text = "재료 볶기";
                m_SubInstruction.text = "예쁘게 썰은 양파를 오일에 두른 팬에 볶아주세요.\n 불을 키고 화면 중앙의 버튼을 눌러 타이머를 시작하세요 (5분)";
                CreateTimerButton("팬 양파 볶기",5);
                break;
            case 6:
                m_MainInstruction.text = "재료 볶기";
                m_SubInstruction.text = "썰어놓은 토마토를 팬에 넣고 볶아주세요.\n 불을 키고 화면 중앙의 버튼을 눌러 타이머를 시작하세요 (5분)";
                CreateTimerButton("팬 토마토 볶기",5);
                break;
            case 7:
                m_MainInstruction.text = "재료 볶기";
                m_SubInstruction.text = "토마토 스파게티 소스를 넣고 볶아주세요.\n 불을 키고 화면 중앙의 버튼을 눌러 타이머를 시작하세요 (5분)";
                CreateTimerButton("팬 소스 볶기",5);
                break;
            case 9:
                m_MainInstruction.text = "재료 볶기";
                m_SubInstruction.text = "삶은 면을 팬에 넣고 소스와 버무려 볶아주세요. \n 불을 키고 화면 중앙의 버튼을 눌러 타이머를 시작하세요 (3분)";
                CreateTimerButton("팬 면 볶기",3);
                break;
            case 10:
                GameManager.Instance.CreateUIPopUp("요리 완료!", "맛있는 파스타를 즐기세요");
                break;              

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
        float to = rt.offsetMax.x - m_InstructionUIMoveOffset;
        m_InfoUIState = InfoPanelState.Moving;
        while(rt.offsetMax.x >= to)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x - 5, rt.offsetMax.y);
            rt.offsetMin = new Vector2(rt.offsetMin.x - 5, rt.offsetMin.y);
            yield return null;
        }
        m_MoveLeftBtn.SetActive(false);
        m_MoveRightBtn.SetActive(true);
        m_InfoUIState = InfoPanelState.Left;
        yield break;
    }

    IEnumerator MoveRight_Animation()
    {
        RectTransform rt = m_InstructionPanel.GetComponent<RectTransform>();
        float to = rt.offsetMax.x + m_InstructionUIMoveOffset;
        m_InfoUIState = InfoPanelState.Moving;
        while (rt.offsetMax.x <= to)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x + 5, rt.offsetMax.y);
            rt.offsetMin = new Vector2(rt.offsetMin.x + 5, rt.offsetMin.y);
            yield return null;
        }
        m_MoveLeftBtn.SetActive(true);
        m_MoveRightBtn.SetActive(false);
        m_InfoUIState = InfoPanelState.Right;

        yield break;
    }

    public void MoveUp()
    {
        StartCoroutine(MoveUp_Animation());
    }
    public void MoveDown()
    {
        StartCoroutine(MoveDown_Animation());
    }
    IEnumerator MoveUp_Animation()
    {
        RectTransform rt = m_TimerPanel.GetComponent<RectTransform>();
        float to = rt.offsetMax.y + m_TimerUIMoveOffset;
        m_TimerUIState = TimerPanelState.Moving;
        while (rt.offsetMax.y <= to)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+3);
            rt.offsetMin = new Vector2(rt.offsetMin.x, rt.offsetMin.y+3);
            yield return null;
        }
        m_MoveUpBtn.SetActive(false);
        m_MoveDownBtn.SetActive(true);
        m_TimerUIState = TimerPanelState.Up;
        yield break;
    }

    IEnumerator MoveDown_Animation()
    {
        RectTransform rt = m_TimerPanel.GetComponent<RectTransform>();
        float to = rt.offsetMax.y - m_TimerUIMoveOffset;
        m_TimerUIState = TimerPanelState.Moving;
        while (rt.offsetMax.y >= to)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y - 3);
            rt.offsetMin = new Vector2(rt.offsetMin.x, rt.offsetMin.y - 3);
            yield return null;
        }
        m_MoveUpBtn.SetActive(true);
        m_MoveDownBtn.SetActive(false);
        m_TimerUIState = TimerPanelState.Down;
        yield break;
    }
}
