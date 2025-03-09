using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

namespace MoreMountains.Tools
{
    public class MMSaveLoadManagerMethodServer : IMMSaveLoadManagerMethod
    {
        private string serverUrl = "http://localhost:3000/api/inventory";
        
        [System.Serializable]
        public class ServerSaveFormat
        {
            public string name;
            public string data; // Chuỗi JSON thực tế
        }


        // ✅ Lưu dữ liệu lên server
        public void Save(object objectToSave, FileStream fileStream)
        {
            string fileName = Path.GetFileNameWithoutExtension(fileStream.Name);
            MonoBehaviourHelper.Instance.StartCoroutine(SaveToServer(objectToSave, fileName));
        }

        // Gửi dữ liệu lên server
        private IEnumerator SaveToServer(object objectToSave, string fileName)
        {
            // Tạo JSON với cấu trúc yêu cầu
            string jsonData = $"{{\"name\": \"{fileName}\", \"data\": {JsonUtility.ToJson(objectToSave)}}}";
            Debug.Log($"📂 Dữ liệu chuẩn bị lưu: {jsonData}");

            UnityWebRequest request = new UnityWebRequest(serverUrl + "/save", "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"✅ Lưu dữ liệu lên server thành công! URL: {serverUrl}/save");
                Debug.Log($"📂 File đã lưu: {fileName}");
                Debug.Log($"📂 Dữ liệu đã lưu: {jsonData}");
                Debug.Log($"📩 Phản hồi từ server: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"❌ Lỗi khi lưu lên server ({serverUrl}/save): {request.error}");
            }
        }
        
        public object Load(Type objectType, FileStream fileStream)
        {
            object result = null;
            MonoBehaviourHelper.Instance.StartCoroutine(LoadFromServer(objectType, fileStream, (loadedObject) => 
            {
                result = loadedObject;
            }));

            return result; // Giá trị ban đầu là null, vì Coroutine chạy bất đồng bộ
        }


        private IEnumerator LoadFromServer(Type objectType, FileStream fileStream, Action<object> callback)
        {
            string fileName = Path.GetFileNameWithoutExtension(fileStream.Name);
            string requestUrl = $"http://localhost:3000/api/inventory/load?name={fileName}";

            using (UnityWebRequest request = UnityWebRequest.Get(requestUrl))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;
                    Debug.Log($"✅ Phản hồi từ server: {json}");
                    object loadedObject = JsonUtility.FromJson(json, objectType);
                    Debug.Log($"🎯 Đã parse thành công: {loadedObject}");
                    callback?.Invoke(loadedObject);
                }
                else
                {
                    Debug.LogError($"❌ Lỗi tải file từ server: {request.error}");
                    callback?.Invoke(null);
                }
            }
        }


    }
}
