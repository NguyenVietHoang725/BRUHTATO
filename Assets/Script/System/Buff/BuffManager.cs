using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuffManager : MonoBehaviour
{
    [SerializeField] List<GameObject> buffPrefab;
    [SerializeField] GameObject[] buffs;
    [SerializeField] GameObject gemsToGive;
    [SerializeField] private int gemsToGiveAmount;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Stats stats;

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
    
    private void SpawnGems()
        {
            int amount = Random.Range(Mathf.RoundToInt(gemsToGiveAmount * enemySpawner.lvl) / 5 - 5, Mathf.RoundToInt(gemsToGiveAmount * enemySpawner.lvl) / 5);
            
            if (enemySpawner.lvl % 5 == 0 && enemySpawner.lvl > PlayerPrefs.GetInt("WavePassed" + PlayerPrefs.GetString("PlayerID")))
            {
                for (int i = 0; i < amount; i++)
                {
                    Vector3 randomOffset = new Vector3(
                        Random.Range(-4f, 4f),
                        Random.Range(-4f, 4f),
                        0f
                    );
                    GameObject gemDrop = Instantiate(gemsToGive, stats.transform.position + randomOffset, stats.transform.rotation);
                }
            }
        }

    private void ChooseBuff()
    {
        ShuffleList(buffPrefab);
        for (int i = 0; i < buffs.Length; i++)
            buffs[i] = buffPrefab[i];
        SpawnGems();
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
