using System;
using System.Media;
using System.Text.RegularExpressions;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using Humanizer;
using System.CodeDom.Compiler;

namespace Voice_calculator_console
{
    internal class Program
    {
        private static SpeechRecognitionEngine recognizer;
        private static SpeechSynthesizer synthesizer;
        private static Choices words;
        private static Grammar grammar;
        private static GrammarBuilder grammarBuilder;

        private static string equation;
        private static bool shouldExit = false;

        private static void InitializeRecognitionObjects()
        {
            recognizer = new SpeechRecognitionEngine();
            synthesizer = new SpeechSynthesizer();
            words = new Choices("0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "plus", "dodać", "dodaj", "razy", "mnożyć", "podziel", "podzielić", "przez",
                "dziel", "odjąć", "odejmij", "minus", "wykonaj", "wyczyść", "pomoc");
            grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(words);
            grammar = new Grammar(grammarBuilder);

            equation = "";

        }

        private static void UpdateEquation(string text)
        {
            if (Regex.IsMatch(text, "^[0-9]$"))
            {
                equation += text;
            }
            else
            {
                switch (text)
                {
                    case "dodaj":
                    case "dodać":
                    case "plus":
                        Program.equation += " + ";
                        break;

                    case "odejmij":
                    case "odjąć":
                    case "minus":
                        Program.equation += " - ";
                        break;

                    case "pomnóż":
                    case "razy":
                    case "mnożyć":
                        Program.equation += " * ";
                        break;

                    case "podziel":
                    case "podzielić":
                    case "dziel":
                    case "przez":
                        Program.equation += " / ";
                        break;

                    case "wyczyść":
                        Program.equation = "";
                        break;
                    case "pomoc":
                        synthesizer.SpeakAsync("Wprowadzaj cyfry od 0 do 9. Możesz wprowadzić kilka pod rząd. Następnie, " +
                            "wprowadź znak równania jakie ma być wykonane: plus, minus, podziel, pomnóż. Potem wprowadź" +
                            "kolejną liczbę. Wypowiedz komendę Wykonaj aby wykonać działanie. Komenda zakończ kończy działanie" +
                            "programu.");
                        break;

                }
            }
        }

        private static void ExecuteEquation()
        {
            string[] parts = equation.Split(' ');

            if (parts.Length != 3)
            {
                synthesizer.Speak("Błąd w równaniu.");
                return;
            }

            int leftOperand, rightOperand;

            if (!int.TryParse(parts[0], out leftOperand) || !int.TryParse(parts[2], out rightOperand))
            {
                synthesizer.SpeakAsync("Błąd w równaniu.");
                return;
            }

            double result = 0;
            string operatorAsString = "", leftOperandAsString = "", rightOperandAsString = "";
            switch (parts[1])
            {
                case "+":
                    result = leftOperand + rightOperand;
                    operatorAsString = "dodać";
                    leftOperandAsString = leftOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    rightOperandAsString = rightOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    break;
                case "-":
                    result = leftOperand - rightOperand;
                    operatorAsString = "odjąć";
                    leftOperandAsString = leftOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    rightOperandAsString = rightOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    break;
                case "*":
                    result = leftOperand * rightOperand;
                    operatorAsString = "razy";
                    leftOperandAsString = leftOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    rightOperandAsString = rightOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    break;
                case "/":
                    if (rightOperand == 0)
                    {
                        synthesizer.SpeakAsync("Nie można dzielić przez zero.");
                        return;
                    }
                    result = leftOperand / rightOperand;
                    operatorAsString = "podzielić przez";
                    leftOperandAsString = leftOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    rightOperandAsString = rightOperand.ToWords(new System.Globalization.CultureInfo("pl-PL"));
                    break;
            }

            string resultToPrint = equation + " = " + result.ToString();
            equation = resultToPrint;
            synthesizer.SpeakAsync("Wynik działania " + leftOperandAsString + " " + operatorAsString + " " + rightOperandAsString + " to " + result);
        }

        static void Main(string[] args)
        {

            InitializeRecognitionObjects();
            recognizer.LoadGrammar(grammar);
            recognizer.SpeechRecognized += (s, e) =>
            {
                if (e.Result != null && e.Result.Confidence > 0.5)
                {
                    string recognizedText = e.Result.Text;
                    UpdateEquation(recognizedText);

                    if (recognizedText.ToLower() == "zakończ")
                    {
                        shouldExit = true;
                    }
                    else if (recognizedText.ToLower() == "wykonaj")
                    {
                        ExecuteEquation();
                    }


                    Console.SetCursorPosition(0, 0);
                    Console.Write(equation.PadRight(Console.WindowWidth - 1));
                }
            };
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            synthesizer.SetOutputToDefaultAudioDevice();
            

            Console.WriteLine("Inicjalizacja silnika rozpoznawania mowy zakończona powodzeniem");
            synthesizer.SpeakAsync("Inicjalizacja silnika rozpoznawania mowy zakończona powodzeniem");

            while (!shouldExit)
            {

            }
            recognizer.RecognizeAsyncStop();
        }
    }
}
