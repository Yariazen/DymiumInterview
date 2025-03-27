/**
 * @brief Reverses a portion of the given character array, from the specified start index to the end index.
 *
 * @param start The starting index of the portion to reverse.
 * @param end The ending index of the portion to reverse.
 * @param buffer The character array to reverse.
 */
static void ReverseInPlace(int start, int end, char[] buffer)
{
    if (start >= end) return;

    while (start < end)
    {
        char temp = buffer[start];
        buffer[start] = buffer[end];
        buffer[end] = temp;
        start++;
        end--;
    }
}

/**
 * @brief Reverses the words in the given character array.
 * 
 * Words are sequences of non-space characters separated by spaces.
 * The function first reverses the entire character array, then reverses each individual word in place.
 *
 * @param buffer The character array containing words to reverse.
 */
static void ReverseWords(char[] buffer)
{
    if (buffer.Length == 0) return;

    // Step 1: Reverse the entire buffer
    ReverseInPlace(0, buffer.Length - 1, buffer);

    // Step 2: Reverse individual words
    int start = 0;
    while (start < buffer.Length)
    {
        // Find the start of the next word (skip spaces)
        while (start < buffer.Length && buffer[start] == ' ')
            start++;
        if (start >= buffer.Length)
            break;  // No more words

        // Find the end of the word (first space or end of buffer)
        int end = start;
        while (end < buffer.Length && buffer[end] != ' ')
            end++;

        // Reverse the word (end is now exclusive, so no need to adjust)
        ReverseInPlace(start, end - 1, buffer);

        // Move start to the next word
        start = end;
    }
}

/**
 * @brief Runs a test case for ReverseWords and prints a detailed result.
 *
 * The function compares the processed output with the expected output and highlights any differences.
 *
 * @param identifier The name of the test case.
 * @param buffer The input character array (modified in-place).
 * @param expectedResult The expected output character array.
 */
static void Test(string identifier, char[] buffer, char[] expectedResult)
{
    try
    {
        string input = new string(buffer);
        ReverseWords(buffer);
        bool passed = CompareArrays(buffer, expectedResult);

        // Color output for easy identification
        Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"[{identifier}] {(passed ? "PASSED ✅" : "FAILED ❌")}");

        if (!passed)
        {
            Console.ResetColor();
            Console.WriteLine($"  Input:    \"{input}\"");
            Console.WriteLine($"  Output:   \"{new string(buffer)}\"");
            Console.WriteLine($"  Expected: \"{new string(expectedResult)}\"");

            // Highlight the difference
            HighlightDifference(buffer, expectedResult);
        }
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[{identifier}] FAILED ⚠️ (Exception: {e.Message})");
        Console.WriteLine(e.StackTrace);
    }
    finally
    {
        Console.ResetColor();
    }
}

/**
 * @brief Compares two character arrays for equality.
 *
 * @param a The first character array.
 * @param b The second character array.
 * @return true if both arrays match, false otherwise.
 */
static bool CompareArrays(char[] a, char[] b)
{
    if (a.Length != b.Length) return false;
    for (int i = 0; i < a.Length; i++)
        if (a[i] != b[i]) return false;
    return true;
}

/**
 * @brief Highlights the differences between expected and actual outputs for debugging.
 *
 * The function prints two lines:
 *  - First line (Red): Actual output with mismatched characters highlighted.
 *  - Second line (Yellow): Expected output with mismatched characters highlighted.
 *
 * @param actual The actual output character array.
 * @param expected The expected output character array.
 */
static void HighlightDifference(char[] actual, char[] expected)
{
    var originalColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("  Actual:   ");
    Console.ResetColor();
    Console.Write("\"");

    for (int i = 0; i < Math.Max(actual.Length, expected.Length); i++)
    {
        if (i < actual.Length && i < expected.Length && actual[i] == expected[i])
        {
            Console.Write(actual[i]); // Matching characters
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed; // Highlight incorrect characters
            if (i < actual.Length) Console.Write(actual[i]); // Incorrect actual character
            Console.ResetColor();
        }
    }
    Console.WriteLine("\"");

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("  Expected: ");
    Console.ResetColor();
    Console.Write("\"");
    for (int i = 0; i < Math.Max(actual.Length, expected.Length); i++)
    {
        if (i < actual.Length && i < expected.Length && actual[i] == expected[i])
        {
            Console.ResetColor();
            Console.Write(expected[i]); // Matching characters
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Highlight incorrect characters
            if (i < expected.Length) Console.Write(expected[i]); // Incorrect expected character
            Console.ResetColor();
        }
    }
    Console.WriteLine("\"");

    Console.ResetColor();
}



Test("Test empty", "".ToCharArray(), "".ToCharArray());
Test("Test single word", "Hello".ToCharArray(), "Hello".ToCharArray());
Test("Test simple sentence", "This is a test".ToCharArray(), "test a is This".ToCharArray());
Test("Test already reversed", "word second first".ToCharArray(), "first second word".ToCharArray());
Test("Test multiple spaces", "This  is  a   test".ToCharArray(), "test   a  is  This".ToCharArray());
Test("Test only spaces", "     ".ToCharArray(), "     ".ToCharArray());
Test("Test trailing and leading spaces", "    Trailing and leading spaces!   ".ToCharArray(), "   spaces! leading and Trailing    ".ToCharArray());
Test("Test punctuation", "Hello, world!".ToCharArray(), "world! Hello,".ToCharArray());
Test("Test mixed case", "CaSe SeNsItIvE".ToCharArray(), "SeNsItIvE CaSe".ToCharArray());