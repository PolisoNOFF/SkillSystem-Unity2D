using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager Instance; // Singleton для доступа из любого места

    public List<SkillData> availableSkills = new List<SkillData>(); // Список всех доступных скиллов
    public List<string> learnedSkills = new List<string>(); // Список ID изученных скиллов

    public int skillPoints = 0; // Доступные очки навыков у игрока

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanLearnSkill(string skillId)
    {
        SkillData skill = availableSkills.FirstOrDefault(s => s.skillId == skillId);
        if (skill == null) return false; // Скилл не найден

        if (learnedSkills.Contains(skillId)) return false; // Скилл уже изучен

        if (skillPoints < skill.cost) return false; // Недостаточно очков навыков

        // Проверяем, изучены ли необходимые скиллы
        foreach (string requiredSkillId in skill.requiredSkills)
        {
            if (!learnedSkills.Contains(requiredSkillId)) return false; // Необходимый скилл не изучен
        }

        return true;
    }

    public void LearnSkill(string skillId)
    {
        if (CanLearnSkill(skillId))
        {
            SkillData skill = availableSkills.FirstOrDefault(s => s.skillId == skillId);
            if (skill != null)
            {
                learnedSkills.Add(skillId);
                skillPoints -= skill.cost;

                ApplySkillEffect(skill); // Применяем эффект скилла к игроку
                Debug.Log("Learned skill: " + skill.skillName);

                // Обновляем UI (вызовите метод UI, чтобы обновить отображение дерева)
                if(SkillTreeUI.Instance != null)
                    SkillTreeUI.Instance.UpdateUI();
            }
        }
        else
        {
            Debug.LogWarning("Cannot learn skill: " + skillId);
        }
    }

    void ApplySkillEffect(SkillData skill)
    {
        // Здесь нужно реализовать применение эффекта к игроку
        // Это может быть изменение параметров персонажа (урон, скорость),
        // разблокировка новых способностей и т.д.
        switch (skill.effectType)
        {
            case SkillData.SkillEffectType.DamageIncrease:
                // Пример: Увеличиваем урон игрока
                // player.damage += skill.effectValue;
                Debug.Log("Damage Increased by " + skill.effectValue);
                break;
            case SkillData.SkillEffectType.SpeedIncrease:
                // player.speed += skill.effectValue;
                Debug.Log("Speed Increased by " + skill.effectValue);
                break;
            case SkillData.SkillEffectType.NewAbilityUnlock:
                // player.UnlockAbility(skill.effectValue); // Предположим, что effectValue - ID способности
                Debug.Log("Ability Unlocked: " + skill.effectValue);
                break;
            // ... Добавьте обработку других типов эффектов
            case SkillData.SkillEffectType.None:
                // Do Nothing
                break;
            default:
                Debug.LogWarning("Unknown Skill Effect Type");
                break;
        }
    }

    //Метод для получения SkillData по ID
    public SkillData GetSkillData(string skillId)
    {
        return availableSkills.FirstOrDefault(s => s.skillId == skillId);
    }

    //Метод для добавления скиллпоинтов
    public void AddSkillPoints(int amount)
    {
        skillPoints += amount;
        if (SkillTreeUI.Instance != null)
            SkillTreeUI.Instance.UpdateUI(); //Обновляем UI чтобы показать изменения
    }
}
