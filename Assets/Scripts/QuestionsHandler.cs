using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QuestionTextUI;
    [SerializeField] private TextMeshProUGUI HeaderTextUI;
    [SerializeField] private Transform ButtonsListUI;
    [SerializeField] private GameObject EndPanel;

    private int IndexHeader=0;
    private int index_correctAnswers = 0;
    private int currentQuestionIndex = 0;
    private QuizData data;
    private DifficultyLevel currentLevel;
    private string LevelName;

    void Start()
    {
        
        LevelName = PlayerPrefs.GetString("SelectedLevelName");
        Debug.Log($"Загружаем уровень: {LevelName}");


        LoadQuizData();
        ShowCurrentQuestion();
    }

    private void LoadQuizData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/LevelConfig");
        if (jsonFile == null)
        {
            Debug.LogError("❌ Файл не найден! Проверь путь: Assets/Resources/Data/LevelConfig.json");
            return;
        }
        Debug.Log("✅ Файл найден");

        string jsonString = jsonFile.text;
        data = JsonUtility.FromJson<QuizData>(jsonString);

        if (data == null)
        {
            Debug.LogError("❌ Ошибка парсинга JSON!");
            return;
        }
        Debug.Log("✅ JSON распарсен");

        
        switch (LevelName.ToLower())
        {
            case "easy":
                currentLevel = data.easy;
                break;
            case "medium":
                currentLevel = data.medium;
                break;
            case "hard":
                currentLevel = data.hard;
                break;
            default:
                Debug.LogError($"❌ Неизвестный уровень: {LevelName}");
                return;
        }

        if (currentLevel == null)
        {
            Debug.LogError($"❌ Уровень '{LevelName}' не найден в JSON данных!");
            return;
        }

        Debug.Log($"✅ Уровень '{LevelName}' загружен. Вопросов: {currentLevel.questions?.Count ?? 0}");
    }

    public void ShowCurrentQuestion()
    {
        // Сбрасываем цвета кнопок перед показом нового вопроса
        ResetButtonColors();

        Debug.Log($"currentLevel: {currentLevel != null}");
        Debug.Log($"questions: {currentLevel?.questions != null}");
        Debug.Log($"questions count: {currentLevel?.questions?.Count ?? 0}");
        Debug.Log($"currentQuestionIndex: {currentQuestionIndex}");

        if (currentLevel?.questions != null && currentQuestionIndex < currentLevel.questions.Count)
        {
            Question question = currentLevel.questions[currentQuestionIndex];
            QuestionTextUI.text = question.text_question;

            for (int i = 0; i < ButtonsListUI.childCount && i < question.answer_options.Count; i++)
            {
                Transform buttonTransform = ButtonsListUI.GetChild(i);
                GameObject button = buttonTransform.gameObject;
                Transform buttonTextTransform = buttonTransform.GetChild(0);

                TextMeshProUGUI textComponent = buttonTextTransform.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = question.answer_options[i];
                }

                int answerIndex = i;
                Button buttonComponent = button.GetComponent<Button>();
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(() => CheckAnswer(answerIndex, question.correct_variant));
            }

            IndexHeader = currentQuestionIndex + 1;
            HeaderTextUI.text = IndexHeader.ToString();

        }
        else
        {
            Debug.Log("Условие else сработало! Причины:");
            if (currentLevel == null) Debug.Log(" - currentLevel is null");
            else if (currentLevel.questions == null) Debug.Log(" - questions is null");
            else if (currentLevel.questions.Count == 0) Debug.Log(" - questions count is 0");
            else if (currentQuestionIndex >= currentLevel.questions.Count)
                Debug.Log($" - currentQuestionIndex {currentQuestionIndex} >= questions count {currentLevel.questions.Count}");

            EndGame();
        }
    }

    private void CheckAnswer(int selectedAnswer, int correctAnswer)
    {
        SetButtonsInteractable(false);

        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Правильно!");
            HighlightCorrectAnswer(correctAnswer);
            index_correctAnswers++;
        }
        else
        {
            Debug.Log("Неправильно!");
            HighlightAnswer(selectedAnswer, correctAnswer);
        }

        StartCoroutine(NextQuestionWithDelay());
    }

    private IEnumerator NextQuestionWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        currentQuestionIndex++;

        SetButtonsInteractable(true);
        ShowCurrentQuestion();
    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach (Transform buttonTransform in ButtonsListUI)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = interactable;
            }
        }
    }

    // 🔥 ДОБАВЛЯЕМ метод для сброса цветов кнопок
    private void ResetButtonColors()
    {
        foreach (Transform buttonTransform in ButtonsListUI)
        {
            Image image = buttonTransform.GetComponentInChildren<Image>();
            if (image != null)
            {
                image.color = Color.white; // исходный цвет
            }
        }
    }

    public void HighlightCorrectAnswer(int correctAnswer)
    {
        if (correctAnswer >= 0 && correctAnswer < ButtonsListUI.childCount)
        {
            Transform buttonTransform = ButtonsListUI.GetChild(correctAnswer);
            Image image = buttonTransform.GetComponentInChildren<Image>();
            if (image != null)
            {
                image.color = Color.green;
            }
        }
    }

    
    public void HighlightAnswer(int selectedAnswer, int correctAnswer)
    {
        // Подсвечиваем правильный ответ зеленым
        if (correctAnswer >= 0 && correctAnswer < ButtonsListUI.childCount)
        {
            Transform correctButtonTransform = ButtonsListUI.GetChild(correctAnswer);
            Image correctImage = correctButtonTransform.GetComponentInChildren<Image>();
            if (correctImage != null)
            {
                correctImage.color = Color.green;
            }
        }

        // Подсвечиваем выбранный неправильный ответ красным
        if (selectedAnswer >= 0 && selectedAnswer < ButtonsListUI.childCount)
        {
            Transform selectedButtonTransform = ButtonsListUI.GetChild(selectedAnswer);
            Image selectedImage = selectedButtonTransform.GetComponentInChildren<Image>();
            if (selectedImage != null)
            {
                selectedImage.color = Color.red;
            }
        }
    }

    private void EndGame()
    {
        Debug.Log("Игра завершена!");
        if (EndPanel != null)
        {
            EndPanel.SetActive(true);
        }

        PlayerPrefs.SetInt("CurrentValueCorrect", index_correctAnswers);

        if(index_correctAnswers > PlayerPrefs.GetInt(LevelName + "_correct"))
            PlayerPrefs.SetInt(LevelName+"_correct", index_correctAnswers);

    }

   
}