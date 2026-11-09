using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

using DALib.Extensions;
using DALib.Functions;

namespace RealThrowing;

public static class Functions
{
	public static bool IsThrowable(Item item)
	{
		if (item.shoot > 0 && !item.accessory && item.useStyle > 0)
		{
			Projectile projectile = new Projectile();
			projectile.SetDefaults(item.shoot);

			// Mod Integrations
			if (Config.Server.ConvertSplitRaquets && item.GetItemClass().Matches("Split.Content.Weapons.Racquets.*")) { return true; }

			// Forced
			if (Config.Server.ConvertThrown && Config.Server.CombinedForcedLists.Thrown.ContainsCI(item.GetItemID())) { return true; }
			if (Config.Server.ConvertThrown && Config.Server.CombinedForcedLists.ThrownClasses.ConainsWC(item.GetItemClass())) { return true; }
			if (Config.Server.ConvertFlails && Config.Server.CombinedForcedLists.Flail.ContainsCI(item.GetItemID())) { return true; }
			if (Config.Server.ConvertFlails && Config.Server.CombinedForcedLists.FlailClasses.ConainsWC(item.GetItemClass())) { return true; }
			if (Config.Server.ConvertYoyos && Config.Server.CombinedForcedLists.Yoyo.ContainsCI(item.GetItemID())) { return true; }
			if (Config.Server.ConvertYoyos && Config.Server.CombinedForcedLists.YoyoClasses.ConainsWC(item.GetItemClass())) { return true; }

			// Ignored
			if (Config.Server.CombinedIgnoredLists.Weapons.ContainsCI(item.GetItemID())) { return false; }
			if (Config.Server.CombinedIgnoredLists.WeaponClasses.ConainsWC(item.GetItemClass())) { return false; }
			if (Config.Server.CombinedIgnoredLists.WeaponProjectiles.ContainsCI(projectile.GetProjectileID())) { return false; }
			if (Config.Server.CombinedIgnoredLists.WeaponProjectileClasses.ConainsWC(item.GetItemClass())) { return false; }

			// Internal Ignored Styles
			if (Config.Server.HardIgnoredLists.WeaponUseStyles.ContainsCI(item.GetItemUseStyleID())) { return false; }
			if (Config.Server.HardIgnoredLists.WeaponProjectileAIStyles.ContainsCI(projectile.GetProjectileAIStyleID())) { return false; }

			// Restrict loose checks
			if (projectile.aiStyle <= 0)
			{
				return false;
			}

			// Automatic (Limited)
			if ((item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged) || item.CountsAsClass(DamageClass.Throwing)))
			{
				if (!item.noUseGraphic)
				{
					return false;
				}
				else if (projectile.aiStyle == ProjAIStyleID.Flail || projectile.aiStyle == ProjAIStyleID.Flairon)
				{
					return Config.Server.ConvertFlails;
				}
				else if (projectile.aiStyle == ProjAIStyleID.Yoyo)
				{
					return Config.Server.ConvertYoyos;
				}
				else
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool CheckMods()
	{
		string[] mods = [
			"ThoriumMod",
			];
		foreach (string mod in mods)
		{
			if (ModLoader.TryGetMod(mod, out var _))
			{
				return true;
			}
		}
		return false;
	}
}
