﻿using System;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	[Serializable]
	public abstract class BaseToolbarElement : IComparable<BaseToolbarElement>
	{
		protected static string GetPackageRootPath { get { return "Packages/com.smkplus.custom-toolbar"; } }

		public abstract string NameInList   { get; }
		public virtual  int    SortingGroup { get; }

		[SerializeField] protected bool  IsEnabled = true;
		[SerializeField] protected float WidthInToolbar;

		protected const float FieldSizeSpace      = 10.0f;
		protected const float FieldSizeSingleChar = 7.0f;
		protected const float FieldSizeWidth      = 50.0f;

		public BaseToolbarElement() : this(100.0f)
		{
			//Init();
		}

		public BaseToolbarElement(float widthInToolbar)
		{
			WidthInToolbar = widthInToolbar;
		}

		public void DrawInList(Rect position)
		{
			position.y += 2;
			position.height -= 4;

			position.x += FieldSizeSpace;
			position.width = 15.0f;
			IsEnabled = EditorGUI.Toggle(position, IsEnabled);

			position.x += position.width + FieldSizeSpace;
			position.width = 200.0f;
			EditorGUI.LabelField(position, NameInList);

			position.x += position.width + FieldSizeSpace;
			position.width = FieldSizeSingleChar * 4;
			EditorGUI.LabelField(position, "Size");

			position.x += position.width + FieldSizeSpace;
			position.width = FieldSizeWidth;
			WidthInToolbar = EditorGUI.IntField(position, (int)WidthInToolbar);

			position.x += position.width + FieldSizeSpace;

			EditorGUI.BeginDisabledGroup(!IsEnabled);
			OnDrawInList(position);
			EditorGUI.EndDisabledGroup();
		}

		public void DrawInToolbar()
		{
			if (IsEnabled)
				OnDrawInToolbar();
		}

		public virtual void Init()
		{
		}

		protected abstract void OnDrawInList(Rect position);
		protected abstract void OnDrawInToolbar();

		public int CompareTo(BaseToolbarElement other)
		{
			return SortingGroup.CompareTo(other.SortingGroup);
		}
	}
}