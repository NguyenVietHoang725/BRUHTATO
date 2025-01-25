using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;

    public int min, max, enemyAmount, lvl = 1;
    // Start is called before the first frame update
    void Start()
    {
        enemyAmount = Random.Range(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        
        if (enemyAmount > 0)
        {
            SpawnEnemy();
            enemyAmount--;
            Debug.Log(enemyAmount);
        }
        if(!GameObject.FindWithTag("Enemy"))
        {
            lvl++;
            enemyAmount = Random.Range(min + lvl, max + lvl);
        }
    }

    private void SpawnEnemy()
    {
        int element = Random.Range(0, enemyPrefab.Length);
        GameObject newEnemy = Instantiate(enemyPrefab[element], new Vector3(Random.Range(-5,5), Random.Range(6,16),0), Quaternion.identity);
    }
}
