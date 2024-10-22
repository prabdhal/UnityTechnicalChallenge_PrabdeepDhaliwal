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
    private Button quitButton;

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

    private PlayerInputManager playerInputManager;
    private GameManager GameManager;

    private bool IsPaused => pauseMenu.activeInHierarchy;
    #endregion

    #region Init & Update
    private void Start()
    {
        GameManager GameManager = GameManager.Instance;
        GameManager.OnPlayerSpawn += SetupOnPauseEvent;     // Once player spawns, add pausing to PlayerInputManager pause event 

        playButton.onClick.AddListener(OnPlay);
        playButton.onClick.AddListener(GameManager.SpawnPlayer);
        quitButton.onClick.AddListener(OnQuit);

        resumeButton.onClick.AddListener(OnPause);
        pauseMenuQuitButton.onClick.AddListener(OnQuit);

        ToggleMainMenu(true);
        TogglePauseMenu(false);
        TogglePlayerHud(false);
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
    private void OnPlay()
    {
        ToggleMainMenu(false);
        TogglePlayerHud(true);
    }
    private void OnQuit()
    {
        Application.Quit();
    }

    // Fire on pause action trigger
    public void OnPause()
    {
        bool toggle = !pauseMenu.activeInHierarchy;

        TogglePauseMenu(toggle);
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
    #endregion
}
