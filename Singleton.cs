using System;
using System.Collections.Generic;

namespace Singleton
{
    public sealed class FruitStore
    {
        private static readonly FruitStore _instance = new FruitStore();

        private FruitStore()
        {
            Fruits = new List<string> { "Apple", "Banana", "Orange", "Grapes" };
        }

        public static FruitStore Instance => _instance;

        public List<string> Fruits { get; private set; }

        public void AddFruit(string fruit)
        {
            Fruits.Add(fruit);
        }

        public void RemoveFruit(string fruit)
        {
            Fruits.Remove(fruit);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FruitStore store1 = FruitStore.Instance;
            FruitStore store2 = FruitStore.Instance;

            if (store1 == store2)
            {
                Console.WriteLine("Magazyn owoców działa poprawnie, obie zmienne wskazują na tę samą instancję.");
            }
            else
            {
                Console.WriteLine("Błąd, magazyn owoców ma różne instancje.");
            }

            store1.AddFruit("Pineapple");
            store2.RemoveFruit("Banana");

            Console.WriteLine("Dostępne owoce w magazynie:");
            foreach (var fruit in store1.Fruits)
            {
                Console.WriteLine(fruit);
            }
        }
    }
}
