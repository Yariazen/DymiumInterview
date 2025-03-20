char[] buffer = "This is a char array!".ToCharArray();

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

static void ReverseWords(char[] buffer)
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
    buffer = ReverseInPlace(start, buffer.Length, buffer);
}

ReverseWords(buffer);
Console.WriteLine(buffer);