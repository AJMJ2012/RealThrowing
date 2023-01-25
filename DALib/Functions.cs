using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using Terraria.ModLoader;
using Terraria;

namespace RealThrowing.DALib
{
    public static class Functions {
		static Dictionary<Type, FieldInfo[]> fieldCache = new();
		public static string FindConstantName<T>(Type containingType, T value) {
			EqualityComparer<T> comparer = EqualityComparer<T>.Default;

			FieldInfo[] fields;
			if (!fieldCache.TryGetValue(containingType, out fields)) {
				fields = containingType.GetFields(BindingFlags.Static | BindingFlags.Public);
				fieldCache.TryAdd(containingType, fields);
			}

			foreach (FieldInfo field in fields) {
				if (field.FieldType == typeof(T) && comparer.Equals(value, (T) field.GetValue(null))) {
					return field.Name;
				}
			}
			return null;
		}

		public static string GetItemID(int type) {
			return GetItemMod(type) + "." + GetItemName(type);
		}
		public static string GetItemMod(int type) {
			ModItem modItem = ItemLoader.GetItem(type);
			return modItem != null ? modItem.Mod.Name : "Terraria";
		}
		public static string GetItemName(int type) {
			ModItem modItem = ItemLoader.GetItem(type);
			return modItem != null ? modItem.Name : GetVanillaItemName(type);
		}

		static Dictionary<int, string> itemTypeIDCache = new();
		internal static string GetVanillaItemName(int type) {
			string typeID = type.ToString();
			if (!itemTypeIDCache.TryGetValue(type, out typeID)) {
				typeID = FindConstantName<short>(typeof(Terraria.ID.ItemID), (short)type) ?? typeID;
				itemTypeIDCache.TryAdd(type, typeID);
			}
			return typeID;
		}
		public static string GetItemID(Item item) { return GetItemID(item.type); }

		static Dictionary<int, string> itemUseStyleIDCache = new();
		internal static string GetItemUseStyleID(int useStyle) {
			string useStyleID = useStyle.ToString();
			if (!itemUseStyleIDCache.TryGetValue(useStyle, out useStyleID)) {
				useStyleID = FindConstantName<short>(typeof(Terraria.ID.ItemUseStyleID), (short)useStyle) ?? useStyleID;
				itemUseStyleIDCache.TryAdd(useStyle, useStyleID);
			}
			return useStyleID;
		}
		public static string GetItemUseStyleID(Item item) { return GetItemUseStyleID(item.useStyle); }


		public static string GetProjectileID(int type) {
			return GetProjectileMod(type) + "." + GetProjectileName(type);
		}
		public static string GetProjectileMod(int type) {
			ModProjectile modProjectile = ProjectileLoader.GetProjectile(type);
			return modProjectile != null ? modProjectile.Mod.Name : "Terraria";
		}
		public static string GetProjectileName(int type) {
			ModProjectile modProjectile = ProjectileLoader.GetProjectile(type);
			return modProjectile != null ? modProjectile.Name : GetVanillaProjectileName(type);
		}

		static Dictionary<int, string> projectileTypeIDCache = new();
		internal static string GetVanillaProjectileName(int type) {
			string typeID = type.ToString();
			if (!projectileTypeIDCache.TryGetValue(type, out typeID)) {
				typeID = FindConstantName<short>(typeof(Terraria.ID.ProjectileID), (short)type) ?? typeID;
				projectileTypeIDCache.TryAdd(type, typeID);
			}
			return typeID;
		}
		public static string GetProjectileID(Projectile projectile) { return GetProjectileID(projectile.type); }

		static Dictionary<int, string> projectileAIStyleIDCache = new();
		public static string GetProjectileAIStyleID(int aiStyle) {
			string aiStyleID = aiStyle.ToString();
			if (!projectileAIStyleIDCache.TryGetValue(aiStyle, out aiStyleID)) {
				aiStyleID = FindConstantName<short>(typeof(Terraria.ID.ProjAIStyleID), (short)aiStyle) ?? aiStyleID;
				projectileAIStyleIDCache.TryAdd(aiStyle, aiStyleID);
			}
			return aiStyleID;
		}
		public static string GetProjectileAIStyleID(Projectile projectile) { return GetProjectileAIStyleID(projectile.aiStyle); }

		public static bool InList<T>(List<T> haystack, T needle, bool caseSensitive = false) {
			if (haystack == null || needle == null) {
				return false;
			}
			if (!caseSensitive && needle is string) {
				return haystack.Any(s => (string)s.ToString().ToLower() == (string)needle.ToString().ToLower());
			}
			else {
				return haystack.Any(s => (string)s.ToString() == (string)needle.ToString());
			}
		}
	}
}
