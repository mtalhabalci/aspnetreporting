using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SDIKit.Common.Helpers
{
    /// <summary>
    /// Utility class for handling random
    /// information.
    /// </summary>
    public class LipsumGeneratorHelper : Random
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public LipsumGeneratorHelper()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Seed">Seed value</param>
        public LipsumGeneratorHelper(int Seed) : base(Seed)
        {
        }

        #endregion Constructors

        #region Public Functions

        /// <summary>
        /// returns a random date/time for a specific date range.
        /// </summary>
        /// <param name="Start">Start time</param>
        /// <param name="End">End time</param>
        /// <returns>A random date/time between the start and end times</returns>
        public DateTime NextDate(DateTime Start, DateTime End)
        {
            if (Start > End)
            {
                throw new ArgumentException("The start value must be earlier than the end value");
            }
            return Start + new TimeSpan((long)(new TimeSpan(End.Ticks - Start.Ticks).Ticks * NextDouble()));
        }

        /// <summary>
        /// returns a randomly generated string
        /// </summary>
        /// <param name="Length">Length of the string</param>
        /// <returns>a randomly generated string of the specified length</returns>
        public string NextString(int Length)
        {
            if (Length < 1)
                return "";
            return NextString(Length, ".");
        }

        /// <summary>
        /// Returns a randomly generated string of a specified length, containing
        /// only a set of characters
        /// </summary>
        /// <param name="Length">length of the string</param>
        /// <param name="AllowedCharacters">Characters that are allowed in the string</param>
        /// <returns>A randomly generated string of the specified length, containing only the allowed characters.</returns>
        public string NextString(int Length, string AllowedCharacters)
        {
            if (Length < 1)
                return "";
            return NextString(Length, AllowedCharacters, Length);
        }

        /// <summary>
        /// Returns a randomly generated string of a specified length, containing
        /// only a set of characters, and at max a specified number of non alpha numeric characters.
        /// </summary>
        /// <param name="Length">Length of the string</param>
        /// <param name="AllowedCharacters">Characters allowed in the string</param>
        /// <param name="NumberOfNonAlphaNumericsAllowed">Number of non alpha numeric characters allowed.</param>
        /// <returns>A randomly generated string of a specified length, containing only a set of characters, and at max a specified number of non alpha numeric characters.</returns>
        public string NextString(int Length, string AllowedCharacters, int NumberOfNonAlphaNumericsAllowed)
        {
            if (Length < 1)
                return "";
            StringBuilder TempBuilder = new StringBuilder();
            Regex Comparer = new Regex(AllowedCharacters);
            Regex AlphaNumbericComparer = new Regex("[0-9a-zA-z]");
            int Counter = 0;
            while (TempBuilder.Length < Length)
            {
                string TempValue = new string(Convert.ToChar(Convert.ToInt32(System.Math.Floor(94 * NextDouble() + 32))), 1);
                if (Comparer.IsMatch(TempValue))
                {
                    if (!AlphaNumbericComparer.IsMatch(TempValue) && NumberOfNonAlphaNumericsAllowed > Counter)
                    {
                        TempBuilder.Append(TempValue);
                        ++Counter;
                    }
                    else if (AlphaNumbericComparer.IsMatch(TempValue))
                    {
                        TempBuilder.Append(TempValue);
                    }
                }
            }
            return TempBuilder.ToString();
        }

        /// <summary>
        /// Creates a Lorem Ipsum sentence.
        /// </summary>
        /// <param name="NumberOfWords">Number of words for the sentence</param>
        /// <returns>A string containing Lorem Ipsum text</returns>
        public string NextLoremIpsum(int NumberOfWords, bool endWithPoint = true)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append(Words[Next(Words.Length)].ToFirstCharacterUpperCase());
            for (int x = 1; x < NumberOfWords; ++x)
            {
                Builder.Append(" ").Append(Words[Next(Words.Length)]);
            }

            if (endWithPoint)
                Builder.Append(".");

            return Builder.ToString();
        }

        /// <summary>
        /// Creates a Lorem Ipsum paragraph.
        /// </summary>
        /// <param name="NumberOfParagraphs">Number of paragraphs</param>
        /// <param name="MaxSentenceLength">Maximum sentence length</param>
        /// <param name="MinSentenceLength">Minimum sentence length</param>
        /// <param name="NumberOfSentences">Number of sentences per paragraph</param>
        /// <param name="HTMLFormatting">Determines if this should use HTML formatting or not</param>
        /// <returns>A string containing Lorem Ipsum text</returns>
        public string NextLoremIpsum(int NumberOfParagraphs, int NumberOfSentences, int MinSentenceLength, int MaxSentenceLength, bool HTMLFormatting)
        {
            StringBuilder Builder = new StringBuilder();
            if (HTMLFormatting)
                Builder.Append("<p>");
            //Builder.Append("Lorem ipsum dolor sit amet. ");
            for (int y = 0; y < NumberOfSentences; ++y)
            {
                Builder.Append(NextLoremIpsum(Next(MinSentenceLength, MaxSentenceLength))).Append(" ");
            }
            if (HTMLFormatting)
                Builder.Append("</p>");
            for (int x = 1; x < NumberOfParagraphs; ++x)
            {
                if (HTMLFormatting)
                    Builder.Append("<p>");
                for (int y = 0; y < NumberOfSentences; ++y)
                {
                    Builder.Append(NextLoremIpsum(Next(MinSentenceLength, MaxSentenceLength))).Append(" ");
                }
                if (HTMLFormatting)
                    Builder.Append("</p>");
                else
                    Builder.Append(System.Environment.NewLine).Append(System.Environment.NewLine);
            }
            return Builder.ToString();
        }

        /// <summary>
        /// Creates a Lorem Ipsum paragraph.
        /// </summary>
        /// <param name="NumberOfParagraphs">Number of paragraphs</param>
        /// <param name="MaxSentenceLength">Maximum sentence length</param>
        /// <param name="MinSentenceLength">Minimum sentence length</param>
        /// <param name="NumberOfSentences">Number of sentences per paragraph</param>
        /// <returns>A string containing Lorem Ipsum text</returns>
        public string NextLoremIpsum(int NumberOfParagraphs, int NumberOfSentences, int MinSentenceLength, int MaxSentenceLength)
        {
            return NextLoremIpsum(NumberOfParagraphs, NumberOfSentences, MinSentenceLength, MaxSentenceLength, false);
        }

        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        /// <returns>returns a boolean</returns>
        public bool NextBool()
        {
            if (Next(0, 2) == 1)
                return true;
            return false;
        }

        /// <summary>
        /// Gets a random enum value
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>A random value from an enum</returns>
        public T NextEnum<T>()
        {
            Array Values = Enum.GetValues(typeof(T));
            int Index = Next(0, Values.Length);
            return (T)Values.GetValue(Index);
        }

        /// <summary>
        /// Randomly generates a new time span
        /// </summary>
        /// <param name="Start">Start time span</param>
        /// <param name="End">End time span</param>
        /// <returns>A time span between the start and end</returns>
        public TimeSpan NextTimeSpan(TimeSpan Start, TimeSpan End)
        {
            if (Start > End)
            {
                throw new ArgumentException("The start value must be earlier than the end value");
            }
            return Start + new TimeSpan((long)(new TimeSpan(End.Ticks - Start.Ticks).Ticks * NextDouble()));
        }

        #endregion Public Functions

        #region Private Variables

        private string[] Words = new string[] { "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
     "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
     "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
     "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet",
     "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
     "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
     "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
     "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet",
     "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
     "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
     "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
     "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis",
     "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie",
     "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et",
     "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit",
     "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "lorem", "ipsum", "dolor", "sit", "amet",
     "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet",
     "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis",
     "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea",
     "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate",
     "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at",
     "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum",
     "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor",
     "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim",
     "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing",
     "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam",
     "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation",
     "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis",
     "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie",
     "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et", "accusam",
     "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea",
     "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
     "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut",
     "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et",
     "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no",
     "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
     "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore", "dolores",
     "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo", "labore",
     "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata",
     "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
     "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut",
     "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam",
     "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed",
     "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea",
     "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum" };

        #endregion Private Variables
    }
}