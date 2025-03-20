/**
 * Reverses a portion of the given character array, from the specified start index to the end index.
 *
 * @param start The starting index of the portion to reverse.
 * @param end The ending index of the portion to reverse.
 * @param buffer The character array to reverse.
 * @return A character array with the specified portion reversed.
 */
static char[] ReverseInPlace(int start, int end, char[] buffer)
{
    int length = end - start;
    for (int i = 0; i < length / 2; i++)
    {
        char c = buffer[i + start];
        buffer[i + start] = buffer[end - 1 - i];
        buffer[end - 1 - i] = c;
    }
    return buffer;
}

/**
 * Reverses the words in the given character array.
 * Words are sequences of non-space characters separated by spaces.
 * The function reverses the entire character array first, then reverses each individual word in place.
 *
 * @param buffer The character array containing words to reverse.
 * @return A character array with the words reversed in place.
 */
static char[] ReverseWords(char[] buffer)
{
    buffer = ReverseInPlace(0, buffer.Length, buffer);
    int start = 0;

    for (int i = 0; i < buffer.Length; i++)
    {
        if (buffer[i] == ' ')
        {
            buffer = ReverseInPlace(start, i, buffer);
            start = i + 1;
        }
    }
    return ReverseInPlace(start, buffer.Length, buffer);
}

/**
 * Compares two character arrays for equality.
 * The arrays are considered equal if they have the same length and identical content.
 *
 * @param a The first character array.
 * @param b The second character array.
 * @return True if the arrays are equal, otherwise false.
 */
static bool Compare(char[] a, char[] b)
{
    if (a.Length != b.Length)
        return false;

    for (int i = 0; a.Length != b.Length; i++)
    {
        if (a[i] != b[i])
            return false;
    }

    return true;
}

/**
 * Tests the ReverseWords function by comparing the output with the expected output.
 * Displays the test result, along with the input, actual output, and expected output if the test fails.
 *
 * @param identifier The identifier for the test (used for display purposes).
 * @param input The input character array to test with.
 * @param expectedOutput The expected output character array.
 */
static void Test(string identifier, char[] buffer, char[] expectedResult)
{
    try
    {
        string input = new string(buffer);
        ReverseWords(buffer);
        Console.WriteLine(identifier);

        if (Compare(expectedResult, buffer))
        {
            Console.WriteLine("Test Passed!!!");
        }
        else
        {
            Console.WriteLine("Test Failed!!!");
            Console.WriteLine("Input:    " + input);
            Console.WriteLine("Output:   " + new string(buffer));
            Console.WriteLine("Expected: " + new string(expectedResult));
        }
    } catch (Exception e) {
        Console.WriteLine("Test failed with exception " + e.Message);
    }
}

char[] buffer1 = "".ToCharArray();
Test("Test empty", buffer1, "".ToCharArray());

char[] buffer2 = "This is a char array!".ToCharArray();
Test("Test simple sentence", buffer2, "array! char a is This".ToCharArray());

char[] buffer3 = "This  is  a   test".ToCharArray();
Test("Test multiple spaces", buffer3, "test   a  is  This".ToCharArray());

// Not sure what the expected behaviour should be, could trim spaces but it would lead to our buffer missing characters.
char[] buffer4 = "    Trailing and leading spaces!   ".ToCharArray();
Test("Test trailing and leading spaces", buffer4, "    spaces! leading and Trailing   ".ToCharArray());