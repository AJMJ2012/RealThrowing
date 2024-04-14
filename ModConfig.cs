using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ID;

// Default Color: 73, 94, 171
// Hue shift to get different colours.

namespace RealThrowing {
	public class ServerConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;
		public static ServerConfig Instance;

		[Header("Prefixes")]

		[Range(0, 2)]
		[DefaultValue(2)]
		[Slider]
		public int PrefixType = 2;

		[Header("AutomaticConversions")]

		[BackgroundColor(171, 76, 73)]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ConvertFlails = true;
		[BackgroundColor(171, 76, 73)]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ConvertYoyos = true;
		[BackgroundColor(171, 76, 73)]
		[ReloadRequired]
		[DefaultValue(false)]
		public bool ToRanged = false;

		[Header("IgnoredWeapons")]

		public IgnoredListsSection IgnoredLists = new IgnoredListsSection();
		public class IgnoredListsSection {
			[BackgroundColor(171, 150, 73)]
			[JsonIgnore]
			public bool ReadMe = true;
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Item")]
			public List<string> Weapons = new List<string> {};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			public List<string> WeaponUseStyles = new List<string> {};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Projectile")]
			public List<string> WeaponProjectiles = new List<string> {};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			public List<string> WeaponProjectileAIStyles = new List<string> {};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Class")]
			public List<string> WeaponClasses = new List<string> {};
		}
		[JsonIgnore]
		[SeparatePage]
		public HardIgnoredListsSection HardIgnoredLists = new HardIgnoredListsSection();
		public class HardIgnoredListsSection {
			[BackgroundColor(171, 150, 73)]
			[JsonIgnore]
			public bool ReadMe = true;
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> Weapons => new List<string> {
				"Liber.PalmAxe",
			};
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> WeaponUseStyles => new List<string> {};
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> WeaponProjectiles => new List<string> {};
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> WeaponProjectileAIStyles => new List<string> {
				"Spear",
				"Drill",
				"HeldProjectile",
				"SleepyOctopod",
				"ForwardStab",
				"ShortSword",
				"FirstFractal",
				"Zenith",
				"Powder",
			};
			[BackgroundColor(73, 73, 73)]
			[JsonIgnore]
			public List<string> WeaponClasses = new List<string> {};
		}

		[Header("ForcedWeapons")]

		public ForcedWeaponsListsSection ForcedWeaponsLists = new ForcedWeaponsListsSection();
		public class ForcedWeaponsListsSection {
			[BackgroundColor(171, 150, 73)]
			[JsonIgnore]
			public bool ReadMe = true;
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Item")]
			public List<string> Throwing = new List<string> {
				"Terraria.MagicDagger",
				"Terraria.FlyingKnife",
				"PrimeRework.LaserStar",
				"PrimeRework.SwarmGrenade",
				"SpiritMod.OakHeart",
				"SpiritMod.VineChain",
				"SpiritMod.Florang",
				"SpiritMod.DuskfeatherDagger",
				"SpiritMod.GtechGrenade",
				"StormDiversMod.AncientKnives",
				"StormDiversMod.BeetleShellWeapon",
				"StormDiversMod.LizardSpinner",
				"StormDiversMod.MagicArrow",
				"StormDiversMod.TurtleShellWeapon",
			};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Item")]
			public List<string> Flail = new List<string> {
				"SpiritMod.MagnetFlail",
			};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Item")]
			public List<string> Yoyo = new List<string> {
				"ModLoader.AprilFools",
			};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Class.*")]
			public List<string> WeaponClasses = new List<string> {
				"SpiritMod.Items.Weapon.Thrown.*",
				"SpiritMod.Items.Sets.Explosives.Thrown.*",
				"Bangarang.Content.Items.Weapons.*",
				"Liber.Content.Items.Weapons.Throwing.*",
				"SpiritMod.Items.Sets.ThrownMisc.*",
			};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Class.*")]
			public List<string> FlailClasses = new List<string> {
				"SpiritMod.Items.Sets.FlailsMisc.*",
				"SpiritMod.Items.Weapon.Flails.*",
			};
			[BackgroundColor(171, 76, 73)]
			[ReloadRequired]
			[DefaultListValue("Mod.Class.*")]
			public List<string> YoyoClasses = new List<string> {
				"SpiritMod.Items.Weapon.Yoyo.*",
			};
		}

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message) {
			return DALib.Auth.IsAdmin(whoAmI, ref message);
		}

		internal CombinedIgnoredListsSection CombinedIgnoredLists = new CombinedIgnoredListsSection();
		internal class CombinedIgnoredListsSection {
			internal List<string> Weapons => Instance.IgnoredLists.Weapons.Concat(Instance.HardIgnoredLists.Weapons).ToList();
			internal List<string> WeaponUseStyles => Instance.IgnoredLists.WeaponUseStyles.Concat(Instance.HardIgnoredLists.WeaponUseStyles).ToList();
			internal List<string> WeaponProjectiles => Instance.IgnoredLists.WeaponProjectiles.Concat(Instance.HardIgnoredLists.WeaponProjectiles).ToList();
			internal List<string> WeaponProjectileAIStyles => Instance.IgnoredLists.WeaponProjectileAIStyles.Concat(Instance.HardIgnoredLists.WeaponProjectileAIStyles).ToList();
			internal List<string> WeaponClasses => Instance.IgnoredLists.WeaponClasses.Concat(Instance.HardIgnoredLists.WeaponClasses).ToList();
		}
	}

	public static class Config {
		public static ServerConfig Server => ServerConfig.Instance;
	}
}
