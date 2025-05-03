using System;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public string skillName;
    [TextArea(3, 10)]
    public string description;
    public Sprite icon;
    public int cost; // Стоимость в очках навыков (skill points)

    public string skillId; // Уникальный ID скилла (генерируется автоматически)
    public string[] requiredSkills; // ID скиллов, необходимых для изучения этого скилла
    public SkillEffectType effectType; // Тип эффекта (увеличение урона, скорости и т.д.)
    public float effectValue; // Значение эффекта

    public enum SkillEffectType
    {
        None,
        DamageIncrease,
        SpeedIncrease,
        HealthIncrease,
        CriticalChanceIncrease,
        ResourceGainIncrease,
        NewAbilityUnlock
    }

    public SkillData()
    {
        skillId = Guid.NewGuid().ToString(); // Генерируем уникальный ID
    }
}
