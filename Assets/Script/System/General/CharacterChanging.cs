using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class CharacterChanging : NPCController
{
    [SerializeField] protected GameObject charPrefab;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && stayCheck)
            NPCFunction();
    }

    public override void NPCFunction()
    {
        if (collision == null) return;

        GameObject newCharacter = Instantiate(charPrefab, collision.transform.position, Quaternion.identity);

        CharacterManager.Instance.CurrentCharacter = newCharacter;
        
        if (newCharacter.GetComponent<Inventory>() != null)
        {
            newCharacter.GetComponent<Inventory>().RegisterInventory();
        }
        
        Destroy(collision.gameObject);
    }
}
