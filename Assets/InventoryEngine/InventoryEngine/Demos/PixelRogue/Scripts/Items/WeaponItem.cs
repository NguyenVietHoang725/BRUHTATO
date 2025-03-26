using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{	
	[CreateAssetMenu(fileName = "WeaponItem", menuName = "MoreMountains/InventoryEngine/WeaponItem", order = 2)]
	[Serializable]
	/// <summary>
	/// Demo class for a weapon item
	/// </summary>
	public class WeaponItem : InventoryItem 
	{
		/// <summary>
		/// What happens when the object is used 
		/// </summary>
		public override bool Equip(string playerID)
		{
			base.Equip(playerID);
			TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetWeapon(Prefab,this);
			
			TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetButton(0, false);
			TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetButton(1, false);
			return true;
		}

		/// <summary>
		/// What happens when the object is used 
		/// </summary>
		public override bool UnEquip(string playerID)
		{
			base.UnEquip(playerID);
			TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().UnSetWeapon(this);
			
			TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetButton(0, true);
			TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetButton(1, true);
			return true;
		}
	}
}