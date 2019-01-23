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
        [InlineData(true)]
        public void ViewAllWords(bool expectation)
        {
            Assert.Equal(expectation, Program.ViewAllWords());
        }

      //  [Theory]
       // [InlineData(true)]
       // public void AddANewWord(bool expectation)
       // {
        //    Assert.Equal(expectation, Program.AddNewWord("hello"));
       // }

        [Theory]
        [InlineData("cow", true)]
        public void CanDeleteDisWord(string input, bool expectation)
        {
            string DeleteDatWord = input;
        }
    
 

    }
}
