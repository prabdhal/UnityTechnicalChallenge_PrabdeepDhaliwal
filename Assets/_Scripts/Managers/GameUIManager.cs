using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    #region Singleton
    public static GameUIManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    #endregion

    #region Fields

    [Header("Player HUD")]
    [SerializeField]
    private GameObject playerHud;

    [Header("Pause Menu UI")]
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button controlsButton;
    [SerializeField]
    private Button statsButton;
    [SerializeField]
    private Button pauseMenuQuitButton;

    [Header("Controls Menu UI")]
    [SerializeField]
    private GameObject controlsMenu;
    [SerializeField]
    private Button backButton;

    [Header("Stats Menu UI")]
    [SerializeField]
    private StatsMenu statsMenuScript;
    [SerializeField]
    private GameObject statsMenu;
    [SerializeField]
    private Button statsMenuBackButton;

    [Header("Game Over UI")]
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button gamOverQuitButton;

    [Header("Game Over UI")]
    public ItemMessageHud itemMessagesHud;

    private bool IsPaused => pauseMenu.activeInHierarchy || controlsMenu.activeInHierarchy || statsMenu.activeInHierarchy;
    private bool uiActive => IsPaused || gameOverMenu.activeInHierarchy;
    private bool IsGameOver => gameOverMenu.activeInHierarchy;

    private PlayerInputManager inputManager;    // required for deteching pause press
    #endregion

    #region Init & Update
    private void Start()
    {
        // Pause menu
        resumeButton.onClick.AddListener(OnPause);
        controlsButton.onClick.AddListener(OnControls);
        statsButton.onClick.AddListener(OnStats);
        pauseMenuQuitButton.onClick.AddListener(OnQuit);

        // Controls menu
        backButton.onClick.AddListener(OnReturnToPauseMenu);

        // Stats menu
        statsMenuBackButton.onClick.AddListener(OnReturnToPauseMenu);

        // Game over menu
        retryButton.onClick.AddListener(OnRestart);
        gamOverQuitButton.onClick.AddListener(OnQuit);

        GameManager.Instance.OnPlayerDespawn += OnGameOver;
        statsMenuScript.Setup(GameManager.Instance.SpawnedPlayer);

        HideAllMenus();
        TogglePlayerHud(true);

        inputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void Update()
    {
        if (inputManager != null)
        {
            if (inputManager.IsPaused)
            {
                OnPause();
            }
        }

        if (IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        CursorHandler();    // Enable/disable cursor depending on UI panels active or not
    }
    #endregion

    #region Cursor Handler
    private void CursorHandler()
    {
        if (uiActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    #endregion

    #region UI Toggles
    public void TogglePlayerHud(bool display)
    {
        playerHud.SetActive(display);
    }
    public void TogglePauseMenu(bool display)
    {
        pauseMenu.SetActive(display);
    }
    public void ToggleControlsMenu(bool display)
    {
        controlsMenu.SetActive(display);
    }
    public void ToggleStatsMenu(bool display)
    {
        statsMenu.SetActive(display);
    }
    public void ToggleGameOverMenu(bool display)
    {
        gameOverMenu.SetActive(display);
    }
    #endregion

    #region Button Events
    // Fire on pause action trigger
    private void OnPause()
    {
        if (IsGameOver) return;     // Don't allow pausing when game is over

        bool toggle = !IsPaused;

        // close all if unpausing
        if (!toggle)
            HideAllMenus();

        TogglePauseMenu(toggle);
    }
    private void OnControls()
    {
        HideAllMenus();

        ToggleControlsMenu(true);
    }
    private void OnStats()
    {
        HideAllMenus();

        ToggleStatsMenu(true);
    }
    private void OnReturnToPauseMenu()
    {
        HideAllMenus();

        TogglePauseMenu(true);
    }
    // Load main menu scene
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(StringData.MainMenuScene);   // Load home scene
    }
    private void OnRestart()
    {
        SceneManager.LoadScene(StringData.GameScene);   // Reload game scene
    }
    // Fire on player death / despawn
    private void OnGameOver()
    {
        HideAllMenus();
        
        ToggleGameOverMenu(true);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
    // Hide all except player HUD
    private void HideAllMenus()
    {
        TogglePauseMenu(false);
        ToggleGameOverMenu(false);
        ToggleControlsMenu(false);
        ToggleStatsMenu(false);
    }
    #endregion
}
