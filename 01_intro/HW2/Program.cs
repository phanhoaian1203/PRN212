using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");
            
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }
            
            string filePath = args[0];
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }
            
            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");
                
                // Read the file content
                string content = File.ReadAllText(filePath);
                
                // 1. Count words
                string[] words = content
                    .Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int wordCount = words.Length;
                Console.WriteLine($"Number of words: {wordCount}");

                // 2. Count characters (with and without whitespace)
                int charCountWithSpaces = content.Length;
                int charCountWithoutSpaces = content.Count(c => !char.IsWhiteSpace(c));
                Console.WriteLine($"Characters (with spaces): {charCountWithSpaces}");
                Console.WriteLine($"Characters (without spaces): {charCountWithoutSpaces}");

                // 3. Count sentences
                int sentenceCount = content.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
                Console.WriteLine($"Number of sentences: {sentenceCount}");

                // 4. Identify most common words
                var commonWords = words
                    .Select(w => w.ToLower().Trim(new char[] { '.', ',', ';', ':', '?', '!' }))
                    .Where(w => w.Length > 2)
                    .GroupBy(w => w)
                    .OrderByDescending(g => g.Count())
                    .Take(5);

                Console.WriteLine("Top 5 common words:");
                foreach (var group in commonWords)
                {
                    Console.WriteLine($"- {group.Key}: {group.Count()} times");
                }

                // 5. Average word length
                double averageWordLength = words.Average(w => w.Length);
                Console.WriteLine($"Average word length: {averageWordLength:F2}");

                // Count lines
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}
