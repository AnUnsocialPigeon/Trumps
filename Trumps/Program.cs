using System;
using System.Collections.Generic;

namespace Trumps {
    class Program {
        private static Random rnd = new Random();

        private const int CardsInDeck = 600;
        private const int Players = 50;
        private const int Suits = 10;
        private const int Runs = 5000000;


        static void Main(string[] args) {
            Console.WriteLine($"Current Settings:\nCards in a deck: {CardsInDeck}\nPlayers: {Players}\nSuits: {Suits}\nRuns: {Runs}");

            //SimumalteGame1(5000);
            SimulateGame2(Runs);

            Console.ReadLine();
        }

        private static void SimumalteGame1(int runs) {
            DateTime start = DateTime.Now;
            int[] winners = new int[Players];

            //Parallel.For(0, runs, (winners) => {
            //    List<int> PlayerOutOrder = PlayGame1();
            //    AddWinner(PlayerOutOrder, ref winners);
            //});
            DateTime end = DateTime.Now;
            Console.WriteLine($"\nTime elapsed: {(end - start).TotalSeconds}s");
            for (int i = 0; i < Players; i++) {
                Console.WriteLine($"Winner of P{i}: {GetWinAmount(winners, i)}");
            }
        }

        private static void SimulateGame2(int runs) {
            DateTime start = DateTime.Now;
            int[] winners = new int[Players];
            for (int i = 0; i < runs; i++) {
                winners[PlayGame2()]++;
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"\nTime elapsed: {(end - start).TotalSeconds}s");
            for (int i = 0; i < Players; i++) {
                Console.WriteLine($"Winner of P{i}: {GetWinAmount(winners, i)}");
            }
            Console.ReadLine();
        }

        private static float GetWinAmount(int[] winners, int pos) {
            int total = 0;
            foreach (int i in winners) {
                total += i;
            }
            return (float)winners[pos] / total;
        }

        private static void AddWinner(List<int> Out, ref int[] winners) {
            if (Out.Count == 0) return;
            for (int i = 0; i < Players; i++) {
                if (!Out.Contains(i)) {
                    winners[i]++;
                    Console.WriteLine($"Winner: P{i}");
                    return;
                }
            }
        }

        /// <summary>
        /// Plays the game
        /// </summary>
        /// <param name="CardsInDeck"></param>
        /// <param name="Players"></param>
        /// <param name="Suits"></param>
        /// <returns>The order in the players getting out</returns>
        public static List<int> PlayGame1() {
            List<int> Deck = CreateDeck(CardsInDeck);
            List<int> OutPlayers = new List<int>();

            int currentPlayer = 0;
            int Card1 = DrawCard(ref Deck);


            while (Deck.Count >= 1 && OutPlayers.Count < Players - 1) {
                currentPlayer++;
                if (OutPlayers.Contains(currentPlayer % Players)) continue;

                int card = DrawCard(ref Deck);
                if (card % Suits == Card1 % Suits && card < Card1) OutPlayers.Add(currentPlayer % Players);
            }

            return Deck.Count == 0 ? new List<int>() : OutPlayers;
        }

        public static int PlayGame2() {
            List<int> Deck = CreateDeck(CardsInDeck);
            List<int> PlayerCards = new List<int>();

            int Card1 = DrawCard(ref Deck);

            for (int i = 1; i < Players; i++) {
                int card = DrawCard(ref Deck);
                if (Card1 % Suits == card % Suits && card > Card1) return i;
            }
            return 0;
        }

        private static List<int> CreateDeck(int CardCount) {
            List<int> Deck = new List<int>();
            for (int i = 0; i < CardCount; i++) {
                Deck.Add(i);
            }
            return Deck;
        }

        private static int DrawCard(ref List<int> Deck) {
            int pos = rnd.Next(0, Deck.Count);
            int card = Deck[pos];
            Deck.RemoveAt(pos);
            return pos;
        }
    }
}
