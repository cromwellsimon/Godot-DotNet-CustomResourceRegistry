using Godot;
using Godot.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotNetCustomResourceRegistry
{
	public static class Settings
	{
		public enum ResourceSearchType
		{
			Recursive = 0,
			Namespace = 1,
		}

		public static string ClassPrefix => GetSettings(nameof(ClassPrefix)).ToString();
		public static ResourceSearchType SearchType => (ResourceSearchType)((int)GetSettings(nameof(SearchType)));
		public static ReadOnlyCollection<string> ResourceScriptDirectories => new ReadOnlyCollection<string>(GetSettings(nameof(ResourceScriptDirectories)).AsStringArray());

		public static void Init()
		{
			AddSetting(nameof(ClassPrefix), Variant.Type.String, "");
			AddSetting(nameof(SearchType), Variant.Type.Int, ((int)ResourceSearchType.Recursive), PropertyHint.Enum, "Recursive,Namespace");
			AddSetting(nameof(ResourceScriptDirectories), Variant.Type.PackedStringArray, new  Array<string> (new string[]{ "res://"}));
		}

		private static Variant GetSettings(string title)
		{
			return ProjectSettings.GetSetting($"{nameof(DotNetCustomResourceRegistry)}/{title}");
		}

		private static void AddSetting(string title, Variant.Type type, Variant value, PropertyHint hint = PropertyHint.None, string hintString = "")
		{
			title = SettingPath(title);
			if (!ProjectSettings.HasSetting(title))
				ProjectSettings.SetSetting(title, value);
			var info = new Dictionary
			{
				["name"] = title,
				["type"] = ((int)type),
				["hint"] = ((int)hint),
				["hint_string"] = hintString,
			};
			ProjectSettings.AddPropertyInfo(info);
		}

		private static string SettingPath(string title) => $"{nameof(DotNetCustomResourceRegistry)}/{title}";
	}
}