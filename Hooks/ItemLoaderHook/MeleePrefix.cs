using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace RealThrowing.Hooks.ItemLoaderHook {
	class MeleePrefix : ModSystem {
		public override void Load() {
			try { HookEndpointManager.Add(MeleePrefixMethod, Override_MeleePrefix); } catch { throw new Exception("Unable to hook into ItemLoader.MeleePrefix"); }
		}

		public override void Unload() {
			try { HookEndpointManager.Remove(MeleePrefixMethod, Override_MeleePrefix); } catch {}
		}

		static MethodInfo MeleePrefixMethod => typeof(ItemLoader).GetMethod("MeleePrefix", BindingFlags.NonPublic | BindingFlags.Static);
		delegate bool OrigMeleePrefix(Item item);

		private static bool Override_MeleePrefix(OrigMeleePrefix MeleePrefix, Item item) {
			if (item.ModItem != null && (bool)typeof(ItemLoader).GetMethod("GeneralPrefix", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[]{item})) {
				return item.ModItem.MeleePrefix() || (Config.Server.PrefixType == 1 && item.DamageType == DamageClass.Throwing);
			}
			return false;
		}
	}
}


