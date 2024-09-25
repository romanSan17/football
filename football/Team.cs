using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football
{
    internal class Team
    {
        //свойства
        public List<Player> Players { get; } = new List<Player>();
        public string Name { get; private set; }
        public Game Game { get; set; }

        public Team(string name)
        {
            Name = name;
        }
        //случайно перемещает игроков на поле
        public void StartGame(int width, int height)
        {
            Random rnd = new Random();
            foreach (var player in Players)
            {
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height
                    );
            }
        }
        //добавляет игрока в команду если он без нее
        public void AddPlayer(Player player)
        {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }
        //дает мяч команде
        public (double, double) GetBallPosition()
        {
            return Game.GetBallPositionForTeam(this);
        }
        //скорость мяча
        public void SetBallSpeed(double vx, double vy)
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }
        //ищет игрока который ближе к мячу
        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue;
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            return closestPlayer;
        }
        //все движения
        public void Move()
        {
            GetClosestPlayerToBall().MoveTowardsBall();
            Players.ForEach(player => player.Move());
        }
    }
}
