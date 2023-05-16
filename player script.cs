using System;
using System.Collections.Generic;
using System.IO;

namespace OOP
{
    class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }
        public bool IsAlive { get; private set; }
        public int Score { get; set; }
        public void DisplayStats()
    {
        Console.WriteLine("Player Name: " + Name);
        Console.WriteLine("Health: " + Health);
        Console.WriteLine("Attack: " + AttackPower);
        Console.WriteLine("Defense: " + DefensePower);
        Console.WriteLine("Score: " + Score);
    }
}

        public Player(string name)
        {
            Name = name;
            Health = 100;
            AttackPower = 10;
            Defense = 5;
            IsAlive = true;
            Score = 0;
        }

        public void Attack(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.TakeDamage(AttackPower);
            }
        }

        public void TakeDamage(int damage)
        {
            int inflictedDamage = Math.Max(damage - Defense, 0);
            Health -= inflictedDamage;
            if (Health <= 0)
            {
                IsAlive = false;
            }
        }

        public void IncreaseScore(int points)
        {
            Score += points;
        }

        internal void Rest()
        {
            throw new NotImplementedException();
        }
    }

    class Enemy
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }
        public bool IsAlive { get; private set; }

        public Enemy(string name, int health, int attackPower, int defense)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Defense = defense;
            IsAlive = true;
        }

        public void Attack(Player player)
        {
            player.TakeDamage(AttackPower);
        }

        public void TakeDamage(int damage)
        {
            int inflictedDamage = Math.Max(damage - Defense, 0);
            Health -= inflictedDamage;

            if (Health <= 0)
            {
                IsAlive = false;
            }
        }
    }
}