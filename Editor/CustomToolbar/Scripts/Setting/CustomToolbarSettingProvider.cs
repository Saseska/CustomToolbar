using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityToolbarExtender
{
	internal class CustomToolbarSettingProvider : SettingsProvider
	{
		private CustomToolbarSetting setting;

		Vector2         scrollPos;
		ReorderableList elementsList;

		private class Styles
		{
			public static readonly GUIContent minFPS   = new GUIContent("Minimum FPS");
			public static readonly GUIContent maxFPS   = new GUIContent("Maximum FPS");
			public static readonly GUIContent limitFPS = new GUIContent("Limit FPS");
		}

		public CustomToolbarSettingProvider(string path, SettingsScope scopes = SettingsScope.User) : base(
			path, scopes)
		{
		}

		public override void OnActivate(string searchContext, VisualElement rootElement)
		{
			setting = CustomToolbarSetting.Settings;
		}

		public static bool IsSettingAvailable()
		{
			return CustomToolbarSetting.Settings != null;
		}

		public override void OnGUI(string searchContext)
		{
			base.OnGUI(searchContext);

			scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

			if (elementsList == null)
			{
				elementsList = CustomToolbarReordableList.Create(setting.elements, OnMenuItemAdd);
				/*elementsList.onRemoveCallback += list =>
				{
					setting.elements.RemoveAt(elementsList.index);
					setting.Save();
					UpdateToolbar();
				};
				elementsList.onReorderCallback += list =>
				{
					UpdateToolbar();
				};*/
			}
			elementsList.DoLayoutList();

			EditorGUILayout.EndScrollView();

			if (GUI.changed) {
				ToolbarExtender.OnGUI();
                setting.Save();
                
                UpdateToolbar();
			}
		}

		private void OnMenuItemAdd(object target)
		{
			setting.elements.Add(target as BaseToolbarElement);
			setting.Save();

			//UpdateToolbar();
		}

		private void UpdateToolbar()
		{
			CustomToolbarInitializer.UpdateToolbarElements();
		}

		[SettingsProvider]
		public static SettingsProvider CreateCustomToolbarSettingProvider()
		{
			if (IsSettingAvailable())
			{
				CustomToolbarSettingProvider provider =
					new CustomToolbarSettingProvider("Project/Custom Toolbar", SettingsScope.Project)
					{
						keywords = GetSearchKeywordsFromGUIContentProperties<Styles>()
					};

				return provider;
			}

			return null;
		}
	}
}