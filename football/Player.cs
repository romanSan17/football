using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football
{
    internal class Player
    {
        //переменные которые будут хранящить данные 
        public string Name { get; }
        public double X { get; private set; }
        public double Y { get; private set; }
        private double _vx, _vy;
        public Team? Team { get; set; } = null;

        private const double MaxSpeed = 5;
        private const double MaxKickSpeed = 25;
        private const double BallKickDistance = 10;

        private Random _random = new Random();

        //создание имени игрока
        public Player(string name)
        {
            Name = name;
        }
        //присвоение ему позиции и команды
        public Player(string name, double x, double y, Team team)
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }
        //устанавливает игрока на присвоенные координаты
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }
        //позиция относительно команды
        public (double, double) GetAbsolutePosition()
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        //рассчитывает расстояние от игрока до мяча по теореме пифагора
        public double GetDistanceToBall()
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        //движение игрока к мячу
        public void MoveTowardsBall()
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            _vx = dx / ratio;
            _vy = dy / ratio;
        }
        //определяет самый близкий ли игрок к мячу, если нет то останавливает игрока
        public void Move()
        {
            if (Team.GetClosestPlayerToBall() != this)
            {
                _vx = 0;
                _vy = 0;
            }

            if (GetDistanceToBall() < BallKickDistance)
            {
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(),
                    MaxKickSpeed * (_random.NextDouble() - 0.5)
                    );
            }
            //Вычисляет новые позиции, добавляя скорости к текущей позиции
            var newX = X + _vx;
            var newY = Y + _vy;
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = _vy = 0;
            }
        }
    }
}
