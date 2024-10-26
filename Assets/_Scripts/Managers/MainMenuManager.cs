using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    #region Singleton
    public static MainMenuManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    #endregion

    #region Fields
    [Header("Main Menu UI")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button controlsButton;
    [SerializeField]
    private Button quitButton;

    [Header("Controls Menu UI")]
    [SerializeField]
    private GameObject controlsMenu;
    [SerializeField]
    private Button backButton;

    private PlayerInputManager playerInputManager;
    #endregion

    #region Init & Update
    private void Start()
    {
        playButton.onClick.AddListener(OnPlay);
        controlsButton.onClick.AddListener(OnControls);
        quitButton.onClick.AddListener(OnQuit);

        backButton.onClick.AddListener(OnReturnToMainMenu);

        HideAll();
        ToggleMainMenu(true);
    }
    #endregion

    #region Button Events
    private void OnPlay()
    {
        HideAll();

        // Load game scene
        SceneManager.LoadScene(StringData.GameScene);
    }
    private void OnControls()
    {
        HideAll();
        ToggleControlsMenu(true);
    }
    private void OnReturnToMainMenu()
    {
        HideAll();
        ToggleMainMenu(true);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
    #endregion

    #region UI Actions
    public void ToggleMainMenu(bool display)
    {
        mainMenu.SetActive(display);

        if (display)
            HighlightElement(playButton.gameObject);
    }
    public void ToggleControlsMenu(bool display)
    {
        controlsMenu.SetActive(display);

        if (display)
            HighlightElement(backButton.gameObject);
    }
    private void HighlightElement(GameObject go)
    {
        EventSystem.current.firstSelectedGameObject = go;
        EventSystem.current.SetSelectedGameObject(go);
    }
    public void HideAll()
    {
        ToggleMainMenu(false);
        ToggleControlsMenu(false);
    }
    #endregion
}
