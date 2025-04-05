using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject buffPanel;
    [SerializeField] private PlayerController player;

    public int min, max, enemyAmount, lvl = 1;
    public bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        enemyAmount = Random.Range(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        
        if (enemyAmount > 0)
        {
            SpawnEnemy();
            enemyAmount--;
        }
        if(!GameObject.FindWithTag("Enemy"))
        {
            buffPanel.SetActive(true);
            canSpawn = false;
            player.canMove = false;
            player.canAttack = false;
        }
    }

    public void SetAmount()
    {
        lvl++;
        enemyAmount = Random.Range(min + lvl, max + lvl);
    }

    private void SpawnEnemy()
    {
        if(canSpawn)
        {
            int element = Random.Range(0, enemyPrefab.Length);
            GameObject newEnemy = Instantiate(enemyPrefab[element],
                new Vector3(Random.Range(-7, 7), Random.Range(-7, 7), 0), Quaternion.identity);
        }
    }
}
