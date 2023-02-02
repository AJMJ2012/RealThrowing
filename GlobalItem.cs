using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

using DALib.Extensions;
using DALib.Functions;

namespace RealThrowing {
	public class GItem : GlobalItem {
		public override void SetDefaults(Item item) {
			if (item.damage > 0 && item.shoot > 0 && !item.accessory) {
				bool ToThrowing = Config.Server.ForcedWeaponsLists.Throwing.ContainsCI(item.GetItemID());
				if (Functions.IsThrowable(item) || ToThrowing) {
					item.mana = 0;
					item.DamageType = DamageClass.Throwing;
				}
			}
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
			if (item.CountsAsClass(DamageClass.Throwing) && !ModLoader.TryGetMod("ThoriumMod", out var _)) {
				tooltips.Add(new TooltipLine(Mod, "Affects", "Melee and Ranged stats affect this item"));
			}
		}

		public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage) {
			if (item.CountsAsClass(DamageClass.Throwing) && !ModLoader.TryGetMod("ThoriumMod", out var _)) {
				damage = damage.CombineWith(player.GetDamage(DamageClass.Melee)).CombineWith(player.GetDamage(DamageClass.Ranged));
			}
		}

		public override void ModifyWeaponCrit(Item item, Player player, ref float crit) {
			if (item.CountsAsClass(DamageClass.Throwing) && !ModLoader.TryGetMod("ThoriumMod", out var _)) {
				crit += (int)player.GetCritChance(DamageClass.Melee) + (int)player.GetCritChance(DamageClass.Ranged);
			}
		}

		public override void  ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback) {
			if (item.CountsAsClass(DamageClass.Throwing) && !ModLoader.TryGetMod("ThoriumMod", out var _)) {
				knockback = knockback.CombineWith(player.GetKnockback(DamageClass.Melee)).CombineWith(player.GetKnockback(DamageClass.Ranged));
			}
		}

		public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (item.CountsAsClass(DamageClass.Throwing) && !ModLoader.TryGetMod("ThoriumMod", out var _)) {
				velocity *= player.GetAttackSpeed(DamageClass.Melee);
			}
		}
	}
}
