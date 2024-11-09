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
                    new Question("If all apples are fruits, and all fruits grow on trees, are all apples growing on trees?",
                        new[] { "Yes", "No" }, 0),
                    new Question("A is taller than B. B is taller than C. Who is the tallest?",
                        new[] { "A", "B", "C", "Not enough information" }, 0)
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
                Console.WriteLine(question.Text);
                for (int i = 0; i < question.Options.Length; i++)
                    Console.WriteLine($"{i + 1}: {question.Options[i]}");

                Console.Write("Your answer (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int answer) && answer - 1 == question.AnswerIndex)
                    scores[category.Key]++;
            }
        }

        // Calculate overall score and level
        int totalScore = 0;
        foreach (var score in scores.Values) totalScore += score;

        string level = DetermineLevel(totalScore);
        Console.WriteLine($"\nYour Total Score: {totalScore}");
        Console.WriteLine($"Your Level: {level}");

        // AI Recommendations
        Console.WriteLine("\nAI Recommendations:");
        foreach (var category in scores)
        {
            Console.WriteLine($"{category.Key}: {GetRecommendation(category.Value)}");
        }
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
// Features
//Question Structure:

//Each question has text, multiple options, and a correct answer index.
//Questions are stored in a dictionary categorized by reasoning type.
//Scoring:

//User answers are scored for each category.
//A total score determines the level (Beginner to Expert).
//AI Recommendations:

//The AI provides tailored recommendations based on scores per category.
//Expandability:

//Add more questions or integrate external question sources.
//Extend recommendations based on specific academic topics.
