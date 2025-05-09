using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSlot : MonoBehaviour
{
    [SerializeField] int slotNumber;
    [SerializeField] Image screenshotDisplayImage;
    [SerializeField] TextMeshProUGUI depthDisplay;
    [SerializeField] Button slotButton;
    [SerializeField] int firstLevelIndex = 3;
    [SerializeField] int firstLevelCheckpointCount = 3;
    [SerializeField] int secondLevelIndex = 6;
    [SerializeField] GameObject defaultImage;
    SaveData saveData;
    Texture2D screenshotTexture;
    private void Awake()
    {
        saveData = SaveManager.ReadSaveData(slotNumber);
        if(saveData != null)
        {
            screenshotTexture = saveData.CreateTexture2DFromScreenshotData();
            screenshotDisplayImage.sprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), Vector2.one * 0.5f);
            depthDisplay.text = $"Depth: {saveData.depth}m";
            defaultImage.SetActive(false);
        }         
    }
    private void OnEnable()
    {
        slotButton.onClick.AddListener(LoadGame);
    }
    private void OnDisable()
    {
        slotButton.onClick.RemoveListener(LoadGame);
    }

    private void OnDestroy()
    {
        if (!screenshotTexture)
        {
            return;
        }
        Destroy(screenshotTexture);
    }

    void LoadGame()
    {
        SaveManager.currentGameSlot = slotNumber;
        SaveManager.currentSaveData = saveData;
        int sceneIndex = saveData.checkPointIndex < firstLevelCheckpointCount ? firstLevelIndex : secondLevelIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    public void DeleteData()
    {
        //comment
        SaveManager.DeleteData(slotNumber);
        Destroy(screenshotTexture);
        screenshotTexture = null;
        defaultImage.SetActive(true);
    }
    public void StartNewGame()
    {
        SaveManager.currentGameSlot = slotNumber;
        SaveManager.currentSaveData = null;
        SceneManager.LoadScene(1);
    }
}
