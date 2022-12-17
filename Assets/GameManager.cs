using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public Canvas m_Canvas;
    public GameObject m_RecipeChoosePanel;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
    }
    public void Initalize()
    {
        if (m_Canvas == null || m_RecipeChoosePanel == null)
            return;
        Instantiate(m_RecipeChoosePanel,m_Canvas.transform);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
