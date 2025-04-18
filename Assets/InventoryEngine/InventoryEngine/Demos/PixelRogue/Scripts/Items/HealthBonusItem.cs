﻿using UnityEngine;
using System;

namespace MoreMountains.InventoryEngine
{	
	[CreateAssetMenu(fileName = "HealthBonusItem", menuName = "MoreMountains/InventoryEngine/HealthBonusItem", order = 1)]
	[Serializable]
	/// <summary>
	/// Demo class for a health item
	/// </summary>
	public class HealthBonusItem : InventoryItem 
	{
		[Header("Health Bonus")]
		/// the amount of health to add to the player when the item is used
		public int StatsBonus;

		/// <summary>
		/// What happens when the object is used 
		/// </summary>
		public override bool Use(string playerID)
		{
			base.Use(playerID);
			// This is where you would increase your character's health,
			// with something like : 
			// Player.Life += HealthValue;
			// of course this all depends on your game codebase.
			Debug.LogFormat("increase character "+playerID+"'s health by "+StatsBonus);
			return true;
		}
		
	}
}