using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGem : Gem
{
    [SerializeField] private int ammount = 5;
	public override void ChangeStat(PlayerStats playerStats)
	{
		playerStats.AddHealth(ammount);
	}
}