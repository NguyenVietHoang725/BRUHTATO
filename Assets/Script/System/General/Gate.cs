using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [SerializeField] private int scene;
    [SerializeField] private PlayerController player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeScene();
        }
    }

    private void Update()
    {
        if(player && !player.gameObject.activeSelf) ChangeScene();
    }

    public void ChangeScene()
    {
        MMGameEvent.Trigger("Save");
        StartCoroutine(ChangeSceneCoroutine());
    }
    
    private IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(scene);
    }
}
