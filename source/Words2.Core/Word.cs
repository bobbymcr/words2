//-----------------------------------------------------------------------
// <copyright file="Word.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2
{
    using System;

    public struct Word : IEquatable<Word>
    {
        private readonly string word;

        public Word(string word)
        {
            this.word = word.ToUpperInvariant();
        }

        public void ForEach(Action<char> action)
        {
            foreach (char c in this.word)
            {
                action(c);
            }
        }

        public void ForEach(Func<char, bool> func)
        {
            foreach (char c in this.word)
            {
                if (!func(c))
                {
                    break;
                }
            }
        }

        public override string ToString()
        {
            return this.word;
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is Word)
            {
                isEqual = this.Equals((Word)obj);
            }

            return isEqual;
        }

        public bool Equals(Word other)
        {
            return this.word == other.word;
        }
    }
}
