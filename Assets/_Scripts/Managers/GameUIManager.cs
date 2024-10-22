using System.Collections;
using System.Collections.Generic;
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
    private Button pauseMenuQuitButton;

    [Header("Controls Menu UI")]
    [SerializeField]
    private GameObject controlsMenu;
    [SerializeField]
    private Button backButton;

    [Header("Game Over UI")]
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button gamOverQuitButton;

    private bool IsPaused => pauseMenu.activeInHierarchy || controlsMenu.activeInHierarchy;
    #endregion

    #region Init & Update
    private void Start()
    {
        // Pause menu
        resumeButton.onClick.AddListener(OnPause);
        controlsButton.onClick.AddListener(OnControls);
        pauseMenuQuitButton.onClick.AddListener(OnQuit);

        // Controls menu
        backButton.onClick.AddListener(OnReturnToPauseMenu);

        // Game over menu
        retryButton.onClick.AddListener(OnRestart);
        gamOverQuitButton.onClick.AddListener(OnQuit);

        GameManager.Instance.OnPlayerDespawn += OnGameOver;

        HideAll();
        TogglePlayerHud(true);
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
    public void ToggleGameOverMenu(bool display)
    {
        gameOverMenu.SetActive(display);
    }
    #endregion

    #region Button Events
    // Fire on pause action trigger
    private void OnPause()
    {
        bool toggle = !pauseMenu.activeInHierarchy;
        TogglePauseMenu(toggle);
    }
    private void OnControls()
    {
        ToggleControlsMenu(true);
    }
    private void OnReturnToPauseMenu()
    {
        HideAll();

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
        HideAll();
        
        TogglePlayerHud(true);
        ToggleGameOverMenu(true);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
    // Hides all menus
    private void HideAll()
    {
        TogglePauseMenu(false);
        TogglePlayerHud(false);
        ToggleGameOverMenu(false);
        ToggleControlsMenu(false);
    }
    #endregion
}
