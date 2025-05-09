using Submarine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerController _playerController;
    public PlayerController Player => _playerController;

    public bool IsGamePaused() => Time.timeScale == 0;
    public bool shouldLoadGame;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (shouldLoadGame)
        {
            LoadGame();
        }        
    }

    private void OnEnable()
    {
        if (_playerController != null)
            _playerController.OnPlayerDie += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        if (_playerController != null)
            _playerController.OnPlayerDie -= ShowGameOverScreen;
    }

    public void ShowGameOverScreen()
    {
        Invoke("GameOver", 3);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ChangeSceneByName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            SceneManager.LoadScene(name);
    }

    public void ChangeSceneByIndex(int index)
    {
        if (index >= 0)
            SceneManager.LoadScene(index);
    }

    void LoadGame()
    {
        SaveData saveData = SaveManager.currentSaveData;
        Checkpoint.lastCheckpointTriggered = -1;
        if (saveData == null)
        {
            return;
        }
        Checkpoint.lastCheckpointTriggered = saveData.checkPointIndex;
        if (SaveManager.levelChange)
            return;
        Player.transform.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY, 0);
        Player.HealthController.SetHealth(saveData.health);
    }
}
