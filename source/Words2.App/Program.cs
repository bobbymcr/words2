//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            WordTrie trie = new WordTrie();
            using (StreamReader reader = new StreamReader("TWL06.txt"))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                while (!reader.EndOfStream)
                {
                    WordLoader.Load(reader.ReadLine(), w => trie.Add(w));
                }

                Console.WriteLine("Loaded in {0:0.000} seconds.", stopwatch.Elapsed.TotalSeconds);
            }

            bool done = false;
            while (!done)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    string[] fields = line.Split(new char[] { ':' }, 2);
                    int length = -1;
                    if (fields.Length == 2)
                    {
                        if (!int.TryParse(fields[1], out length))
                        {
                            Console.WriteLine("Bad length value.");
                            continue;
                        }
                    }

                    bool isAnagram = false;
                    if (fields[0].StartsWith("*", StringComparison.Ordinal))
                    {
                        isAnagram = true;
                        fields[0] = fields[0].Substring(1);
                    }

                    if (fields[0].Length > Word.MaxLength)
                    {
                        Console.WriteLine("Word is too long.");
                        continue;
                    }

                    Word input = new Word(fields[0]);

                    if (length == -1)
                    {
                        length = isAnagram ? input.Length : Word.MaxLength;
                    }

                    if (isAnagram)
                    {
                        MatchAnagram(trie, input, length);
                    }
                    else
                    {
                        MatchPrefix(trie, input, length);
                    }
                }
                else
                {
                    done = true;
                }
            }
        }

        private static void MatchAnagram(WordTrie trie, Word input, int minLength)
        {
            HashSet<Word> found = new HashSet<Word>();
            Action<Word> onMatch = delegate(Word match)
            {
                if (found.Add(match))
                {
                    Console.Write(match.ToString());
                    Console.Write(" ");
                }
            };

            Console.Write("Matches: ");
            Stopwatch stopwatch = Stopwatch.StartNew();
            trie.MatchAnagram(input, minLength, onMatch);

            Console.WriteLine();
            Console.WriteLine(" total found: {0} (elapsed time {1:0.000} sec).", found.Count, stopwatch.Elapsed.TotalSeconds);
        }

        private static void MatchPrefix(WordTrie trie, Word input, int maxLength)
        {
            int count = 0;
            Action<Word> onMatch = delegate(Word match)
            {
                Console.Write(match.ToString());
                Console.Write(" ");
                ++count;
            };

            Console.Write("Matches: ");
            Stopwatch stopwatch = Stopwatch.StartNew();
            trie.Match(input, maxLength, onMatch);

            Console.WriteLine();
            Console.WriteLine(" total found: {0} (elapsed time {1:0.000} sec).", count, stopwatch.Elapsed.TotalSeconds);
        }
    }
}
