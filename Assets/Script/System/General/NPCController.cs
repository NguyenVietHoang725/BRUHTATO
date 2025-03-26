using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    protected Collision2D collision;
    protected bool stayCheck;
    public void OnCollisionEnter2D(Collision2D other)
    {
        collision = other;
        stayCheck = true;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        stayCheck = false;
    }

    public virtual void NPCFunction()
    {
        return;
    }
}
