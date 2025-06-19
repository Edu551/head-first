
OperatorExamples();
TrySomeLoops();

void OperatorExamples()
{
    int width = 3;
    width++;
    int height = 2 + 4;

    int area = width * height;
    Console.WriteLine(area);

    string result = "The area";
    result = result + " is " + area;
    height = 12;
    Console.WriteLine(result);

    bool truthValue = true;
    Console.WriteLine(truthValue);
 }

void TrySomeLoops()
{
    int c = 0;
    while (c < 10)
    {
        c = c + 1;
    }

    for (int i = 0; i < 5; i++)
    {
        c = c - 1;
    }

    Console.WriteLine(c);
}