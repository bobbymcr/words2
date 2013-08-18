//-----------------------------------------------------------------------
// <copyright file="WordLoader.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2
{
    using System;

    public static class WordLoader
    {
        private static readonly char[] WhitespaceChars = new char[] { ' ', '\t', '\r', '\n' };

        public static void Load(string line, Action<Word> onWordFound)
        {
            foreach (string s in line.Split(WhitespaceChars, StringSplitOptions.RemoveEmptyEntries))
            {
                if (s.Length <= 15)
                {
                    Word word = new Word(s);
                    onWordFound(word);
                }
            }
        }
    }
}
