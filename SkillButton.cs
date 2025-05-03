using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillButton : Button
{
    public string skillId;
    public string skillName;
    public Image iconImage;
    public TextMeshProUGUI buttonText; //TextMeshPro для отображения имени скилла (если нужно)

    private Color defaultColor = Color.white;
    private Color canLearnColor = Color.green;
    private Color learnedColor = Color.blue;
    private Color notAvailableColor = Color.gray; // Цвет, когда скилл недоступен

    //Используем override для Start(), чтобы избежать ошибок, если Button.Start() не вызывается.
    protected override void Start()
    {
        base.Start(); // Важно вызвать базовый Start, если вы его переопределяете
        if (iconImage == null)
        {
            iconImage = GetComponent<Image>(); //Ищем Image на этом же GameObject
            if (iconImage == null)
            {
                Debug.LogError("SkillButton: Icon Image not found!");
            }
        }
        if (buttonText != null)
        {
            buttonText.text = skillName; // Устанавливаем текст на кнопке
        }
    }

    public void SetIcon(Sprite icon)
    {
        if (iconImage != null)
        {
            iconImage.sprite = icon;
        }
    }

    public void SetCanLearn(bool canLearn)
    {
        if (iconImage != null)
        {
            if (canLearn)
            {
                iconImage.color = canLearnColor;
                interactable = true; //Кнопка активна
            }
            else
            {
                //Проверяем, изучен ли скилл, прежде чем устанавливать цвет "недоступен"
                if(SkillTreeManager.Instance.learnedSkills.Contains(skillId))
                {
                    SetLearned(true); //Если изучен - устанавливаем цвет "изучен"
                }
                else
                {
                   iconImage.color = notAvailableColor;
                   interactable = false; //Кнопка неактивна
                }
            }
        }
    }

    public void SetLearned(bool learned)
    {
        if (iconImage != null)
        {
            if (learned)
            {
                iconImage.color = learnedColor;
                interactable = false; //Кнопка становится неактивной после изучения
            }
            else
            {
                //Если скилл не изучен, возвращаем цвет по умолчанию или "доступен для изучения"
                if(SkillTreeManager.Instance.CanLearnSkill(skillId))
                {
                    SetCanLearn(true);
                }
                else
                {
                    iconImage.color = defaultColor;
                }
            }
        }
    }
}
