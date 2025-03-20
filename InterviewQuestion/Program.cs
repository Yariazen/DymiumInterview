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

static void Test(string identifier, char[] input, char[] expectedOutput)
{
    try
    {
        char[] output = ReverseWords(input);
        Console.WriteLine(identifier);

        if (input == expectedOutput)
        {
            Console.WriteLine("Test Passed!!!");
        }
        else
        {
            Console.WriteLine("Test Failed!!!");
            Console.WriteLine("Input:    " + input);
            Console.WriteLine("Output:   " + ReverseWords(input));
            Console.WriteLine("Expected: " + expectedOutput);
        }
    } catch (Exception e) {
        Console.WriteLine("Test failed with exception " + e.Message);
    }
}

char[] buffer1 = "".ToCharArray();
Test("Test empty", buffer1, "".ToCharArray());

char[] buffer2 = "This is a char array!".ToCharArray();
Test("Test simple sentence", buffer2, "array! char a is This".ToCharArray());



