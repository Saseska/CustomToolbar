﻿using System;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	[Serializable]
	internal class ToolbarEnterPlayMode : BaseToolbarElement
	{
#if UNITY_2019_3_OR_NEWER
		int      selectedEnterPlayMode;
		string[] enterPlayModeOption;
#endif

		public override string NameInList   => "[Dropdown] Enter play mode option";
		public override int    SortingGroup => 2;

		public override void Init()
		{
			enterPlayModeOption = new[]
			{
				"Disabled",
				"Reload All",
				"Reload Scene",
				"Reload Domain",
				"FastMode",
			};
		}

		protected override void OnDrawInList(Rect position)
		{

		}

		protected override void OnDrawInToolbar()
		{
#if UNITY_2019_3_OR_NEWER
			if (EditorSettings.enterPlayModeOptionsEnabled)
			{
				EnterPlayModeOptions option = EditorSettings.enterPlayModeOptions;
				selectedEnterPlayMode = (int)option + 1;
			}

			selectedEnterPlayMode = EditorGUILayout.Popup(selectedEnterPlayMode, enterPlayModeOption,
				GUILayout.Width(WidthInToolbar));

			if (GUI.changed && 0 <= selectedEnterPlayMode && selectedEnterPlayMode < enterPlayModeOption.Length)
			{
				EditorSettings.enterPlayModeOptionsEnabled = selectedEnterPlayMode != 0;
				EditorSettings.enterPlayModeOptions = (EnterPlayModeOptions)(selectedEnterPlayMode - 1);
			}
#endif
		}
	}
}