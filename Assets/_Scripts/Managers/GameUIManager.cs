using UnityEngine;
using UnityEngine.EventSystems;
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
    private Button gameOverQuitButton;

    [Header("Victory UI")]
    [SerializeField]
    private GameObject victoryMenu;
    [SerializeField]
    private Button victoryRetryButton;
    [SerializeField]
    private Button victoryQuitButton;

    [Header("Game Over UI")]
    public ItemMessageHud itemMessagesHud;

    [Header("Audio Settings")]
    [SerializeField]
    private AudioClip gameOverSound;
    [SerializeField]
    private AudioClip victorySound;

    private AudioSource audioSource;

    private bool IsPaused => pauseMenu.activeInHierarchy || controlsMenu.activeInHierarchy || statsMenu.activeInHierarchy;
    private bool uiActive => IsPaused || IsGameOver;
    private bool IsGameOver => gameOverMenu.activeInHierarchy || victoryMenu.activeInHierarchy;

    private PlayerInputManager inputManager;    // required for deteching pause press
    #endregion

    #region Init & Update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

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
        gameOverQuitButton.onClick.AddListener(OnQuit);
        
        // Victory menu
        victoryRetryButton.onClick.AddListener(OnRestart);
        victoryQuitButton.onClick.AddListener(OnQuit);

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

        if (IsPaused)           // freeze time
        {
            Time.timeScale = 0f;
        }
        else if (IsGameOver)    // slow time by half during game over or victory screen
        {
            Time.timeScale = 0.5f;
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

        if (display)
            HighlightElement(resumeButton.gameObject);
    }
    public void ToggleControlsMenu(bool display)
    {
        controlsMenu.SetActive(display);

        if (display)
            HighlightElement(backButton.gameObject);
    }
    public void ToggleStatsMenu(bool display)
    {
        statsMenu.SetActive(display);

        if (display)
            HighlightElement(statsMenuBackButton.gameObject);
    }
    public void ToggleGameOverMenu(bool display)
    {
        gameOverMenu.SetActive(display);

        if (display)
            HighlightElement(retryButton.gameObject);
    }
    public void ToggleVictoryMenu(bool display)
    {
        victoryMenu.SetActive(display);

        if (display)
            HighlightElement(victoryRetryButton.gameObject);
    }
    private void HighlightElement(GameObject go)
    {
        EventSystem.current.firstSelectedGameObject = go;
        EventSystem.current.SetSelectedGameObject(go);
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
        SceneManager.LoadScene(StringData.MainMenuScene);
    }
    // Reload game scene
    private void OnRestart()
    {
        SceneManager.LoadScene(StringData.GameScene);
    }
    // Fire on player death 
    private void OnGameOver()
    {
        HideAllMenus();
        ToggleGameOverMenu(true);
        audioSource.clip = gameOverSound;
        audioSource.Play();

    }
    // Fire on boss death 
    public void OnVictory()
    {
        HideAllMenus();
        ToggleVictoryMenu(true);
        audioSource.clip = victorySound;
        audioSource.Play();
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
        ToggleVictoryMenu(false);
        ToggleControlsMenu(false);
        ToggleStatsMenu(false);
    }
    #endregion
}
