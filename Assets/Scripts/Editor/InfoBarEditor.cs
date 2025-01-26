using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    [CustomEditor(typeof(InfoBar))]
    public class InfoBarEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            InfoBar infoBar = (InfoBar)target;
            serializedObject.Update();

            infoBar.MinValue = EditorGUILayout.IntField("Min Value", infoBar.MinValue);
            infoBar.MaxValue = EditorGUILayout.IntField("Max Value", infoBar.MaxValue);
            infoBar.UseWholeNumbers = EditorGUILayout.Toggle("Use Whole Numbers", infoBar.UseWholeNumbers);
            infoBar.Value = EditorGUILayout.Slider("Value", infoBar.Value, infoBar.MinValue, infoBar.MaxValue);
            infoBar.BaseColor = EditorGUILayout.ColorField("Base Color", infoBar.BaseColor);
            infoBar.FillSlider = EditorGUILayout.ObjectField("Fill Slider", infoBar.FillSlider, typeof(Slider), true) as Slider;
            EditorGUILayout.Space();
            
            infoBar.UseDynamicColors = EditorGUILayout.Toggle("Use Dynamic Colors", infoBar.UseDynamicColors);
            if(infoBar.UseDynamicColors)
            {
                infoBar.WarningColor = EditorGUILayout.ColorField("Warning Color", infoBar.WarningColor);
                infoBar.CriticalColor = EditorGUILayout.ColorField("Critical Color", infoBar.CriticalColor);
                
                infoBar.WarningThreshold = EditorGUILayout.Slider("Warning Threshold", infoBar.WarningThreshold, 0, 1);
                infoBar.CriticalThreshold = EditorGUILayout.Slider("Critical Threshold", infoBar.CriticalThreshold, 0, 1);
            }
            EditorGUILayout.Space();
            
            infoBar.UseEaseSlider = EditorGUILayout.Toggle("Use Ease Slider", infoBar.UseEaseSlider);
            if(infoBar.UseEaseSlider)
            {
                infoBar.EaseColor = EditorGUILayout.ColorField("Ease Color", infoBar.EaseColor);
                infoBar.EaseTime = EditorGUILayout.FloatField("Ease Time", infoBar.EaseTime);
                infoBar.EaseSlider = EditorGUILayout.ObjectField("Ease Slider", infoBar.EaseSlider, typeof(Slider), true) as Slider;
            }
            EditorGUILayout.Space();

            infoBar.UseDisplayText = EditorGUILayout.Toggle("Use Display Text", infoBar.UseDisplayText);
            if(infoBar.UseDisplayText)
            {
                infoBar.DisplayText = EditorGUILayout.ObjectField("Display Text", infoBar.DisplayText, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}