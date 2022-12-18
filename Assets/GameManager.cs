using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public Canvas m_Canvas;
    public GameObject m_RecipeChoosePanel;

    bool initialized;
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
    void OnEnable()
    {
        ARSession.stateChanged += ArSessionStateChanged;
    }

    void OnDisable()
    {
        ARSession.stateChanged -= ArSessionStateChanged;
    }
    void ArSessionStateChanged(ARSessionStateChangedEventArgs eventArgs)
    {
        Debug.Log($"AR session state changed to {eventArgs.state}");

        if (!initialized && eventArgs.state == ARSessionState.SessionTracking)
        {
            initialized = true;
            StartCoroutine(OpenRecipeSelectUI());
        }
    }
    void Start()
    {
        
    }
    IEnumerator OpenRecipeSelectUI()
    {
        if (m_Canvas == null || m_RecipeChoosePanel == null)
            yield break;
        Instantiate(m_RecipeChoosePanel,m_Canvas.transform);
    }

    public void SelectUI(GameObject obj)
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
}
