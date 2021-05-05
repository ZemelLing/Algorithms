using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Algorithms
{
    public sealed class TextInput
    {
        internal static readonly Regex WhiteSpace = new(@"[\s]+", RegexOptions.Compiled);

        private readonly TextReader _input;
        private string _buffer = "";
        private bool _newBuffer = true;
        private string _nextToken = "";

        /// <summary>
        ///     Open a file for reading. Exceptions associated with the file open
        ///     operation may be thrown
        /// </summary>
        /// <param name="inputFileName">the name of the file to read from</param>
        public TextInput(string inputFileName)
        {
            _input = new StreamReader(File.OpenRead(inputFileName));
        }

        /// <summary>
        ///     Connect to the console for reading
        /// </summary>
        public TextInput()
        {
            _input = Console.In;
        }

        /// <summary>
        ///     Check if there is no more input from the character stream
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                TryNextToken();
                return _buffer == null;
            }
        }

        // get the next white space separated token from the buffer
        private void NextToken()
        {
            if (_buffer == "") _buffer = _input.ReadLine();
            if (_buffer != null)
            {
                var m = Regex.Match(_buffer, @"\s*\S+\s*", RegexOptions.Compiled);
                _buffer = _buffer.Substring(m.Value.Length);
                _nextToken = m.Value.Trim();
                if (_nextToken.Equals("")) _buffer = ""; // blank line
                _newBuffer = false;
            }
        }

        // see if there it a token without removing it from the buffer
        private void TryNextToken()
        {
            if (_buffer.Equals(""))
            {
                _buffer = _input.ReadLine();
                _newBuffer = true;
            }

            if (_buffer != null)
            {
                var m = Regex.Match(_buffer, @"\s*\S+\s*", RegexOptions.Compiled);
                //buffer = buffer.Substring(m.Value.Length);
                _nextToken = m.Value.Trim();
            }
        }

        /// <summary>
        ///     Check if there is an integer token from the coming stream
        /// </summary>
        /// <returns>true if an integer can be obtained from the read next</returns>
        public bool HasNextInt()
        {
            TryNextToken();
            if (_buffer == null)
                return false;
            int dummy;
            return int.TryParse(_nextToken, out dummy);
        }

        /// <summary>
        ///     Reads an integer from the stream
        /// </summary>
        /// <exception cref="FormatException">if the token can't be parsed to an integer</exception>
        /// <returns>an integer</returns>
        public int ReadInt()
        {
            NextToken();
            try
            {
                return int.Parse(_nextToken);
            }
            catch (Exception)
            {
                throw new FormatException("error reading int");
            }
        }

        /// <summary>
        ///     Check if there is a double token from the coming stream
        /// </summary>
        /// <returns>true if a double can be obtained from the read next</returns>
        public bool HasNextDouble()
        {
            TryNextToken();
            if (_buffer == null)
                return false;
            double dummy;
            return double.TryParse(_nextToken, out dummy);
        }

        /// <summary>
        ///     Reads a double from the stream
        /// </summary>
        /// <exception cref="FormatException">if the token can't be parsed to an double</exception>
        /// <returns>a double</returns>
        public double ReadDouble()
        {
            NextToken();
            try
            {
                return double.Parse(_nextToken);
            }
            catch (Exception)
            {
                throw new FormatException("error reading double");
            }
        }

        /// <summary>
        ///     Check if there is a char from the coming stream
        /// </summary>
        /// <returns>true if a char can be obtained from the next read</returns>
        public bool HasNextChar()
        {
            if (_buffer.Equals(""))
            {
                _buffer = _input.ReadLine();
                _newBuffer = true;
            }

            if (_buffer == null)
                return false;
            return _buffer != "";
        }

        /// <summary>
        ///     Reads a char from the stream
        /// </summary>
        /// <exception cref="FormatException">if the char can't be obtained</exception>
        /// <returns>a char</returns>
        public char ReadChar()
        {
            if (_buffer.Equals(""))
            {
                _buffer = _input.ReadLine();
                _newBuffer = true;
            }

            if (_buffer == null)
                throw new FormatException("end of file might have been reached");

            if (_buffer.Length > 0)
            {
                var nextChar = _buffer[0];
                _buffer = _buffer.Substring(1);
                return nextChar;
            }

            throw new FormatException("error reading char");
        }

        /// <summary>
        ///     Checks if there is a space-delimited string from the stream
        /// </summary>
        /// <returns>true if there is a space-delimited string</returns>
        public bool HasNextString()
        {
            TryNextToken();
            return _nextToken != "";
        }

        /// <summary>
        ///     Reads a space-delimited string from the stream
        /// </summary>
        /// <returns>the next space-delimited string</returns>
        public string ReadString()
        {
            NextToken();
            var s = _nextToken;
            return s;
        }

        /// <summary>
        ///     Checks if there is a bool value (1, 0, true or false) from the input stream
        /// </summary>
        /// <returns>true if there is a bool value, case-insensitive</returns>
        public bool HasNextBool()
        {
            TryNextToken();
            if (_nextToken.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
                _nextToken.Equals("false", StringComparison.InvariantCultureIgnoreCase) ||
                _nextToken.Equals("1") || _nextToken.Equals("0"))
                return true;
            return false;
        }

        /// <summary>
        ///     Reads a bool value (1, 0, true or false, case-insensitive) from the input stream
        /// </summary>
        /// <returns>the bool value</returns>
        public bool ReadBool()
        {
            NextToken();
            if (_nextToken.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                return true;
            if (_nextToken.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (_nextToken.Equals("1"))
                return true;
            if (_nextToken.Equals("0"))
                return false;
            throw new FormatException("error reading bool");
        }

        /// <summary>
        ///     Check if there is a line, which is essentially a sequence of any
        ///     characters
        /// </summary>
        /// <returns>true if a string can be read next</returns>
        public bool HasNextLine()
        {
            TryNextToken();
            return _buffer != null;
        }

        /// <summary>
        ///     Read a whole new line, discarding unprocessed strings in the previous line
        /// </summary>
        /// <returns>the new line content</returns>
        public string ReadLine()
        {
            string s;
            if (!_newBuffer)
                _buffer = _input.ReadLine();
            s = _buffer;
            _buffer = "";
            return s;
        }

        /// <summary>
        ///     Read all characters from the steam into a string
        /// </summary>
        /// <returns>the rest of the file in a string</returns>
        public string ReadAll()
        {
            return ReadLine() + _input.ReadToEnd();
        }

        /// <summary>
        ///     Reads all space-delimited strings from the input stream as an
        ///     array of strings
        /// </summary>
        /// <returns>the array of strings</returns>
        public string[] ReadAllStrings()
        {
            var bigStr = ReadAll();
            var ss = WhiteSpace.Split(bigStr.Trim());
            return ss;
        }

        /// <summary>
        ///     Reads all integers from the input stream as an
        ///     array of integers
        /// </summary>
        /// <returns>the array of integers</returns>
        public int[] ReadAllInts()
        {
            var ss = ReadAllStrings();
            var vals = new int[ss.Length];
            try
            {
                for (var i = 0; i < ss.Length; i++)
                    vals[i] = int.Parse(ss[i]);
            }
            catch (Exception)
            {
                throw new FormatException("error reading all ints");
            }

            return vals;
        }

        /// <summary>
        ///     Reads all doubles from the input stream as an
        ///     array of doubles
        /// </summary>
        /// <returns>the array of doubles</returns>
        public double[] ReadAllDoubles()
        {
            var ss = ReadAllStrings();
            var vals = new double[ss.Length];
            try
            {
                for (var i = 0; i < ss.Length; i++)
                    vals[i] = double.Parse(ss[i]);
            }
            catch (Exception)
            {
                throw new FormatException("error reading all doubles");
            }

            return vals;
        }

        /// <summary>
        ///     Closes the input stream
        /// </summary>
        public void Close()
        {
            if (_input != null) _input.Close();
        }

        /// <summary>
        ///     Closes the input stream
        /// </summary>
        ~TextInput()
        {
            Close();
        }

        /// <summary>
        ///     Demo test for the <c>TextInput</c> data type. The test shows
        ///     the methods' behavior and how to use them.
        /// </summary>
        /// <param name="args">Place holder for user arguments</param>
        public static void MainTest(string[] args)
        {
            var stdIn = new TextInput();
            Console.Write("Type 3 char, 1 integer, a few strings: ");
            char[] c = {stdIn.ReadChar(), stdIn.ReadChar(), stdIn.ReadChar()};
            var a = stdIn.ReadInt();
            var s = stdIn.ReadLine();
            Console.WriteLine("3 char: {0}, 1 int: {1}, new line: {2}", new string(c), a, s);

            Console.Write("Type a string: ");
            s = stdIn.ReadString();
            Console.WriteLine("Your string was: " + s);
            Console.WriteLine();

            Console.Write("Type an int: ");
            a = stdIn.ReadInt();
            Console.WriteLine("Your int was: " + a);
            Console.WriteLine();

            Console.Write("Type a bool: ");
            var b = stdIn.ReadBool();
            Console.WriteLine("Your bool was: " + b);
            Console.WriteLine();

            Console.Write("Type a double: ");
            var d = stdIn.ReadDouble();
            Console.WriteLine("Your double was: " + d);
            Console.WriteLine();

            Console.WriteLine("Enter a line:");
            s = stdIn.ReadLine();
            Console.WriteLine("Your line was: " + s);
            Console.WriteLine();

            Console.Write("Type any thing you like, enter Ctrl-Z to finish: ");
            var all = stdIn.ReadAllStrings();
            Console.WriteLine("Your remaining input was:");
            foreach (var str in all) Console.Write(str + " ");
            Console.WriteLine();
        }
    }
}