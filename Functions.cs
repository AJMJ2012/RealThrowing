using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

using DALib.Extensions;
using DALib.Functions;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace RealThrowing {
	public static class Extensions
	{
		// TODO: Throw into DALib
		public static bool Matches(this string input, string pattern)
		{
			pattern = $"^{pattern.Replace("*", ".*?")}$";
			return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
		}

		public static bool ConainsWC(this List<string> haystack, string needle)
		{
			if (haystack == null || needle == null)
			{
				return false;
			}
			return haystack.Any(s => needle.Matches(s));
		}

		public static string GetItemClass(this Item item)
		{
			if (item.ModItem != null)
			{
				return item.ModItem.GetType().ToString();
			}
			else
			{
				return item.GetType().ToString();
			}
		}

		public static string GetItemMod(this Item item)
		{
			if (item.ModItem != null)
			{
				return item.ModItem.Mod.Name;
			}
			else
			{
				return null;
			}
		}

	}

	public static class Functions {
		public static bool IsThrowable(Item item) {
			if (item.shoot > 0 && !item.accessory && item.useStyle > 0)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(item.shoot);

				// Forced
				if (Config.Server.ForcedWeaponsLists.WeaponClasses.ConainsWC(item.GetItemClass())) { return true; }
				if (Config.Server.ConvertFlails && Config.Server.ForcedWeaponsLists.FlailClasses.ConainsWC(item.GetItemClass())) { return true; }
				if (Config.Server.ConvertYoyos && Config.Server.ForcedWeaponsLists.YoyoClasses.ConainsWC(item.GetItemClass())) { return true; }

				if (Config.Server.ForcedWeaponsLists.Throwing.ContainsCI(item.GetItemID())) { return true; }
				if (Config.Server.ConvertFlails && Config.Server.ForcedWeaponsLists.Flail.ContainsCI(item.GetItemID())) { return true; }
				if (Config.Server.ConvertYoyos && Config.Server.ForcedWeaponsLists.Yoyo.ContainsCI(item.GetItemID())) { return true; }

				// Ignored
				if (Config.Server.CombinedIgnoredLists.WeaponClasses.ConainsWC(item.GetItemClass())) { return false; }

				if (Config.Server.CombinedIgnoredLists.Weapons.ContainsCI(item.GetItemID())) { return false; }
				if (Config.Server.CombinedIgnoredLists.WeaponUseStyles.ContainsCI(item.GetItemUseStyleID())) { return false; }
				if (Config.Server.CombinedIgnoredLists.WeaponProjectiles.ContainsCI(projectile.GetProjectileID())) { return false; }
				if (Config.Server.CombinedIgnoredLists.WeaponProjectileAIStyles.ContainsCI(projectile.GetProjectileAIStyleID())) { return false; }
				if (projectile.aiStyle == 0 || projectile.aiStyle == -1) { return false; }

				// Automatic (Limited)
				if ((item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged) || item.CountsAsClass(DamageClass.Throwing)))
				{
					if (!item.noUseGraphic) { return false; }
					else if (projectile.aiStyle == ProjAIStyleID.Flail || projectile.aiStyle == ProjAIStyleID.Flairon)
					{
						if (Config.Server.ConvertFlails) { return true; }
						else { return false; }
					}
					else if (projectile.aiStyle == ProjAIStyleID.Yoyo)
					{
						if (Config.Server.ConvertYoyos) { return true; }
						else { return false; }
					}
					/*else
					{
						switch (projectile.aiStyle)
						{
							case ProjAIStyleID.ThrownProjectile:
							case ProjAIStyleID.Boomerang:
							case ProjAIStyleID.Explosive:
							case ProjAIStyleID.StickProjectile:
							case ProjAIStyleID.GroundProjectile:
							case ProjAIStyleID.Harpoon:
							case ProjAIStyleID.Arrow:
							case ProjAIStyleID.MolotovCocktail:
								return true;
						}
					}*/
					else { return true; }
				}
			}
			return false;
		}
	}
}
