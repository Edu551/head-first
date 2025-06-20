// See https://aka.ms/new-console-template for more information
using PickRandomCards;

Console.Write("Enter the number of cards to pick: ");
string line = Console.ReadLine();

while (line != "q")
{
    if (int.TryParse(line, out int numberOfCards))
    {
        foreach (string card in CardPicker.PickSomeCards(numberOfCards))
        {
            Console.WriteLine(card);
        }
    }
    else
    {
        Console.WriteLine("Invalid value!!!!");
    }

    Console.Write("Enter the number of cards to pick: ");
    line = Console.ReadLine();
}
