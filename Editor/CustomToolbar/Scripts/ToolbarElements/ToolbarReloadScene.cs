using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender
{
	[Serializable]
	internal class ToolbarReloadScene : BaseToolbarElement
	{
		private static GUIContent reloadSceneBtn;

		public override string NameInList   => "[Button] Reload scene";
		public override int    SortingGroup => 3;

		public override void Init()
		{
			reloadSceneBtn = EditorGUIUtility.IconContent("d_RotateTool@2x");
			reloadSceneBtn.tooltip= "Reload scene";
		}

		protected override void OnDrawInList(Rect position)
		{

		}

		protected override void OnDrawInToolbar()
		{
			EditorGUIUtility.SetIconSize(new Vector2(17, 17));
			if (GUILayout.Button(reloadSceneBtn, ToolbarStyles.commandButtonStyle))
			{
				if (EditorApplication.isPlaying)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else
				{
					EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
					EditorSceneManager.OpenScene(SceneManager.GetActiveScene().path);
				}
			}
		}
	}
}