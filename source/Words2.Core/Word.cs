//-----------------------------------------------------------------------
// <copyright file="Word.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public struct Word : IEquatable<Word>, IEnumerable<char>
    {
        private readonly string word;

        public Word(string word)
        {
            this.word = word.ToUpperInvariant();
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

        public override int GetHashCode()
        {
            return this.word.GetHashCode();
        }

        public bool Equals(Word other)
        {
            return this.word == other.word;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return this.word.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
