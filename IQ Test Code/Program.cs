using System;
using System.Collections.Generic;

class IQTest
{
    static void Main()
    {
        // Question database
        var questions = new Dictionary<string, List<Question>>()
        {
            { "Verbal", new List<Question>
                {
                    new Question("What is the synonym of 'happy'?", new[] { "Sad", "Joyful", "Angry", "Tired" }, 1),
                    new Question("What is the antonym of 'expand'?", new[] { "Grow", "Contract", "Lengthen", "Widen" }, 1)
                }
            },
            { "Spatial", new List<Question>
                {
                    new Question("Which shape comes next in the sequence? ([Black][White][Black][White]...)", new[] { "[White]", "[Black]", "[White][White]", "[Black][Black]" }, 0),
                    new Question("What is the rotation of [Up] rotated 90° clockwise?", new[] { "[Left]", "[Down]", "[Right]", "[Up]" }, 2)
                }
            },
            { "Numerical", new List<Question>
                {
                    new Question("What is 7 + 5?", new[] { "11", "12", "13", "14" }, 1),
                    new Question("What is 8 * 6?", new[] { "42", "46", "48", "50" }, 2)
                }
            },
            { "Logical", new List<Question>
                {
                    new Question("If all apples are fruits, and all fruits grow on trees, are all apples growing on trees?", new[] { "Yes", "No" }, 0),
                    new Question("A is taller than B. B is taller than C. Who is the tallest?", new[] { "A", "B", "C", "Not enough information" }, 0)
                }
            },
            { "Visual", new List<Question>
                {
                    new Question("Which pattern completes the series?", new[] { "Pattern A", "Pattern B", "Pattern C", "Pattern D" }, 3),
                    new Question("Which object is different?", new[] { "Object 1", "Object 2", "Object 3", "Object 4" }, 2)
                }
            }
        };

        var scores = new Dictionary<string, int>();
        foreach (var category in questions.Keys)
            scores[category] = 0;

        // Run the test
        Console.WriteLine("Welcome to the IQ Test! Answer the following questions:");
        foreach (var category in questions)
        {
            Console.WriteLine($"\nCategory: {category.Key}");
            foreach (var question in category.Value)
            {
                AskQuestion(question);
                int answer = GetAnswer(question);
                if (answer - 1 == question.AnswerIndex)
                {
                    scores[category.Key]++;
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine($"Wrong answer. The correct answer was: {question.Options[question.AnswerIndex]}");
                }
            }
        }

        // Calculate overall score and level
        int totalScore = CalculateTotalScore(scores);
        string level = DetermineLevel(totalScore);

        DisplayResults(totalScore, level, scores);
    }

    // Method to ask a question and display its options
    static void AskQuestion(Question question)
    {
        Console.WriteLine(question.Text);
        for (int i = 0; i < question.Options.Length; i++)
            Console.WriteLine($"{i + 1}: {question.Options[i]}");
    }

    // Method to get user answer and validate input
    static int GetAnswer(Question question)
    {
        int answer = 0;
        while (true)
        {
            Console.Write($"Your answer (1-{question.Options.Length}): ");
            if (int.TryParse(Console.ReadLine(), out answer) && answer >= 1 && answer <= question.Options.Length)
                break;
            else
                Console.WriteLine("Invalid input. Please enter a number between 1 and " + question.Options.Length + ".");
        }
        return answer;
    }

    // Method to calculate the total score from individual category scores
    static int CalculateTotalScore(Dictionary<string, int> scores)
    {
        int totalScore = 0;
        foreach (var score in scores.Values)
            totalScore += score;
        return totalScore;
    }

    // Determine the user's level based on total score
    static string DetermineLevel(int score)
    {
        if (score <= 5) return "Beginner";
        if (score <= 8) return "Basic";
        if (score <= 12) return "Intermediate";
        if (score <= 15) return "Advanced";
        return "Expert";
    }

    // Display the total score, level, and AI recommendations
    static void DisplayResults(int totalScore, string level, Dictionary<string, int> scores)
    {
        Console.WriteLine($"\nYour Total Score: {totalScore}");
        Console.WriteLine($"Your Level: {level}");

        // AI Recommendations
        Console.WriteLine("\nAI Recommendations:");
        foreach (var category in scores)
        {
            Console.WriteLine($"{category.Key}: {GetRecommendation(category.Value)}");
        }
    }

    // Generate a recommendation based on category score
    static string GetRecommendation(int score)
    {
        if (score == 0) return "Start with basic concepts in this category.";
        if (score <= 1) return "Focus on foundational exercises.";
        if (score <= 2) return "Practice intermediate-level challenges.";
        if (score <= 3) return "Engage with advanced topics.";
        return "Explore expert-level concepts and creative problem-solving.";
    }
}

// Question class
class Question
{
    public string Text { get; }
    public string[] Options { get; }
    public int AnswerIndex { get; }

    public Question(string text, string[] options, int answerIndex)
    {
        Text = text;
        Options = options;
        AnswerIndex = answerIndex;
    }
}
