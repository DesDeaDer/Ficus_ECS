using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ColorSetting
{
    public int Multiple;
    public Color Color;
}

[CreateAssetMenu(fileName = "ColorSettings", menuName ="AAAAAAAAAAAAAAA")]
public class ColorSettings : ScriptableObject
{
    [SerializeField] private ColorSetting[] _colorSettings;
    [SerializeField] private Color _color;

    public IEnumerable<ColorSetting> Values => _colorSettings;
    public Color ColorDefault => _color;

}
