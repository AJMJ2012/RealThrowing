using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace RealThrowing.Hooks.ItemLoaderHook {
	class WeaponPrefix : ModSystem {
		public override void Load() {
			try { HookEndpointManager.Add(WeaponPrefixMethod, Override_WeaponPrefix); } catch { throw new Exception("Unable to hook into ItemLoader.WeaponPrefix"); }
		}

		public override void Unload() {
			try { HookEndpointManager.Remove(WeaponPrefixMethod, Override_WeaponPrefix); } catch {}
		}

		static MethodInfo WeaponPrefixMethod => typeof(ItemLoader).GetMethod("WeaponPrefix", BindingFlags.NonPublic | BindingFlags.Static);
		delegate bool OrigWeaponPrefix(Item item);

		private static bool Override_WeaponPrefix(OrigWeaponPrefix WeaponPrefix, Item item) {
			if (item.ModItem != null && (bool)typeof(ItemLoader).GetMethod("GeneralPrefix", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[]{item})) {
				return item.ModItem.WeaponPrefix() || (Config.Server.PrefixType == 0 && item.DamageType == DamageClass.Throwing);
			}
			return false;
		}
	}
}


