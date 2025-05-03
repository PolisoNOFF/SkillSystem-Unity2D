using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //Обязательно импортируйте TextMeshPro

public class SkillTreeUI : MonoBehaviour
{
    public static SkillTreeUI Instance; //Singleton

    public GameObject skillButtonPrefab; // Префаб кнопки скилла
    public Transform skillButtonContainer; // Объект, где будут располагаться кнопки скиллов
    public SkillTreeManager skillTreeManager; // Ссылка на SkillTreeManager

    public GameObject skillDescriptionPanel; //Панель с описанием скилла
    public TextMeshProUGUI skillNameText; //TextMeshPro для имени скилла
    public TextMeshProUGUI skillDescriptionText; //TextMeshPro для описания
    public TextMeshProUGUI skillCostText; //TextMeshPro для стоимости
    public TextMeshProUGUI skillPointsText; //TextMeshPro для отображения текущих очков навыков

    private List<GameObject> skillButtons = new List<GameObject>();
    private string selectedSkillId; // ID выбранного скилла

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        skillDescriptionPanel.SetActive(false); //Скрываем панель описания при старте
    }

    void Start()
    {
        if (skillTreeManager == null)
        {
            skillTreeManager = SkillTreeManager.Instance; //Находим SkillTreeManager, если он не был назначен в инспекторе
        }
        GenerateSkillButtons();
        UpdateUI();
    }

    // Создаем кнопки для каждого скилла
    void GenerateSkillButtons()
    {
        foreach (SkillData skill in skillTreeManager.availableSkills)
        {
            GameObject buttonGO = Instantiate(skillButtonPrefab, skillButtonContainer);
            SkillButton button = buttonGO.GetComponent<SkillButton>(); // Предполагаем, что на кнопке есть скрипт SkillButton
            if (button != null)
            {
                button.skillId = skill.skillId;
                button.skillName = skill.skillName;
                button.SetIcon(skill.icon);
                button.onClick.AddListener(() => OnSkillButtonClicked(skill.skillId));
            }
            skillButtons.Add(buttonGO);
        }
    }

    public void UpdateUI()
    {
        foreach (GameObject buttonGO in skillButtons)
        {
