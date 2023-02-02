using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria.ModLoader.Config;
using Terraria.ID;

// Default Color: 73, 94, 171
// Hue shift to get different colours.

namespace RealThrowing {
	[Label("Server Config")]
	public class ServerConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;
		public static ServerConfig Instance;

		[Header("Prefixes")]
		[Label("Throwing prefix pool")]
		[Tooltip("0: Generic\n1: Melee\n2: Ranged\nNote: Doesn't seem to affect Yoyos and Flails")]
		[Range(0, 2)]
		[DefaultValue(2)]
		[Slider]
		public int PrefixType = 2;

		[Header("Automatic Conversions")]
		[Label("Convert Flails")]
		[Tooltip("Allows flails to also be converted to throwing damage")]
		[BackgroundColor(171, 76, 73)]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ConvertFlails = true;

		[Label("Convert Yoyos")]
		[Tooltip("Allows yoyos to also be converted to throwing damage")]
		[BackgroundColor(171, 76, 73)]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ConvertYoyos = true;

		[Header("Ignored Weapons")]
		[Label("Ignored Weapons")]
		[SeparatePage]
		public IgnoredListsSection IgnoredLists = new IgnoredListsSection();
		public class IgnoredListsSection {
			[Label("Read Me!")]
			[Tooltip("Some weapons may wrongfully be converted and need to be added to one of these lists")]
			[BackgroundColor(171, 150, 73)]
			[JsonIgnore]
			public bool ReadMe = true;

			[Label("Ignored Weapons by IDs")]
			[Tooltip("All weapons in this list will not be automatically converted")]
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Item")]
			public List<string> Weapons = new List<string> {};

			[Label("Ignored Weapons by Use Styles")]
			[Tooltip("All weapons with use styles in this list will not be automatically converted")]
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			public List<string> WeaponUseStyles = new List<string> {};

			[Label("Ignored Weapons by Projectiles")]
			[Tooltip("All weapons that shoot projectiles in this list will not be automatically converted")]
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Projectile")]
			public List<string> WeaponProjectiles = new List<string> {};

			[Label("Ignored Weapons by Projectile AI Styles")]
			[Tooltip("All weapons that shoot projectiles with AI styles in this list will not be automatically converted")]
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			public List<string> WeaponProjectileAIStyles = new List<string> {};
		}

		[Label("Hard Coded Ignored Weapons")]
		[SeparatePage]
		[JsonIgnore]
		public HardIgnoredListsSection HardIgnoredLists = new HardIgnoredListsSection();
		public class HardIgnoredListsSection {
			[Label("Read Me!")]
			[Tooltip("Some weapons may wrongfully be converted and have been added to these lists\nThese lists are hard coded cannot be modified")]
			[BackgroundColor(171, 150, 73)]
			[JsonIgnore]
			public bool ReadMe = true;

			[Label("Ignored Weapons by IDs")]
			[Tooltip("All weapons in this list will not be automatically converted")]
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> Weapons => new List<string>{};

			[Label("Ignored Weapon by Use Styles")]
			[Tooltip("All weapons with use styles in this list will not be automatically converted")]
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> WeaponUseStyles => new List<string> {};

			[Label("Ignored Weapons by Projectiles")]
			[Tooltip("All weapons that shoot projectiles in this list will not be automatically converted")]
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> WeaponProjectiles => new List<string> {};

			[Label("Ignored Weapons by Projectile AI Styles")]
			[Tooltip("All weapons that shoot projectiles with AI styles in this list will not be automatically converted")]
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> WeaponProjectileAIStyles => new List<string> {
				"Terraria.Spear",
				"Terraria.Drill",
				"Terraria.HeldProjectile",
				"Terraria.SleepyOctopod",
				"Terraria.ForwardStab",
				"Terraria.ShortSword",
				"Terraria.FirstFractal",
				"Terraria.Zenith",
			};
		}

		[Header("Forced Weapons")]
		[Label("Forced Weapons")]
		[SeparatePage]
		public ForcedWeaponsListsSection ForcedWeaponsLists = new ForcedWeaponsListsSection();
		public class ForcedWeaponsListsSection {
			[Label("Read Me!")]
			[Tooltip("Some weapons may not be converted and need to be added to one of these lists")]
			[BackgroundColor(171, 150, 73)]
			[JsonIgnore]
			public bool ReadMe = true;

			[Label("Forced Weapons")]
			[Tooltip("All weapons in this list will be converted to throwing")]
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Item")]
			public List<string> Throwing = new List<string> {
				"Terraria.MagicDagger",
				"Terraria.FlyingKnife",
			};
		}

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message) {
			return DALib.Auth.IsAdmin(whoAmI, ref message);
		}

		internal CombinedIgnoredListsSection CombinedIgnoredLists = new CombinedIgnoredListsSection();
		internal class CombinedIgnoredListsSection {
			internal List<string> Weapons => Instance.IgnoredLists.Weapons.Concat(Instance.HardIgnoredLists.Weapons).ToList();
			internal List<string> WeaponUseStyles => Instance.IgnoredLists.WeaponUseStyles.Concat(Instance.HardIgnoredLists.WeaponUseStyles).ToList();
			internal List<string> WeaponProjectiles => Instance.IgnoredLists.WeaponProjectiles.Concat(Instance.HardIgnoredLists.WeaponProjectiles).ToList();
			internal List<string> WeaponProjectileAIStyles => Instance.IgnoredLists.WeaponProjectileAIStyles.Concat(Instance.HardIgnoredLists.WeaponProjectileAIStyles).ToList();
		}
	}

	public static class Config {
		public static ServerConfig Server => ServerConfig.Instance;
	}
}
