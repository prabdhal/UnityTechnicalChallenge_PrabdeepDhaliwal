using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        SpawnPlayer();
    }
    #endregion

    #region Fields
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private CinemachineFreeLook freeLookCam;
    [SerializeField]
    private CinemachineTargetGroup targetGroupCam;

    private GameObject spawnedPlayer;
    public GameObject SpawnedPlayer => spawnedPlayer;

    public Action<GameObject> OnPlayerSpawn;
    public Action OnPlayerDespawn;
    #endregion


    #region Player Spawner
    public void SpawnPlayer()
    {
        if (spawnedPlayer != null) return;

        GameObject go = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity);
        spawnedPlayer = go;

        if (targetGroupCam != null)
        {
            targetGroupCam.AddMember(spawnedPlayer.transform, 1f, 10);
        }

        if (freeLookCam != null)
        {
            freeLookCam.Follow = targetGroupCam.transform;
            freeLookCam.LookAt = targetGroupCam.transform;
        }

        PlayerController controller = go.GetComponent<PlayerController>();
        controller.OnPlayerKilled += DespawnPlayer;

        OnPlayerSpawn?.Invoke(spawnedPlayer);
    }

    public void DespawnPlayer()
    {
        if (spawnedPlayer == null) return;

        OnPlayerDespawn?.Invoke();

        freeLookCam.Follow = null;
        freeLookCam.LookAt = null;
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
