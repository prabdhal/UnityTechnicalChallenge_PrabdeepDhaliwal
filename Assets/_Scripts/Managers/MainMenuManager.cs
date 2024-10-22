using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button quitButton;

    private Canvas mainMenuCanvas;
    #endregion

    #region Init
    private void Start()
    {
        mainMenuCanvas = GetComponentInParent<Canvas>();
        playButton.onClick.AddListener(OnPlay);
        playButton.onClick.AddListener(GameManager.Instance.SpawnPlayer);
        quitButton.onClick.AddListener(OnQuit);
    }
    #endregion

    #region Button Events
    private void OnPlay()
    {
        mainMenuCanvas.gameObject.SetActive(false);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
    #endregion
}
