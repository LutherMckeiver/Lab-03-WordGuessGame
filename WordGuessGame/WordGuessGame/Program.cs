using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordGuessGame
{
    public class Program

    {
        static HashSet<char> characterGuessed = new HashSet<char>();
        /// <summary>
        /// Main Method that starts the program. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            GuessingGameWelcomeMessage();
            GuessingGameMenu();
            Console.ReadLine();
        }
        
        
        /// <summary>
        /// Welcome Message Method
        /// </summary>
        static void GuessingGameWelcomeMessage()
        {
            Console.WriteLine("*************************************************");
            Console.WriteLine("***     Welcome To Luther's Guessing Game!    ***");
            Console.WriteLine("***     Please see your options below!        ***");
            Console.WriteLine("***                                           ***");
            Console.WriteLine("***     To quit at any time, type quit        ***");
            Console.WriteLine("************************************************* \n");
        }

        
        /// <summary>
        /// This method shows the game options. 
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>userInput</returns>
        public static void GuessingGameMenu()
        {
            Console.WriteLine("**************************************************");
            Console.WriteLine("***    What would you like to do today?        ***");
            Console.WriteLine("***    1. Start A New Game                     ***");
            Console.WriteLine("***    2. View All Words                       ***");
            Console.WriteLine("***    3. Add Words                            ***");
            Console.WriteLine("***    4. Remove Words                         ***");
            Console.WriteLine("***    5. Quit The Game                        ***");
            Console.WriteLine("***                                            ***");
            Console.WriteLine("***    Choose a number to continue.            ***");
            Console.WriteLine("**************************************************");
            Console.Write("> ");
            string userInput = Console.ReadLine();
            Options(userInput);
        }
        
        
        /// <summary>
        /// Game logic for starting a new game. 
        /// </summary>
        public static void StartANewGame()
        {
            CreateNewFile();
            string word = RandomWord();

            
            char[] showUnderscore = ShowUnderscoresForSelectedRandomWord(word);
            Console.WriteLine(String.Join(" ", showUnderscore));

            HashSet<char> charactersGuessed = new HashSet<char>();



            while (ContainsUnderscores(showUnderscore))
            {
                string input = GuessALetter();
                showUnderscore = GuessIfLetterInWord(showUnderscore, input, word.ToCharArray());
                Console.WriteLine(String.Join(" ", showUnderscore));
            }
            Console.WriteLine("Good game, thanks for playing. Want to play another game?");
            Console.Write("> ");
            string answer = Console.ReadLine();
            Options(answer);
        }

       
        /// <summary>
        /// This method handles the logic of which choice someone picks off the menu.
        /// </summary>
        /// <param name="userInput"></param>
        public static void Options(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    StartANewGame();
                    break;
                case "2":
                    ViewAllWords();
                    break;
                case "3":
                    Console.Write("Add your word here: ");
                    string input = Console.ReadLine();
                    AddNewWord(input);
                    break;
                case "4":
                    DeleteAWord();
                    break;
                case "5":
                    Console.WriteLine("See you later");
                    Environment.Exit(0);
                    break;
                case "yes":
                    StartANewGame();
                    break;
                case "no":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter a valid input.\n\n");
                    Console.Clear();
                    GuessingGameMenu();
                    break;
            }
        }
        
        
        /// <summary>
        /// Prompts the user, and stores data. 
        /// </summary>
        /// <returns>user input</returns>
        public static string GuessALetter()
        {


            string input = String.Empty; 
            while (input != "quit")
            {
                
                Console.WriteLine();
                Console.WriteLine("Please guess one letter in the word.");
                Console.Write("> ");
                input = Console.ReadLine();
                return input;
            }
            return input;
        }


        /// <summary>
        /// Creates a file in the path specified. And adds default words.
        /// </summary>
        /// <returns>filepath</returns>
        public static bool CreateNewFile()
        {
            string path = "../../../allWords.txt";

            if (!File.Exists(path))
            {
                try
                {
                    using (StreamWriter writer = File.AppendText(path))
                    {
                        writer.Write("tiger\ncat\nfrog\ncow\nsheep\nlion\ndragon\nfish\nshark\n");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return File.Exists(path);
        }
        
        
        /// <summary>
        /// Adds a new word to the game file. 
        /// </summary>
        /// <returns>the appended text</returns>
        public static bool AddNewWord(string input)
        {

            string path = "../../../allWords.txt";
            string appendText = input;
            try
            {
                File.AppendAllText(path, appendText + Environment.NewLine);
                GuessingGameMenu();
            }

            catch (Exception)
            {
                throw;
            }
            return File.Exists(path);
        }
       

        /// <summary>
        /// Deletes a word from the game file. 
        /// </summary>
        /// <returns></returns>
        public static string DeleteAWord()
        {
            string path = "../../../allWords.txt";
            Console.WriteLine("Here is a list of the current words: ");
            try
            {
                string[] words = File.ReadAllLines(path);

                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }

                Console.WriteLine("Which word would you like to delete?");
                Console.Write("> ");
                string input = Console.ReadLine().ToLower();

                var newFile = words.Where(word => !word.Contains(input));
                File.WriteAllLines(path, newFile);
                Console.WriteLine("You have successfully deleted: " + input + "\n");
                Console.WriteLine("Words left: ");
                foreach (string word in newFile)
                {
                    Console.WriteLine(word);
                }
                return string.Join(", ", newFile);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        
        
        /// <summary>
        /// Allows us to view all the words in the game. 
        /// </summary>
        /// <returns>String of all words in file.</returns>
        public static string ViewAllWords()
        {
            string path = "../../../allWords.txt";
            Console.WriteLine("Here is a list of the current words: ");

            try
            {
                string[] words = File.ReadAllLines(path);

                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }

                return string.Join(", ", words);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        
        /// <summary>
        /// Returns a random word from the file. 
        /// </summary>
        /// <returns>String of random word</returns>
        public static string RandomWord()
        {
            string path = "../../../allWords.txt";
            Random random = new Random();
            string[] words = File.ReadAllLines(path);

            int randomWordIndex = random.Next(words.Length);
            string randomWord = words[randomWordIndex];



            return randomWord;
        }
        
        
        /// <summary>
        /// Will show the amount of underscores in random word that's selected. 
        /// </summary>
        /// <param name="word"></param>
        /// <returns>char array</returns>
        public static char[] ShowUnderscoresForSelectedRandomWord(string word)
        {
            
            char[] stringToCharWord = word.ToCharArray();

            for (int i = 0; i < stringToCharWord.Length; i++)
            {
                stringToCharWord[i] = '_';
            }
            return stringToCharWord;
        }
        
        
        /// <summary>
        /// This method handles the logic of comparing if the userInput is equal to the one of the letters in the selected word index.
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <param name="input"></param>
        /// <param name="selectedWord"></param>
        /// <returns>Char array of letters</returns>
        public static char[] GuessIfLetterInWord(char[] underscoredWord, string input, char[] selectedWord)
        {
            try
            {
                char userInput = char.Parse(input);
                char[] letters = underscoredWord;
                char[] newValue = new Char[underscoredWord.Length];
                bool contains = ContainsLetter(input, selectedWord);
                if (contains)
                {
                    for (int i = 0; i < selectedWord.Length; i++)
                    {
                        if (userInput == selectedWord[i])
                        {
                            underscoredWord[i] = userInput;
                            if (underscoredWord == selectedWord)
                                break;
                        }
                    }
                }
                else
                {

                    characterGuessed.Add(userInput);
                    Console.WriteLine("These are your previous guesses: ");
                    foreach (char letter in characterGuessed)
                    {
                        Console.WriteLine(letter);
                    }
                }
                return underscoredWord;
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }
           
        }
        
        
        /// <summary>
        /// This is the method to see whether or not a guess is in a word. 
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="word"></param>
        /// <returns>bool</returns>
        public static bool ContainsLetter(string guess, char[] word)
        {
            string stringifyGuess = guess;
            string stringifyWord = String.Join(" ", word);

            if (stringifyWord.Contains(stringifyGuess))
            {
                return true;
            }
            return false;
        }
        
        
        /// <summary>
        /// This sees if a word still contains underscores
        /// </summary>
        /// <param name="noUnderscores"></param>
        /// <returns>boolean</returns>
        public static bool ContainsUnderscores(char[] noUnderscores)
        {
           for (int i =  0; i < noUnderscores.Length; i++)
            {
                if (noUnderscores[i] == '_')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
