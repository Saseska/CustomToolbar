using UnityEngine;

namespace UnityToolbarExtender
{
	public class ToolbarFlexibleSpace : BaseToolbarElement
	{
		public override string NameInList => $"[Empty flexible space]";
		protected override void OnDrawInList(Rect position)
		{
		}

		protected override void OnDrawInToolbar()
		{
			GUILayout.FlexibleSpace();
		}
	}
}