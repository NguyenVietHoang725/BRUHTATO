using System;
using System.Collections;
using MoreMountains.InventoryEngine;
using UnityEngine;
using UnityEngine.Networking;

public class ServerSync : MonoBehaviour
{
    [SerializeField] private string serverBaseUrl = "https://yourserver.com/api/inventory"; // URL cơ sở của server
    [SerializeField] private string inventorySaveName = "MainInventorySavePlayerX00";
    [SerializeField] private bool useEncryption = false; // Tùy chọn sử dụng mã hóa // Định danh duy nhất của người chơi
    [SerializeField] private string playerID;
    
    public void SetPlayerID(string PlayerID)
    {
        this.playerID = PlayerID;
        PlayerPrefs.SetString("PlayerID", PlayerID);
        Debug.Log("Player ID: " + playerID);
    }

    // Lưu dữ liệu lên server tại endpoint /save
    public void SaveDataToServer()
    {
        // Lấy dữ liệu từ PlayerPrefs
        string inventoryData = PlayerPrefs.GetString(inventorySaveName);
        Debug.Log("Data in PlayerPrefs: " + inventoryData);

        // Tạo JSON object chứa dữ liệu và playerID
        ServerData dataToSend = new ServerData()
        {
            playerID = PlayerPrefs.GetString("PlayerID"), // Thêm playerID vào dữ liệu gửi lên server
            inventory = useEncryption ? EncryptData(inventoryData) : inventoryData // Mã hóa nếu cần
        };

        string jsonData = JsonUtility.ToJson(dataToSend);
        Debug.Log("Data sync to server: " + jsonData);

        // Gửi dữ liệu lên server tại endpoint /save
        string saveUrl = serverBaseUrl + "/save";
        StartCoroutine(PostRequest(saveUrl, jsonData));
    }

    // Tải dữ liệu từ server tại endpoint /load và ghi đè lên PlayerPrefs
    public void LoadDataFromServer()
    {
        // Gửi playerID để tải dữ liệu cụ thể của người chơi
        string loadUrl = serverBaseUrl + "/load?playerID=" + playerID;
        StartCoroutine(GetRequest(loadUrl));
    }

    // Gửi dữ liệu lên server bằng POST request
    private IEnumerator PostRequest(string url, string jsonData, int retryCount = 3)
    {
        int attempts = 0;
        while (attempts < retryCount)
        {
            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Data saved to server successfully!");
                    yield break; // Thoát khỏi vòng lặp nếu thành công
                }
                else
                {
                    Debug.LogError("Failed to save data to server: " + request.error);
                    attempts++;
                    if (attempts < retryCount)
                    {
                        yield return new WaitForSeconds(2); // Chờ 2 giây trước khi thử lại
                    }
                }
            }
        }
        Debug.LogError("Failed to save data after " + retryCount + " attempts.");
    }

    // Tải dữ liệu từ server bằng GET request
    private IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse dữ liệu từ server
                string jsonData = request.downloadHandler.text;
                Debug.Log("Data sync from server: " + jsonData);
                ServerData serverData = JsonUtility.FromJson<ServerData>(jsonData);

                // Kiểm tra tính hợp lệ của dữ liệu trước khi ghi đè
                if (!string.IsNullOrEmpty(serverData.inventory))
                {
                    string inventoryData = useEncryption ? DecryptData(serverData.inventory) : serverData.inventory; // Giải mã nếu cần
                    Debug.Log("Data after parse: " + inventoryData);

                    PlayerPrefs.SetString(inventorySaveName, inventoryData);
                    PlayerPrefs.Save();
                    Debug.Log("Data loaded from server successfully!");
                }
                else
                {
                    Debug.LogError("Invalid data received from server.");
                }
            }
            else
            {
                Debug.LogError("Failed to load data from server: " + request.error);
            }
        }
    }

    // Class để lưu trữ dữ liệu từ server
    [System.Serializable]
    private class ServerData
    {
        public string playerID;
        public string inventory;
    }

    // Mã hóa dữ liệu (tùy chọn)
    private string EncryptData(string data)
    {
        if (!useEncryption) return data; // Bỏ qua mã hóa nếu không cần
        // Thêm logic mã hóa tại đây (ví dụ: sử dụng AES)
        return data; // Thay thế bằng mã hóa thực tế
    }

    // Giải mã dữ liệu (tùy chọn)
    private string DecryptData(string encryptedData)
    {
        if (!useEncryption) return encryptedData; // Bỏ qua giải mã nếu không cần
        // Thêm logic giải mã tại đây (ví dụ: sử dụng AES)
        return encryptedData; // Thay thế bằng giải mã thực tế
    }
}