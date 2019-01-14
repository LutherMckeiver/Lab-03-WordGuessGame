using System;
using Xunit;
using WordGuessGame;

namespace WordGuessGameTesting
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(true)]
        public void CanCreateANewFile(bool expectation)
        {
            Assert.Equal(expectation, Program.CreateNewFile());
        }

        [Theory]
        [InlineData("hello", true)]
        public void AddNewWords(string word, bool expectation)
        {
            Assert.Equal(expectation, Program.AddNewWord(word));
        }

        [Theory]
        [InlineData("tiger, cat, frog, cow, sheep, lion, dragon, fish, shark, hello, hello, hello, hello", true)]
        public void ViewAllWords(string expectation, bool actual)
        {
            Assert.Equal(expectation, Program.ViewAllWords());
        }

        //[Fact]
        //public void CheckIfLetterExistsInWord() // come back to testing..
        //{
        //    char[] word = { 'h', 'e', 'l', 'l', 'o' };
        //    string guess = "h";
        //    bool expected = true;

        //    for(int i = 0; i < word.Length; i++)
        //    {

        //    }


        //    Assert.Equal(guess, Program.ContainsLetter(guess, word));
        //}
    }
}
