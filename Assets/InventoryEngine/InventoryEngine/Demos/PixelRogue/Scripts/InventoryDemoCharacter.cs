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

		public string PlayerID = "Player1";
		/// the sprite used to show the current weapon
		public GameObject WeaponPrefab;
		/// the armor inventory
		public Inventory WeaponInventory1;
		/// the weapon inventory
		public Inventory WeaponInventory2;

		private void Start()
		{
			WeaponPrefab = GameObject.FindWithTag("Weapon");
		}

		/// <summary>
		/// Sets the current weapon sprite
		/// </summary>
		/// <param name="newSprite">New sprite.</param>
		/// <param name="item">Item.</param>
		public virtual void SetWeapon(GameObject prefab, InventoryItem item)
		{
			WeaponPrefab = GameObject.FindWithTag("Weapon");
			Destroy(WeaponPrefab);
			WeaponPrefab = prefab;
			GameObject newWeapon = Instantiate(WeaponPrefab, new Vector3(0,0,0), Quaternion.identity);
		}

		/// <summary>
		/// Catches MMInventoryEvents and if it's an "inventory loaded" one, equips the first armor and weapon stored in the corresponding inventories
		/// </summary>
		/// <param name="inventoryEvent">Inventory event.</param>
		public virtual void OnMMEvent(MMInventoryEvent inventoryEvent)
		{
			if (inventoryEvent.InventoryEventType == MMInventoryEventType.InventoryLoaded)
			{
				if (inventoryEvent.TargetInventoryName == "RogueWeaponInventory1")
				{
					if (WeaponInventory1 != null)
					{
						if (!InventoryItem.IsNull(WeaponInventory1.Content [0]))
						{
							WeaponInventory1.Content [0].Equip (PlayerID);	
						}
					}
				}
				if (inventoryEvent.TargetInventoryName == "RogueWeaponInventory2")
				{
					if (WeaponInventory2 != null)
					{
						if (!InventoryItem.IsNull (WeaponInventory2.Content [0]))
						{
							WeaponInventory2.Content [0].Equip (PlayerID);
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