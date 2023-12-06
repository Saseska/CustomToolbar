using System;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	[Serializable]
	public class ToolbarMenuItemCall : BaseToolbarElement
	{
		public override string NameInList => "[Button] MenuItem Call";

		[SerializeField] private string _menuItem;
		[SerializeField] private string _icon;
		
		private static GUIContent reloadSceneBtn;

		public override void Init()
		{
			base.Init();
			
			reloadSceneBtn = EditorGUIUtility.IconContent(_icon);
			reloadSceneBtn.tooltip= "Reload scene";
		}

		protected override void OnDrawInList(Rect position)
		{
			position.x += FieldSizeSpace;
			position.width = 200.0f;
			
			_menuItem = EditorGUI.TextField(position, _menuItem);
			
			position.x += position.width + FieldSizeSpace;
			position.width = 200.0f;
			_icon = EditorGUI.TextField(position, _icon);
		}

		protected override void OnDrawInToolbar()
		{
			if (GUILayout.Button(reloadSceneBtn, ToolbarStyles.commandButtonStyle))
			{
				EditorApplication.ExecuteMenuItem(_menuItem);
			}
		}
	}
}