using System;
using System.IO;
using System.Linq;
using System.Text;

namespace WordGuessGame
{
    class Program
    {
         
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
            Console.WriteLine("***    5. Exit The Game                        ***");
            Console.WriteLine("***                                            ***");
            Console.WriteLine("***    Choose a number to continue.            ***");
            Console.WriteLine("**************************************************");
            Console.Write("> ");
            string userInput = Console.ReadLine();
            Options(userInput); 
        }
        /// <summary>
        /// Actual game logic
        /// </summary>
        public static void StartANewGame()
        {
            CreateNewFile();
            string word = RandomWord();
            ShowUnderscoresForSelectedRandomWord(word);
            Console.WriteLine();
            Console.WriteLine("Please guess one letter in the word.");
            Console.Write("> ");
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
                    AddNewWord();
                    break;
                case "4":
                    DeleteAWord();
                    break;
                case "5":
                    Console.WriteLine("See you later");
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
        /// Creates a file in the path specified. And adds default words.
        /// </summary>
        /// <param name="path"></param>
        public static void CreateNewFile()
        {
            string path = "../../../allWords.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.Write("tiger\ncat\nfrog\ncow\nsheep\nlion\ndragon\nfish\nshark\n");
                }
            }
        }
        /// <summary>
        /// Adds a new word to the game file. 
        /// </summary>
        /// <returns></returns>
        public static string AddNewWord()
        {

            string path = "../../../allWords.txt";
            Console.Write("Add your word here: ");
            string input = Console.ReadLine();

            string appendText = input + Environment.NewLine;
            File.AppendAllText(path, appendText);
            GuessingGameMenu();

            return appendText;
        }
        /// <summary>
        /// Deletes a word from the game file. 
        /// </summary>
        /// <returns></returns>
        public static string DeleteAWord()
        {
            string path = "../../../allWords.txt";
            Console.WriteLine("Here is a list of the current words: ");
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
            foreach(string word in newFile)
            {
                Console.WriteLine(word);
            }
            return string.Join(", ", newFile);
        }
        /// <summary>
        /// Allows us to view all the words in the game. 
        /// </summary>
        /// <returns>String of all words in file.</returns>
        public static string ViewAllWords()
        {
            string path = "../../../allWords.txt";
            Console.WriteLine("Here is a list of the current words: ");
            string[] words = File.ReadAllLines(path);

            foreach (string word in words)
            {
                Console.WriteLine(word);
            }

            return string.Join(", ", words);
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

        public static char[] ShowUnderscoresForSelectedRandomWord(string word)
        {
            char[] underscoreWord = word.ToCharArray();

            foreach(char letter in underscoreWord)
            {
                Console.Write("_ ");
            }
            return underscoreWord;
        }
    }
}
