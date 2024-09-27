using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем стадион с шириной 100 и высотой 50
            Stadium stadium = new Stadium(100, 50);

            // Создаем команды
            Team homeTeam = new Team("Home Team");
            Team awayTeam = new Team("Away Team");

            // Добавляем игроков в домашнюю команду
            homeTeam.AddPlayer(new Player("Player 1"));
            homeTeam.AddPlayer(new Player("Player 2"));
            homeTeam.AddPlayer(new Player("Player 3"));

            // Добавляем игроков в гостевую команду
            awayTeam.AddPlayer(new Player("Player A"));
            awayTeam.AddPlayer(new Player("Player B"));
            awayTeam.AddPlayer(new Player("Player C"));

            // Создаем игру
            Game game = new Game(homeTeam, awayTeam, stadium);

            // Запускаем игру
            game.Start();

            // Симуляция движений
            for (int i = 0; i < 10; i++) // допустим, 10 итераций
            {
                game.Move();
                // Выводим позиции мяча
                Console.WriteLine($"Ball position: X = {game.Ball.X}, Y = {game.Ball.Y}");
            }
        }
    }
}
