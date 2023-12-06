using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UnityToolbarExtender
{
	internal class CustomToolbarSetting
	{
		private const string SETTING_PATH = "UserSettings/Plugins/CustomToolbarSetting.json";

		private static CustomToolbarSetting _setting;

		public static CustomToolbarSetting Settings
		{
			get
			{
				if (_setting == null)
				{
					_setting = LoadOrCreateSetting();
				}

				return _setting;
			}
		}

		[SerializeReference] internal List<BaseToolbarElement> elements = new List<BaseToolbarElement>();


		private static CustomToolbarSetting LoadOrCreateSetting()
		{
			if (File.Exists(SETTING_PATH))
			{
				return JsonUtility.FromJson<CustomToolbarSetting>(File.ReadAllText(SETTING_PATH));
			}

			var setting = new CustomToolbarSetting
			{
				elements = new List<BaseToolbarElement>() { }
			};

			if (!Directory.Exists("UserSettings/Plugins"))
			{
				Directory.CreateDirectory("UserSettings/Plugins");
			}

			setting.Save();

			return setting;
		}

		internal void Save()
		{
			StreamWriter streamWriter = File.CreateText(SETTING_PATH);
			streamWriter.WriteLine(JsonUtility.ToJson(this));
			streamWriter.Flush();
			streamWriter.Close();
		}
	}
}