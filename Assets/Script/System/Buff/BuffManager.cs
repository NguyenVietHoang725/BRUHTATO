using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] List<GameObject> buffPrefab;
    [SerializeField] GameObject[] buffs;

    private void OnEnable()
    {
        SpawnBuff();
    }

    public void SpawnBuff()
    {
        ChooseBuff();
        
        int count = Mathf.Min(buffs.Length, transform.childCount);

        for (int i = 0; i < count; i++)
        {
            Transform oldChild = transform.GetChild(i);

            // Lấy vị trí và góc quay của object cũ
            Vector3 position = oldChild.position;
            Quaternion rotation = oldChild.rotation;

            // Xóa object cũ
            Destroy(oldChild.gameObject);

            // Tạo object mới từ buffs[i] và đặt nó làm con của parentObject
            GameObject newBuff = Instantiate(buffs[i], position, rotation, transform);
        }
    }

    private void ChooseBuff()
    {
        ShuffleList(buffPrefab);
        for (int i = 0; i < buffs.Length; i++)
            buffs[i] = buffPrefab[i];
    }
    
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]); // Hoán đổi vị trí
        }
    }
}
