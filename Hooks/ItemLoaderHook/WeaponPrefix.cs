using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace RealThrowing.Hooks.ItemLoaderHook; 
	class WeaponPrefix : ModSystem {
		public override void Load() {
			try { MonoModHooks.Add(WeaponPrefixMethod, Override_WeaponPrefix); } catch { throw new Exception("Unable to hook into ItemLoader.WeaponPrefix"); }
		}

		static MethodInfo WeaponPrefixMethod => typeof(ItemLoader).GetMethod("WeaponPrefix", BindingFlags.NonPublic | BindingFlags.Static);
		delegate bool OrigWeaponPrefix(Item item);

		private static bool Override_WeaponPrefix(OrigWeaponPrefix WeaponPrefix, Item item) {
			if (item.ModItem != null) {
				return item.ModItem.WeaponPrefix() || (!Functions.CheckMods() && Config.Server.GenericPrefixes && item.DamageType == DamageClass.Throwing);
			}
			return false;
		}
	}


