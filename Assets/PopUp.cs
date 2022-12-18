using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUp : MonoBehaviour
{
    public Text m_main;
    public Text m_description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(string main, string desc)
    {
        m_main.text = main;
        m_description.text = desc;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
