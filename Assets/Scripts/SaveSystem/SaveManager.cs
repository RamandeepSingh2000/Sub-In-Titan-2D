using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] Camera screenshotCamera;
    [SerializeField] DepthManager depthManager;
    public static int currentGameSlot = 0;
    public static SaveData currentSaveData;
    static string directoryPath = $"{Application.dataPath}/Saves";
    public static bool levelChange = false;
    public void SaveGameData()
    {
        screenshotCamera.Render();
        var rt = screenshotCamera.targetTexture;
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height);

        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        // Restore previously active render texture
        RenderTexture.active = currentActiveRT;
        
        var saveData = new SaveData()
        {
            screenshotData = tex.EncodeToPNG(),
            playerPositionX = GameManager.Instance.Player.transform.position.x,
            playerPositionY = GameManager.Instance.Player.transform.position.y,
            health = GameManager.Instance.Player.HealthController.HealthPoints,
            depth = depthManager.Depth,
            screenshotHeight = tex.height,
            screenshotWidth = tex.width,
            checkPointIndex = Checkpoint.lastCheckpointTriggered
        };

        Destroy(tex);
        EnsureDirectoryExists();
        using (var filestream = new FileStream($"{directoryPath}/Save{currentGameSlot}", FileMode.Create))
        {
            new BinaryFormatter().Serialize(filestream, saveData);
        }
    }

    static void EnsureDirectoryExists()
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public static SaveData ReadSaveData(int slotNumber)
    {
        EnsureDirectoryExists();
        string path = $"{directoryPath}/Save{slotNumber}";
        if (!File.Exists(path))
        {
            return null;
        }
        using (var filestream = new FileStream(path, FileMode.Open))
        {
            SaveData saveData = (SaveData)new BinaryFormatter().Deserialize(filestream);
            return saveData;
        }
    }

    public static void DeleteData(int slotNumber)
    {
        EnsureDirectoryExists();
        string path = $"{directoryPath}/Save{slotNumber}";
        if (!File.Exists(path))
        {
            return;
        }
        File.Delete(path);
    }

}
[System.Serializable]
public class SaveData
{
    public byte[] screenshotData;
    public float playerPositionX;
    public float playerPositionY;
    public int health;
    public float depth;
    public int screenshotWidth;
    public int screenshotHeight;
    public int checkPointIndex;
    public Texture2D CreateTexture2DFromScreenshotData()
    {
        Texture2D tex = new Texture2D(screenshotWidth, screenshotHeight);
        tex.LoadImage(screenshotData);
        return tex;
    }
}
