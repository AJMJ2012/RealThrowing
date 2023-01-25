using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace RealThrowing.Hooks.ItemLoaderHook {
	class RangedPrefix : ModSystem {
		public override void Load() {
			try { HookEndpointManager.Add(RangedPrefixMethod, Override_RangedPrefix); } catch { throw new Exception("Unable to hook into ItemLoader.RangedPrefix"); }
		}

		public override void Unload() {
			try { HookEndpointManager.Remove(RangedPrefixMethod, Override_RangedPrefix); } catch {}
		}

		static MethodInfo RangedPrefixMethod => typeof(ItemLoader).GetMethod("RangedPrefix", BindingFlags.NonPublic | BindingFlags.Static);
		delegate bool OrigRangedPrefix(Item item);

		private static bool Override_RangedPrefix(OrigRangedPrefix RangedPrefix, Item item) {
			if (item.ModItem != null && (bool)typeof(ItemLoader).GetMethod("GeneralPrefix", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[]{item})) {
				return item.ModItem.RangedPrefix() || (Config.Server.PrefixType == 2 && item.DamageType == DamageClass.Throwing);
			}
			return false;
		}
	}
}


