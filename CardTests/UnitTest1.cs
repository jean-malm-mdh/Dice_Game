using DiceGame;
using Godot;
using NUnit.Framework.Constraints;
using System.Diagnostics;

namespace CardTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }
        [TearDown]
        public void TearDown()
        {
            Trace.Flush();
        }

        [Test]
        public void Test1()
        {
            Random rnd = new Random(1337);
            var cards = CardData.GetCards(_ => (uint)rnd.Next());
            foreach (var card in cards)
                Console.WriteLine(card);
            Assert.Pass();
        }
    }
}