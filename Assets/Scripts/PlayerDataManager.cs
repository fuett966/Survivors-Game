using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;

    #region Singetone
    public static PlayerDataManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
            return;
        }
        Destroy(gameObject);

    }
    #endregion Singletone

    private void Initialize()
    {
        playerData = LoadPlayerData();

        if (playerData == null)
        {
            playerData = new PlayerData();
        }
    }
    public PlayerData GetData()
    {
        return playerData;
    }

    public void SavePlayerData(PlayerData _playerData)
    {
        File.WriteAllText(Application.streamingAssetsPath + "/JSON.json", JsonUtility.ToJson(_playerData));
    }
    public PlayerData LoadPlayerData()
    {
        return JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.streamingAssetsPath + "/JSON.json"));
    }
    private void OnApplicationQuit()
    {
        SavePlayerData(playerData);
    }
}
