using Cinemachine;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    #endregion

    #region Fields
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private CinemachineFreeLook freeLookCamera;

    private GameObject spawnedPlayer;

    public Action<GameObject> OnPlayerSpawn;
    public Action<GameObject> OnPlayerDespawn;
    #endregion

    #region Init
    private void Start()
    {

    }
    #endregion

    #region Player Spawner
    public void SpawnPlayer()
    {
        if (spawnedPlayer != null) return;

        GameObject go = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity);
        spawnedPlayer = go;

        if (freeLookCamera != null)
        {
            freeLookCamera.Follow = spawnedPlayer.transform;
            freeLookCamera.LookAt = spawnedPlayer.transform;
        }

        PlayerController controller = go.GetComponent<PlayerController>();
        controller.OnPlayerKilled += DespawnPlayer;

        OnPlayerSpawn?.Invoke(go);
    }

    public void DespawnPlayer()
    {
        if (spawnedPlayer == null) return;

        OnPlayerDespawn?.Invoke(spawnedPlayer);

        freeLookCamera.Follow = null;
        freeLookCamera.LookAt = null;
        Destroy(spawnedPlayer);
        spawnedPlayer = null;
    }
    #endregion

    #region Restart Game
    public void RestartGame()
    {
        Debug.Log("GameManager - Restart");
        DespawnPlayer();

        Invoke("SpawnPlayer", 1f);
    }
    #endregion
}
