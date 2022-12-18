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
    public GameObject m_RecipeChoosePanel_Inst;
    public GameObject m_PastaCanvas;
    public GameObject m_PastaCanvas_Inst;
    bool initialized;


    public enum Recipe
    {
        Curry,
        Pasta
    }

    Recipe m_SelectedRecipe;
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
        m_RecipeChoosePanel_Inst = Instantiate(m_RecipeChoosePanel,m_Canvas.transform);
    }

    public void SelectUI(GameObject obj)
    {
        Debug.Log(obj.name);
        if(obj.name.Contains("Recipe"))
        {
            StartCooking(obj.name.Split('_').GetValue(1) as string);
        }
        if(obj.name == "MoveRight")
        {
            if(m_SelectedRecipe == Recipe.Pasta)
            {
                m_PastaCanvas_Inst.GetComponent<PastaInstruction>().MoveRight();
            }
            else
            {

            }
        }
        if (obj.name == "MoveLeft")
        {
            if (m_SelectedRecipe == Recipe.Pasta)
            {
                m_PastaCanvas_Inst.GetComponent<PastaInstruction>().MoveLeft();
            }
            else
            {

            }
        }
    }

    void StartCooking(string recipe)
    {
        Debug.Log(recipe);

        m_RecipeChoosePanel_Inst.SetActive(false);
        if (recipe == "Pasta")
        {
            m_SelectedRecipe = Recipe.Pasta;
            m_PastaCanvas_Inst = Instantiate(m_PastaCanvas, m_Canvas.transform);
        }
        else
        {
            m_SelectedRecipe = Recipe.Curry;
        }
    }   
    // Update is called once per frame
    void Update()
    {
    }
}
