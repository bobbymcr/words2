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
    using System.Text;

    public struct Word : IEquatable<Word>, IComparable<Word>, IEnumerable<char>
    {
        public const char WildChar = '?';
        public const int MaxLength = 15;

        private readonly byte length;
        private readonly byte c0;
        private readonly byte c1;
        private readonly byte c2;
        private readonly byte c3;
        private readonly byte c4;
        private readonly byte c5;
        private readonly byte c6;
        private readonly byte c7;
        private readonly byte c8;
        private readonly byte c9;
        private readonly byte cA;
        private readonly byte cB;
        private readonly byte cC;
        private readonly byte cD;
        private readonly byte cE;

        public Word(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException("word");
            }

            if (word.Length > MaxLength)
            {
                throw new ArgumentOutOfRangeException("word", "Word cannot be longer than " + MaxLength + " characters.");
            }

            this = new Word();
            this.length = (byte)word.Length;
            for (int i = 0; i < this.length; ++i)
            {
                byte a = Encode(word[i]);
                switch (i)
                {
                    case 0x0:
                        this.c0 = a;
                        break;
                    case 0x1:
                        this.c1 = a;
                        break;
                    case 0x2:
                        this.c2 = a;
                        break;
                    case 0x3:
                        this.c3 = a;
                        break;
                    case 0x4:
                        this.c4 = a;
                        break;
                    case 0x5:
                        this.c5 = a;
                        break;
                    case 0x6:
                        this.c6 = a;
                        break;
                    case 0x7:
                        this.c7 = a;
                        break;
                    case 0x8:
                        this.c8 = a;
                        break;
                    case 0x9:
                        this.c9 = a;
                        break;
                    case 0xA:
                        this.cA = a;
                        break;
                    case 0xB:
                        this.cB = a;
                        break;
                    case 0xC:
                        this.cC = a;
                        break;
                    case 0xD:
                        this.cD = a;
                        break;
                    case 0xE:
                        this.cE = a;
                        break;
                }
            }
        }

        private Word(Word other, char c)
        {
            this = other;
            byte a = Encode(c);
            this.length = (byte)(other.length + 1);

            switch (this.length)
            {
                case 0x1:
                    this.c0 = a;
                    break;
                case 0x2:
                    this.c1 = a;
                    break;
                case 0x3:
                    this.c2 = a;
                    break;
                case 0x4:
                    this.c3 = a;
                    break;
                case 0x5:
                    this.c4 = a;
                    break;
                case 0x6:
                    this.c5 = a;
                    break;
                case 0x7:
                    this.c6 = a;
                    break;
                case 0x8:
                    this.c7 = a;
                    break;
                case 0x9:
                    this.c8 = a;
                    break;
                case 0xA:
                    this.c9 = a;
                    break;
                case 0xB:
                    this.cA = a;
                    break;
                case 0xC:
                    this.cB = a;
                    break;
                case 0xD:
                    this.cC = a;
                    break;
                case 0xE:
                    this.cD = a;
                    break;
                case 0xF:
                    this.cE = a;
                    break;
            }
        }

        private Word(Word other, int index, char c)
        {
            this = other;
            byte a = Encode(c);

            switch (index)
            {
                case 0x0:
                    this.c0 = a;
                    break;
                case 0x1:
                    this.c1 = a;
                    break;
                case 0x2:
                    this.c2 = a;
                    break;
                case 0x3:
                    this.c3 = a;
                    break;
                case 0x4:
                    this.c4 = a;
                    break;
                case 0x5:
                    this.c5 = a;
                    break;
                case 0x6:
                    this.c6 = a;
                    break;
                case 0x7:
                    this.c7 = a;
                    break;
                case 0x8:
                    this.c8 = a;
                    break;
                case 0x9:
                    this.c9 = a;
                    break;
                case 0xA:
                    this.cA = a;
                    break;
                case 0xB:
                    this.cB = a;
                    break;
                case 0xC:
                    this.cC = a;
                    break;
                case 0xD:
                    this.cD = a;
                    break;
                case 0xE:
                    this.cE = a;
                    break;
            }
        }

        public int Length
        {
            get { return this.length; }
        }

        public char this[int index]
        {
            get
            {
                if (index >= this.length)
                {
                    throw new IndexOutOfRangeException();
                }

                return (char)(this.GetByteValue(index) + WildChar);
            }
        }

        public Word Append(char suffix)
        {
            return new Word(this, suffix);
        }

        public Word Replace(int index, char replacement)
        {
            return new Word(this, index, replacement);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(this.length);
            for (int i = 0; i < this.length; ++i)
            {
                sb.Append(this[i]);
            }

            return sb.ToString();
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
            ulong x =
                ((ulong)this.length) |
                ((ulong)this.c0 << 8) |
                ((ulong)this.c1 << 16) |
                ((ulong)this.c2 << 24) |
                ((ulong)this.c3 << 32) |
                ((ulong)this.c4 << 40) |
                ((ulong)this.c5 << 48) |
                ((ulong)this.c6 << 56);
            ulong y =
                ((ulong)this.c7) |
                ((ulong)this.c8 << 8) |
                ((ulong)this.c9 << 16) |
                ((ulong)this.cA << 24) |
                ((ulong)this.cB << 32) |
                ((ulong)this.cC << 40) |
                ((ulong)this.cD << 48) |
                ((ulong)this.cE << 56);

            return x.GetHashCode() ^ y.GetHashCode();
        }

        public bool Equals(Word other)
        {
            return this.CompareTo(other) == 0;
        }

        public int CompareTo(Word other)
        {
            int n1 = this.length;
            int n2 = other.length;
            int m;
            if (n1 < n2)
            {
                m = n1;
            }
            else
            {
                m = n2;
            }

            for (int i = 0; i < m; ++i)
            {
                int c = this[i].CompareTo(other[i]);
                if (c != 0)
                {
                    return c;
                }
            }

            if (n1 < n2)
            {
                return -1;
            }
            else if (n1 > n2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (int i = 0; i < this.length; ++i)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static byte Encode(char c)
        {
            char cu = char.ToUpperInvariant(c);
            int a = cu - WildChar;
            if ((a < 0) || (a > 28))
            {
                throw new ArgumentException("Word contained an invalid character ('" + c + "').", "word");
            }

            return (byte)a;
        }

        private byte GetByteValue(int index)
        {
            switch (index)
            {
                case 0x0: return this.c0;
                case 0x1: return this.c1;
                case 0x2: return this.c2;
                case 0x3: return this.c3;
                case 0x4: return this.c4;
                case 0x5: return this.c5;
                case 0x6: return this.c6;
                case 0x7: return this.c7;
                case 0x8: return this.c8;
                case 0x9: return this.c9;
                case 0xA: return this.cA;
                case 0xB: return this.cB;
                case 0xC: return this.cC;
                case 0xD: return this.cD;
                case 0xE: return this.cE;
                default: throw new IndexOutOfRangeException();
            }
        }
    }
}
