using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUIController : MonoBehaviour
{
    [Header("===== CANVAS ======")]

    [SerializeField] Canvas gameEndCanvas;

    [Header("===== BUTTONS ======")]
    [SerializeField] Button buttonReturnToMainMenu;
    [SerializeField] Button buttonQuit;

    [Header("===== TEXTS =====")]
    [SerializeField] private Text firstTitle;
    [SerializeField] private Text lastTitle;
    [SerializeField] private Image backGroundImage;
    [SerializeField] private Sprite congratulationImage;
    [SerializeField] private Sprite failImage;
    [SerializeField] private string congratulationText = "Congratulations!";
    [SerializeField] private string congratulationDetailText = "你还在坚持！";
    [SerializeField] private string failText = "Unfortunately:(";
    [SerializeField] private string failDetailText = "你未能挺过来";
    
    private void OnEnable()
    {
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonReturnToMainMenu.gameObject.name, OnButtonReturnToMainMenu);
        ButtonPressedBehaviour.buttonFunctionTable.Add(buttonQuit.gameObject.name, OnButtonQuitClicked);
    }

    private void OnDisable()
    {
        ButtonPressedBehaviour.buttonFunctionTable.Clear();
    }
    
    private void Start()
    {
        Time.timeScale = 1f;
        if (DataTransferer.Instance.mode == Mode.Dead)
        {
            backGroundImage.sprite = failImage;
            firstTitle.text = failText;
            firstTitle.color = Color.red;
            lastTitle.text = failDetailText;
            lastTitle.color = Color.red;
            lastTitle.fontStyle = FontStyle.Bold;
        }
        else if (DataTransferer.Instance.mode == Mode.Survive)
        {
            backGroundImage.sprite = congratulationImage;
            firstTitle.text = congratulationText;
            firstTitle.color = Color.magenta;
            lastTitle.text = congratulationDetailText;
            lastTitle.color = Color.magenta;
            lastTitle.fontStyle = FontStyle.Bold;
        }
        
        UIInput.Instance.SelectUI(buttonReturnToMainMenu);
    }
    
    private void OnButtonReturnToMainMenu()
    {
        gameEndCanvas.enabled = false;
        SceneLoader.Instance.LoadMainScene();
    }
    
    void OnButtonQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



}
