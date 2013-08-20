//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Brian Rogers">
// Copyright (c) Brian Rogers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Words2
{
    using System;
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
                    if (line.StartsWith("?", StringComparison.Ordinal))
                    {
                        MatchAnagram(trie, line);
                    }
                    else
                    {
                        MatchPrefix(trie, line);
                    }
                }
                else
                {
                    done = true;
                }
            }
        }

        private static void MatchAnagram(WordTrie trie, string line)
        {
            if (line.Length == 1)
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            Word input = new Word(line.Substring(1));
            int count = 0;
            Action<Word> onMatch = delegate(Word match)
            {
                Console.Write(match.ToString());
                Console.Write(" ");
                ++count;
            };

            Console.Write("Matches: ");
            Stopwatch stopwatch = Stopwatch.StartNew();
            trie.MatchAnagram(input, onMatch);

            Console.WriteLine();
            Console.WriteLine(" total found: {0} (elapsed time {1:0.000} sec).", count, stopwatch.Elapsed.TotalSeconds);
        }

        private static void MatchPrefix(WordTrie trie, string line)
        {
            string[] fields = line.Split(new char[] { ':' }, 2);
            int maxLength = Word.MaxLength;
            if (fields.Length == 2)
            {
                if (!int.TryParse(fields[1], out maxLength))
                {
                    Console.WriteLine("Bad length value.");
                    return;
                }
            }

            if (fields[0].Length > Word.MaxLength)
            {
                Console.WriteLine("Word is too long.");
                return;
            }

            Word prefix = new Word(fields[0]);
            int count = 0;
            Action<Word> onMatch = delegate(Word match)
            {
                Console.Write(match.ToString());
                Console.Write(" ");
                ++count;
            };

            Console.Write("Matches: ");
            Stopwatch stopwatch = Stopwatch.StartNew();
            trie.Match(prefix, maxLength, onMatch);

            Console.WriteLine();
            Console.WriteLine(" total found: {0} (elapsed time {1:0.000} sec).", count, stopwatch.Elapsed.TotalSeconds);
        }
    }
}
