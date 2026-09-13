using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Range))] 
    public class RangeDrawerEditor : PropertyDrawer
    {
        private const float minSliderValue = 0f;
        private const float maxSliderValue = 1f;
        private const float fieldWidth = 45f;
        private const float spacing = 4f;

        private float minValue = minSliderValue;
        private float maxValue = maxSliderValue;
        
        /// <summary>
        /// Draws a min max slider in the inspector
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            #region SerializedProperties
                
            SerializedProperty minProperty = property.FindPropertyRelative("min");
            SerializedProperty maxProperty = property.FindPropertyRelative("max");

            #endregion
            
            EditorGUI.BeginProperty(position, label, property);
            
                position = EditorGUI.PrefixLabel(position, label);
                SetupMinMaxSlider(position);
                
                minProperty.floatValue = minValue;
                maxProperty.floatValue = maxValue;
                
            EditorGUI.EndProperty();
        }

        private void SetupMinMaxSlider(Rect position)
        {
            Rect currentMinField = new Rect(position.x, position.y + spacing, fieldWidth, position.height);
            Rect currentMaxField = new Rect(position.x + position.width - fieldWidth, position.y, fieldWidth, position.height);
            
            EditorGUI.FloatField(currentMinField, minValue);
            EditorGUI.FloatField(currentMaxField, maxValue);
            
            Rect sliderRect = new Rect(position.x + fieldWidth + spacing, position.y + spacing,
                position.width - fieldWidth * 2 - spacing * 2, position.height);
                
            EditorGUI.MinMaxSlider(sliderRect, ref minValue, ref maxValue,
                minSliderValue, maxSliderValue);
        }
        
    }
}
