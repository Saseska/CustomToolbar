using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;

namespace UnityToolbarExtender
{
    [InitializeOnLoad]
    public static class CustomToolbarInitializer
    {
        static CustomToolbarInitializer()
        {
            UpdateToolbarElements();
        }

        public static void UpdateToolbarElements()
        {
            CustomToolbarSetting setting = CustomToolbarSetting.Settings;
            setting.elements.ForEach(element => element.Init());
            
            List<BaseToolbarElement> leftTools = setting.elements.TakeWhile(element => !(element is ToolbarSides)).ToList();
            List<BaseToolbarElement> rightTools = setting.elements.Except(leftTools).ToList();
            IEnumerable<Action> leftToolsDrawActions = leftTools.Select(TakeDrawAction);
            IEnumerable<Action> rightToolsDrawActions = rightTools.Select(TakeDrawAction);

            ToolbarExtender.LeftToolbarGUI.Clear();
            ToolbarExtender.RightToolbarGUI.Clear();
            ToolbarExtender.LeftToolbarGUI.AddRange(leftToolsDrawActions);
            ToolbarExtender.RightToolbarGUI.AddRange(rightToolsDrawActions);
            
            InternalEditorUtility.RepaintAllViews();
        }

        private static Action TakeDrawAction(BaseToolbarElement element)
        {
            Action action = element.DrawInToolbar;
            return action;
        }
    }
}