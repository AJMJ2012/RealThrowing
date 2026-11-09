using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace RealThrowing.Hooks.ItemLoaderHook; 
	class RangedPrefix : ModSystem {
		public override void Load() {
			try { MonoModHooks.Add(RangedPrefixMethod, Override_RangedPrefix); } catch { throw new Exception("Unable to hook into ItemLoader.RangedPrefix"); }
		}

		static MethodInfo RangedPrefixMethod => typeof(ItemLoader).GetMethod("RangedPrefix", BindingFlags.NonPublic | BindingFlags.Static);
		delegate bool OrigRangedPrefix(Item item);

		private static bool Override_RangedPrefix(OrigRangedPrefix RangedPrefix, Item item) {
			if (item.ModItem != null) {
				return item.ModItem.RangedPrefix() || (!Functions.CheckMods() && Config.Server.RangedPrefixes && item.DamageType == DamageClass.Throwing);
			}
			return false;
		}
	}


