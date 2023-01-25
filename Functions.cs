using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace RealThrowing {
	public static class Functions {
		public static bool IsThrowable(Item item) {
			Projectile projectile = new Projectile();
			projectile.SetDefaults(item.shoot);
			if (DALib.Functions.InList(Config.Server.IgnoredLists.Weapons, DALib.Functions.GetItemID(item))) { return false; }
			if (DALib.Functions.InList(Config.Server.IgnoredLists.WeaponUseStyles, DALib.Functions.GetItemUseStyleID(item))) { return false; }
			if (DALib.Functions.InList(Config.Server.IgnoredLists.WeaponProjectiles, DALib.Functions.GetProjectileID(projectile))) { return false; }
			if (DALib.Functions.InList(Config.Server.CombinedIgnoredLists.WeaponProjectileAIStyles, DALib.Functions.GetProjectileAIStyleID(projectile))) { return false; }
			if (!Config.Server.ConvertFlails && projectile.aiStyle == ProjAIStyleID.Flail) { return false; }
			if (!Config.Server.ConvertYoyos && projectile.aiStyle == ProjAIStyleID.Yoyo) { return false; }
			return ((item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged)) && item.shoot > 0 && item.noUseGraphic && item.noMelee);
		}
	}
}
