using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender
{
	[Serializable]
	internal class ToolbarStartFromFirstScene : BaseToolbarElement
	{
		private static GUIContent startFromFirstSceneBtn;

		public override string NameInList   => "[Button] Start from first scene";
		public override int    SortingGroup => 3;

		public override void Init()
		{
			EditorApplication.playModeStateChanged += LogPlayModeState;

			startFromFirstSceneBtn =
				new GUIContent(
					(Texture2D)AssetDatabase.LoadAssetAtPath(
						$"{GetPackageRootPath}/Editor/CustomToolbar/Icons/LookDevPlayFirst@2x.png", typeof(Texture2D)),
					"Play from " + Path.GetFileName(EditorBuildSettings.scenes[0].path));
		}

		protected override void OnDrawInList(Rect position)
		{

		}

		protected override void OnDrawInToolbar()
		{
			if (GUILayout.Button(startFromFirstSceneBtn, ToolbarStyles.commandButtonStyle))
			{
				if (!EditorApplication.isPlaying)
				{
					EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
					EditorPrefs.SetInt("LastActiveSceneToolbar", EditorSceneManager.GetActiveScene().buildIndex);
					EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
				}

				EditorApplication.isPlaying = !EditorApplication.isPlaying;
			}
		}

		private static void LogPlayModeState(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.EnteredEditMode && EditorPrefs.HasKey("LastActiveSceneToolbar"))
			{
				EditorSceneManager.OpenScene(
					SceneUtility.GetScenePathByBuildIndex(EditorPrefs.GetInt("LastActiveSceneToolbar")));
				EditorPrefs.DeleteKey("LastActiveSceneToolbar");
			}
		}
	}
}
