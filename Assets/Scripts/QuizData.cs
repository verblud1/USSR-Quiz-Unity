using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class QuizData
{
    public DifficultyLevel easy;
    public DifficultyLevel medium;
    public DifficultyLevel hard;
}

[Serializable]
public class DifficultyLevel
{
    public List<Question> questions;
}

[Serializable]
public class Question
{
    public string id;
    public string text_question;
    public List<string> answer_options;
    public int correct_variant;
}