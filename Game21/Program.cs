using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Program
    {
        enum Suit
        {
            Diamonds, // Бубны
            Hearts, //  Черви
            Spades, //  Пики
            Clubs //  Трефы
        }

        enum Value
        {
            Six = 6,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack = 2,
            Lady,
            King,
            Ace = 11
        }

        struct Card
        {
            public Suit Suit;
            public Value Value; // ценность карты // имя 6, 7, 8, 9, 10, валет - 2, дама - 3, король - 4, туз - 11
        }

        static void Main(string[] args)
        {
            // Игра 21

            Card[] cards = new Card[36];

            int suit = 0; // идентификатор масти карты

            // генерация упорядоченной колоды карт
            for (int i = 0; i < 36;)
            {
                for (int j = 11; j < (2 + 10); j++) // j = 11, потому что первый в колоде туз
                {
                    cards[i] = new Card { Suit = (Suit)suit, Value = (Value)j };
                    if (j == 11) // потом идут 6, 7, 8, 9, 10
                    {
                        j = 5;
                    }
                    if (j == 10) // потом валет, дама, король
                    {
                        j = 1;
                    }
                    if (j == 4)
                    {
                        j = 2 + 10;
                    }
                    i++;
                }
                suit++;
            }

            int sumYourWins = 0;
            int sumCompWins = 0;

            string compWins = "Computer is winner";
            string yourWins = "You are winner";
            string compOrYou = "(Computer - \"c\", you - \"y\")";
            string tied = "Computer and you have tied";

            string answerContinueGame = "";

            do
            {
                // перемешивание колоды. Каждый раз в середину кладется
                // рандомный элемент. Каждый раз середина будет разная 

                Random random = new Random();

                for (int i = 0; i < cards.Length; i++)
                {
                    int r = random.Next(0, 36);

                    Card card = cards[r];
                    cards[r] = cards[cards.Length / 2];
                    cards[cards.Length / 2] = card;
                }

                int yourTotalCards = 0;
                int compTotalCards = 0;

                yourTotalCards += (int)cards[0].Value;
                yourTotalCards += (int)cards[1].Value;
                compTotalCards += (int)cards[2].Value;
                compTotalCards += (int)cards[3].Value;
                int c = 4;

                // at first you should enter who receives first cards
                string answer = "";

                do
                {
                    Console.WriteLine($"Enter who receives first cards?\n{compOrYou}");
                    answer = Console.ReadLine();
                } while (answer != "c" && answer != "y");

                string firstPlayer = "You";

                // если компьютер первый, меняются значения суммы выданных карт 
                if (answer == "c")
                {
                    int temp = yourTotalCards;
                    yourTotalCards = compTotalCards;
                    compTotalCards = temp;

                    firstPlayer = "Computer";
                }

                bool continueGame = true;

                while (true)
                {
                    if (yourTotalCards == 21 || compTotalCards == 21)
                    {
                        if (yourTotalCards == compTotalCards)
                        {
                            Console.WriteLine(tied);
                        }
                        else
                        {
                            if (yourTotalCards == 21)
                            {
                                Console.WriteLine("You have 21 point. " + yourWins);
                                sumYourWins++;
                            }
                            else
                            {
                                Console.WriteLine("Computor have 21 point. " + compWins);
                                sumCompWins++;
                            }
                            break;
                        }
                    }
                    else if (yourTotalCards == 22 || compTotalCards == 22)
                    {
                        if (yourTotalCards == compTotalCards)
                        {
                            Console.WriteLine(tied);
                        }
                        else
                        {
                            if (yourTotalCards == 22)
                            {
                                Console.WriteLine("You have 2 aces. " + yourWins);
                                sumYourWins++;
                            }
                            else
                            {
                                Console.WriteLine("Computor have 2 aces. " + compWins);
                                sumCompWins++;
                            }
                            break;
                        }
                    }

                    string cont = ""; // continue?

                    do
                    {
                        Console.WriteLine($"Sum of your cards is {yourTotalCards}.\nDo you want to continue? (yes/no)");

                        cont = Console.ReadLine();
                    } while (cont != "yes" && cont != "no");

                    if (cont == "no")
                    {
                        continueGame = false;

                        while (compTotalCards < 17)
                        {
                            compTotalCards += (int)cards[c].Value;
                            c++;
                        }
                    }
                    else
                    {
                        if (firstPlayer == "You")
                        {
                            yourTotalCards += (int)cards[c].Value;
                            c++;

                            while (compTotalCards < 17)
                            {
                                compTotalCards += (int)cards[c].Value;
                                c++;
                            }
                        }
                        else
                        {
                            while (compTotalCards < 17)
                            {
                                compTotalCards += (int)cards[c].Value;
                                c++;
                            }

                            yourTotalCards += (int)cards[c].Value;
                            c++;
                        }
                    }

                    // конец игры
                    if (!continueGame || yourTotalCards > 21)
                    {
                        Console.WriteLine($"Sum of your cards is {yourTotalCards}.\n"
                            + $"Sum of computer cards is {compTotalCards}.\n");

                        if (yourTotalCards == compTotalCards)
                        {
                            Console.WriteLine(tied);
                        }
                        else if (compTotalCards > 21 && yourTotalCards > 21)
                        {
                            if (yourTotalCards > compTotalCards)
                            {
                                Console.WriteLine(compWins);
                                sumCompWins++;
                            }
                            else
                            {
                                Console.WriteLine(yourWins);
                                sumYourWins++;
                            }
                        }
                        else if (compTotalCards > 21)
                        {
                            Console.WriteLine(yourWins);
                            sumYourWins++;
                        }
                        else if (yourTotalCards > 21)
                        {
                            Console.WriteLine(compWins);
                            sumCompWins++;
                        }
                        else if (compTotalCards < yourTotalCards)
                        {
                            Console.WriteLine(yourWins);
                            sumYourWins++;
                        }
                        else
                        {
                            Console.WriteLine(compWins);
                            sumCompWins++;
                        }

                        break;
                    }
                }

                Console.WriteLine("Do you want to play again? (yes/no)");
                answerContinueGame = Console.ReadLine();
            } while (answerContinueGame == "yes");

            Console.WriteLine();
            Console.WriteLine($"You won {sumYourWins} times");
            Console.WriteLine($"Computer won {sumCompWins} times");
        }
    }
}
