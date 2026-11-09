using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;

namespace RealThrowing;

public class GItem : GlobalItem
{
	public override void SetDefaults(Item item)
	{
		if (item.shoot > 0 && !item.accessory && item.useStyle > 0)
		{
			if (Functions.IsThrowable(item))
			{
				item.mana = 0;
				item.DamageType = Config.Server.ToType switch
				{
					DamageTypes.Throwing => DamageClass.Throwing,
					DamageTypes.Ranged => DamageClass.Ranged,
					DamageTypes.Melee => DamageClass.Melee,
					_ => DamageClass.Generic,
				};
			}
		}
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		if (Functions.CheckMods()) return;
		if (item.CountsAsClass(DamageClass.Throwing))
		{
			tooltips.Add(new TooltipLine(Mod, "Affects", "Melee and Ranged stats affect this item"));
		}
	}

	public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
	{
		if (Functions.CheckMods()) return;
		if (item.CountsAsClass(DamageClass.Throwing))
		{
			damage = damage.CombineWith(player.GetDamage(DamageClass.Melee)).CombineWith(player.GetDamage(DamageClass.Ranged));
		}
	}

	public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
	{
		if (Functions.CheckMods()) return;
		if (item.CountsAsClass(DamageClass.Throwing))
		{
			crit += (int)player.GetCritChance(DamageClass.Melee) + (int)player.GetCritChance(DamageClass.Ranged);
		}
	}

	public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
	{
		if (Functions.CheckMods()) return;
		if (item.CountsAsClass(DamageClass.Throwing))
		{
			knockback = knockback.CombineWith(player.GetKnockback(DamageClass.Melee)).CombineWith(player.GetKnockback(DamageClass.Ranged));
		}
	}

	public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (Functions.CheckMods()) return;
		if (item.CountsAsClass(DamageClass.Throwing))
		{
			velocity *= player.GetAttackSpeed(DamageClass.Melee);
		}
	}
}
