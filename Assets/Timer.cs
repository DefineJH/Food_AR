using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text m_toolTipText;
    public Text m_remainTimeText;
    public Text m_maxTimeText;

    int m_remainTime;
    int m_maxTime;

    public void Initalize(string toolTip, int idx, int Minute, System.Action<int> onFinish)
    {
        m_maxTime = Minute * 60;
        m_toolTipText.text = toolTip;
        m_maxTimeText.text = (m_maxTime / 60).ToString() + "m" + (m_maxTime % 60).ToString() + "s";
        m_remainTime = m_maxTime;
        StartCoroutine(StartTimer(onFinish,idx));
    }
    
    IEnumerator StartTimer(System.Action<int> onFinish, int idx)
    {
        while(m_remainTime > 0)
        {
            yield return new WaitForSeconds(1);
            m_remainTime--;
            m_remainTimeText.color = new Color(1, 1 - (float)m_remainTime / m_maxTime, 1 - (float)m_remainTime / m_maxTime);
            m_remainTimeText.text = (m_remainTime / 60).ToString() + "m" + (m_remainTime % 60).ToString() + "s";
        }
        m_maxTimeText.text = "";
        m_remainTimeText.text = "";
        m_toolTipText.text = "";
        onFinish(idx);
        yield break;
    }
    
}
