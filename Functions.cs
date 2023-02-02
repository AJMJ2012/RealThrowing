using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

using DALib.Extensions;
using DALib.Functions;

namespace RealThrowing {
	public static class Functions {
		public static bool IsThrowable(Item item) {
			Projectile projectile = new Projectile();
			projectile.SetDefaults(item.shoot);
			if (Config.Server.IgnoredLists.Weapons.ContainsCI(item.GetItemID())) { return false; }
			if (Config.Server.IgnoredLists.WeaponUseStyles.ContainsCI(item.GetItemUseStyleID())) { return false; }
			if (Config.Server.IgnoredLists.WeaponProjectiles.ContainsCI(projectile.GetProjectileID())) { return false; }
			if (Config.Server.CombinedIgnoredLists.WeaponProjectileAIStyles.ContainsCI(projectile.GetProjectileAIStyleID())) { return false; }
			if (!Config.Server.ConvertFlails && projectile.aiStyle == ProjAIStyleID.Flail) { return false; }
			if (!Config.Server.ConvertYoyos && projectile.aiStyle == ProjAIStyleID.Yoyo) { return false; }
			return ((item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged)) && item.shoot > 0 && item.noUseGraphic && item.noMelee);
		}
	}
}
