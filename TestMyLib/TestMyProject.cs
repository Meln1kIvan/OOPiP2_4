using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace TestMyProject
{
    [TestClass]
    public class TestMyProject
    {
        [TestMethod]
        public void TestIndentText_ShouldIndentTextWithSpecifiedSpaces()
        {
            // Arrange
            string text = "Hello" + Environment.NewLine + "World";
            int spaces = 4;
            string expectedIndentedText = "    Hello" + Environment.NewLine + "    World";

            // Act
            string indentedText = IndentText(text, spaces);

            // Assert
            Assert.AreEqual(expectedIndentedText, indentedText);
        }

        public string IndentText(string text, int spaces)
        {
            // Логика смещения текста на указанное количество пробелов
            // (в данном примере просто добавляем указанное количество пробелов в начало каждой строки)
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new string(' ', spaces) + lines[i];
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}