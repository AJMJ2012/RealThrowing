using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using System.Reflection;

namespace RealThrowing
{
    public class GProjectile : GlobalProjectile {
		public override void OnSpawn(Projectile projectile, IEntitySource source) {
			if (source != null) {
				FieldInfo itemField = source.GetType().GetField("Item", BindingFlags.Public | BindingFlags.Instance);
				if (itemField != null) {
					Item item = itemField.GetValue(source) as Item;
					if (item != null && item.DamageType == DamageClass.Throwing) {
						// Too bad you can't have multiple damage classes anymore.
						projectile.DamageType = DamageClass.Melee;
					}
				}
			}
		}
	}
}
