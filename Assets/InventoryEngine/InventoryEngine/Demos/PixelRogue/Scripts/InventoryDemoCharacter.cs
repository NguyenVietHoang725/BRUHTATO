using System;
using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.InventoryEngine
{	

	[RequireComponent(typeof(Rigidbody2D))]
	/// <summary>
	/// Demo character controller, very basic stuff
	/// </summary>
	public class InventoryDemoCharacter : MonoBehaviour, MMEventListener<MMInventoryEvent>
	{
		[MMInformation(
			"A very basic demo character controller, that makes the character move around on the xy axis. Here you can change its speed and bind sprites and equipment inventories.",
			MMInformationAttribute.InformationType.Info, false)]

		public string PlayerID = "PlayerX00";
		/// the Weapon inventory
		public Inventory WeaponInventory;

		public InventoryDisplay WeaponDisplay;

		/// <summary>
		/// Sets the current weapon sprite
		/// </summary>
		/// <param name="newSprite">New sprite.</param>
		/// <param name="item">Item.</param>
		public virtual void SetWeapon(GameObject prefab, InventoryItem item)
		{
			if(item.name == WeaponInventory.Content[0].name)
			{
				UnSetWeapon(item);
				GameObject newWeapon = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
			}
		}

		public virtual void UnSetWeapon(InventoryItem item)
		{
			if(GameObject.Find("BlankWeapon")) Destroy(GameObject.Find("BlankWeapon").gameObject);
			if(item.name == WeaponInventory.Content[0].name)
				Destroy(GameObject.Find(WeaponInventory.Content[0].Prefab.name + "(Clone)"));
		}

		public void SetButton(int index, bool value)
		{
			if (WeaponInventory.Content[index])
			{
				WeaponInventory.Content[index].DisplayProperties.DisplayMoveButton = value;
				WeaponInventory.Content[index].DisplayProperties.DisplayEquipButton = value;
			}
		}
		
		private void SwapWeapon()
		{
			UnSetWeapon(WeaponInventory.Content[0]);

			InventoryItem tmpItem = null;
			tmpItem = WeaponInventory.Content[0];
			WeaponInventory.Content[0] = WeaponInventory.Content[1];
			WeaponInventory.Content[1] = tmpItem;

			WeaponDisplay.UpdateInventoryContent();
			
			SetWeapon(WeaponInventory.Content[0].Prefab,WeaponInventory.Content[0]);
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.E) && WeaponInventory.Content[0] && WeaponInventory.Content[1])
				SwapWeapon();
		}

		/// <summary>
		/// Catches MMInventoryEvents and if it's an "inventory loaded" one, equips the first armor and weapon stored in the corresponding inventories
		/// </summary>
		/// <param name="inventoryEvent">Inventory event.</param>
		public virtual void OnMMEvent(MMInventoryEvent inventoryEvent)
		{
			if (inventoryEvent.InventoryEventType == MMInventoryEventType.InventoryLoaded)
			{
				if (inventoryEvent.TargetInventoryName == WeaponInventory.name)
				{
					if (WeaponInventory != null)
					{
						if (!InventoryItem.IsNull(WeaponInventory.Content [0]))
						{
							WeaponInventory.Content [0].Equip (PlayerID);	
						}
					}
				}
			}
		}

		/// <summary>
		/// On Enable, we start listening to MMInventoryEvents
		/// </summary>
		protected virtual void OnEnable()
		{
			this.MMEventStartListening<MMInventoryEvent>();
		}


		/// <summary>
		/// On Disable, we stop listening to MMInventoryEvents
		/// </summary>
		protected virtual void OnDisable()
		{
			this.MMEventStopListening<MMInventoryEvent>();
		}
	}
}