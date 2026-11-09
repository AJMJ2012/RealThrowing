using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria.Localization;
using Terraria.ModLoader.Config;

// Default Color: 73, 94, 171
// Hue shift to get different colours.

// I would love to use ItemDefinition lists but it seems certain mods will crash the game if used from the main menu

namespace RealThrowing;

public class ServerConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;
	public static ServerConfig Instance;

	[Header("AutomaticConversions")]

	//[ReloadRequired]
	[DefaultValue(true)]
	public bool ConvertThrown = true;

	//[ReloadRequired]
	[DefaultValue(true)]
	public bool ConvertFlails = true;

	//[ReloadRequired]
	[DefaultValue(true)]
	public bool ConvertYoyos = true;

	[Header("ModIntegration")]

	//[ReloadRequired]
	[DefaultValue(true)]
	public bool ConvertSplitRaquets = true;

	[Header("DamageType")]

	//[ReloadRequired]
	[DefaultValue(DamageTypes.Throwing)]
	public DamageTypes ToType = DamageTypes.Throwing;

	[Header("Prefixes")]

	[DefaultValue(true)]
	public bool GenericPrefixes = true;

	[DefaultValue(false)]
	public bool RangedPrefixes = false;

	[DefaultValue(false)]
	public bool MeleePrefixes = false;

	[Header("Forced")]

	[Expand(false)]
	[BackgroundColor(73, 171, 94)]
	public ForcedListsSection ForcedLists = new ForcedListsSection();
	public class ForcedListsSection
	{

		[Header("Info")]
		[BackgroundColor(94, 73, 171)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public bool ReadMe = true;

		[Header("Throwing")]

		//[ReloadRequired]
		[DefaultListValue("Mod.Item")]
		public List<string> Thrown = new List<string> { };

		//[ReloadRequired]
		[DefaultListValue("Mod.Class.*")]
		public List<string> ThrownClasses = new List<string> { };

		[Header("Flails")]

		//[ReloadRequired]
		[DefaultListValue("Mod.Item")]
		public List<string> Flail = new List<string> { };

		//[ReloadRequired]
		[DefaultListValue("Mod.Class.*")]
		public List<string> FlailClasses = new List<string> { };

		[Header("Yoyos")]

		//[ReloadRequired]
		[DefaultListValue("Mod.Item")]
		public List<string> Yoyo = new List<string> { };

		//[ReloadRequired]
		[DefaultListValue("Mod.Class.*")]
		public List<string> YoyoClasses = new List<string> { };
	}

	[Expand(false)]
	[BackgroundColor(127, 127, 127)]
	[JsonIgnore]
	[ShowDespiteJsonIgnore]
	public HardForcedListsSection HardForcedLists = new HardForcedListsSection();
	public class HardForcedListsSection
	{

		[Header("Info")]
		[BackgroundColor(94, 73, 171)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public bool ReadMe = true;

		[Header("Thrown")]

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] Thrown => [
			"CalamityFables.ObsidianThrowingDagger",
				"CalamityFables.SpikedBall",
				"CalamityFables.TornElectrosac",
				"PrimeRework.LaserStar",
				"PrimeRework.SwarmGrenade",
				"SpiritMod.DuskfeatherDagger",
				"SpiritMod.Florang",
				"SpiritMod.GtechGrenade",
				"SpiritMod.OakHeart",
				"SpiritMod.VineChain",
				"Split.FireSalts",
				"Split.MemorySoapBar",
				"Spooky.AntSpiderBoomerang",
				"Spooky.BoomerangFish",
				"Spooky.BoomerangFishMetal",
				"Spooky.ElGourdo",
				"Spooky.GlowBulb",
				"Spooky.JackPack",
				"Spooky.JacksBag",
				"Spooky.MarbleJar",
				"Spooky.OrbWeaverBoomerang",
				"Spooky.Scycler",
				"Spooky.SentientPaladinsHammer",
				"Spooky.TarBomb",
				"Spooky.ToiletPaper",
				"Spooky.WitchesBrew",
				"StormDiversMod.AncientKnives",
				"StormDiversMod.BeetleShellWeapon",
				"StormDiversMod.LizardSpinner",
				"StormDiversMod.MagicArrow",
				"StormDiversMod.TurtleShellWeapon",
				"Terraria.MoonGlobe",
				"Terraria.PaperAirplaneA",
				"Terraria.PaperAirplaneB",
				"TRAE.ShellSpinner",
				"TRAEBossRework.ShellSpinner",
			];

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] ThrownClasses => [
			"Bangarang.Content.Items.Weapons.*",
				"Liber.Content.Items.Weapons.Throwing.*",
				"SpiritMod.Items.Sets.Explosives.Thrown.*",
				"SpiritMod.Items.Sets.ThrownMisc.*",
				"SpiritMod.Items.Weapon.Thrown.*",
			];

		[Header("Flails")]

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] Flail => [
			"CalamityFables.CloudInAMace",
				"RemasteredBosses.GlacialCrash",
				"SpiritMod.MagnetFlail",
				"Spooky.BoogerFlail",
				"Spooky.EyeFlail",
				"Spooky.GourdFlail",
				"Spooky.PufferfishFlail",
				"Spooky.PufferfishFlailMetal",
				"Spooky.ShroomFlail",
				"Spooky.SphiderFlail",
				"Spooky.SpiderWarFlail",
				"Spooky.SpookFishronFlail",
			];

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] FlailClasses => [
			"SpiritMod.Items.Sets.FlailsMisc.*",
				"SpiritMod.Items.Weapon.Flails.*",
			];

		[Header("Yoyos")]

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] Yoyo => [
			"ModLoader.AprilFools",
			];

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] YoyoClasses => [
			"SpiritMod.Items.Weapon.Yoyo.*",
			];
	}

	[Header("Ignored")]

	[Expand(false)]
	[BackgroundColor(171, 94, 73)]
	public IgnoredListsSection IgnoredLists = new IgnoredListsSection();
	public class IgnoredListsSection
	{

		[Header("Info")]
		[BackgroundColor(94, 73, 171)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public bool ReadMe = true;

		[Header("Weapons")]

		//[ReloadRequired]
		[DefaultListValue("Mod.Item")]
		public List<string> Weapons = new List<string> { };

		//[ReloadRequired]
		[DefaultListValue("Mod.Class")]
		public List<string> WeaponClasses = new List<string> { };

		[Header("Projectiles")]

		//[ReloadRequired]
		[DefaultListValue("Mod.Projectile")]
		public List<string> WeaponProjectiles = new List<string> { };

		//[ReloadRequired]
		[DefaultListValue("Mod.Class")]
		public List<string> WeaponProjectileClasses = new List<string> { };
	}

	[Expand(false)]
	[BackgroundColor(127, 127, 127)]
	[JsonIgnore]
	[ShowDespiteJsonIgnore]
	public HardIgnoredListsSection HardIgnoredLists = new HardIgnoredListsSection();
	public class HardIgnoredListsSection
	{

		[Header("Info")]
		[BackgroundColor(94, 73, 171)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public bool ReadMe = true;

		[Header("Weapons")]

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] Weapons => [
			"Liber.PalmAxe",
				"SpiritReforged.Blunderbuss",
				"SpiritReforged.HuntingRifle",
				"Split.Monsoon",
				"Split.Musketron",
				"Split.Ragnarok",
				"Split.ReflexBow",
				"Split.WeatherRocket",
			];

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] WeaponClasses => [
			"MetroidMod.*",
			];

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] WeaponUseStyles => [];

		[Header("Projectiles")]

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] WeaponProjectiles => [];

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] WeaponProjectileClasses => [];

		[BackgroundColor(127, 127, 127)]
		[JsonIgnore]
		[ShowDespiteJsonIgnore]
		public string[] WeaponProjectileAIStyles => [
			"Spear",
				"Drill",
				"HeldProjectile",
				"SleepyOctopod",
				"ForwardStab",
				"ShortSword",
				"FirstFractal",
				"Zenith",
				"Powder",
			];
	}

	public override void OnChanged()
	{
		ForcedLists.ReadMe = true;
		HardForcedLists.ReadMe = true;
		IgnoredLists.ReadMe = true;
		HardIgnoredLists.ReadMe = true;
	}
	public override void OnLoaded()
	{
		ForcedLists.ReadMe = true;
		HardForcedLists.ReadMe = true;
		IgnoredLists.ReadMe = true;
		HardIgnoredLists.ReadMe = true;
	}

	public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
	{
		return DALib.Auth.IsAdmin(whoAmI, ref message);
	}

	internal CombinedIgnoredListsSection CombinedIgnoredLists = new CombinedIgnoredListsSection();
	internal class CombinedIgnoredListsSection
	{
		internal List<string> Weapons => Instance.IgnoredLists.Weapons.Concat(Instance.HardIgnoredLists.Weapons).ToList();
		internal List<string> WeaponClasses => Instance.IgnoredLists.WeaponClasses.Concat(Instance.HardIgnoredLists.WeaponClasses).ToList();
		internal List<string> WeaponProjectiles => Instance.IgnoredLists.WeaponProjectiles.Concat(Instance.HardIgnoredLists.WeaponProjectiles).ToList();
		internal List<string> WeaponProjectileClasses => Instance.IgnoredLists.WeaponProjectileClasses.Concat(Instance.HardIgnoredLists.WeaponProjectileClasses).ToList();
	}

	internal CombinedForcedListsSection CombinedForcedLists = new CombinedForcedListsSection();
	internal class CombinedForcedListsSection
	{
		internal List<string> Thrown => Instance.ForcedLists.Thrown.Concat(Instance.HardForcedLists.Thrown).ToList();
		internal List<string> ThrownClasses => Instance.ForcedLists.ThrownClasses.Concat(Instance.HardForcedLists.ThrownClasses).ToList();
		internal List<string> Flail => Instance.ForcedLists.Flail.Concat(Instance.HardForcedLists.Flail).ToList();
		internal List<string> FlailClasses => Instance.ForcedLists.FlailClasses.Concat(Instance.HardForcedLists.FlailClasses).ToList();
		internal List<string> Yoyo => Instance.ForcedLists.Yoyo.Concat(Instance.HardForcedLists.Yoyo).ToList();
		internal List<string> YoyoClasses => Instance.ForcedLists.YoyoClasses.Concat(Instance.HardForcedLists.YoyoClasses).ToList();
	}
}

public static class Config
{
	public static ServerConfig Server => ServerConfig.Instance;
}
