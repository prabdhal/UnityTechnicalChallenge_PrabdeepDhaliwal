using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
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

    [Header("Pause Menu UI")]
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button pauseMenuQuitButton;

    [Header("Player HUD")]
    [SerializeField]
    private GameObject playerHud;

    [Header("Game Over UI")]
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button gamOverQuitButton;

    private PlayerInputManager playerInputManager;
    private GameManager GameManager;

    private bool IsPaused => pauseMenu.activeInHierarchy;
    #endregion

    #region Init & Update
    private void Start()
    {
        GameManager GameManager = GameManager.Instance;
        GameManager.OnPlayerSpawn += SetupOnPauseEvent;     // Once player spawns, add pausing to PlayerInputManager pause event 
        GameManager.OnPlayerDespawn += OnGameOver;     // Once player despawns, display GameOverMenu

        playButton.onClick.AddListener(OnPlay);
        playButton.onClick.AddListener(GameManager.SpawnPlayer);
        controlsButton.onClick.AddListener(OnControls);
        quitButton.onClick.AddListener(OnQuit);

        backButton.onClick.AddListener(OnReturnToMainMenu);

        resumeButton.onClick.AddListener(OnPause);
        pauseMenuQuitButton.onClick.AddListener(OnQuit);

        retryButton.onClick.AddListener(OnPlay);
        pauseMenuQuitButton.onClick.AddListener(OnQuit);

        HideAll();
        ToggleMainMenu(true);
    }
    private void SetupOnPauseEvent(GameObject playerObj)
    {
        playerObj.GetComponent<PlayerInputManager>().OnPauseAction += OnPause;
    }

    private void Update()
    {
        if (IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    #endregion

    #region Button Events
    // Fire on pause action trigger
    public void OnPause()
    {
        bool toggle = !pauseMenu.activeInHierarchy;
        TogglePauseMenu(toggle);
    }

    private void OnPlay()
    {
        HideAll();

        TogglePlayerHud(true);
    }
    private void OnControls()
    {
        HideAll();
        ToggleControlsMenu(true);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
    // Fire on player despawn / death
    private void OnGameOver(GameObject go)
    {
        OnReturnToMainMenu();
    }
    private void OnReturnToMainMenu()
    {
        HideAll();

        ToggleMainMenu(true);
    }
    #endregion

    #region UI Actions
    public void ToggleMainMenu(bool display)
    {
        mainMenu.SetActive(display);
    }
    public void TogglePauseMenu(bool display)
    {
        pauseMenu.SetActive(display);
    }
    public void TogglePlayerHud(bool display)
    {
        playerHud.SetActive(display);
    }
    public void ToggleGameOverMenu(bool display)
    {
        gameOverMenu.SetActive(display);
    }
    public void ToggleControlsMenu(bool display)
    {
        controlsMenu.SetActive(display);
    }
    public void HideAll()
    {
        ToggleMainMenu(false);
        TogglePauseMenu(false);
        TogglePlayerHud(false);
        ToggleGameOverMenu(false);
        ToggleControlsMenu(false);
    }
    #endregion
}
