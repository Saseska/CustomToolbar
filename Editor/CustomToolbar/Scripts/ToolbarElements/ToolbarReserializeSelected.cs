﻿using System;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	[Serializable]
	internal class ToolbarReserializeSelected : BaseToolbarElement
	{
		private static GUIContent reserializeSelectedBtn;

		public override string NameInList   => "[Button] Reserialize selected";
		public override int    SortingGroup => 5;

		public override void Init()
		{
			reserializeSelectedBtn = EditorGUIUtility.IconContent("Refresh");
			reserializeSelectedBtn.tooltip = "Reserialize Selected Assets";
		}

		protected override void OnDrawInList(Rect position)
		{

		}

		protected override void OnDrawInToolbar()
		{
			if (GUILayout.Button(reserializeSelectedBtn, ToolbarStyles.commandButtonStyle))
			{
				ForceReserializeAssetsUtils.ForceReserializeSelectedAssets();
			}
		}
	}
}