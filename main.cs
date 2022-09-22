using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        Que mainQueue = new Que();
        Food[] menu = new Food[9] {new Food("apple", 10, 100),
        new Food("orange", 11, 101),
        new Food("pizza", 25, 500),
        new Food("meat", 35, 700),
        new Food("salad", 15, 200),
        new Food("juice", 14, 125),
        new Food("pepsi", 15, 125),
        new Food("soup", 20, 400),
        new Food("pierogi", 5, 77),
        };
        while (true)
        {
            Console.WriteLine("Enter the name of your client:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter his money:");
            double money = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter his Hunger:");
            double hunger = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter food count:");
            int foodCount = Int16.Parse(Console.ReadLine());
            Random rnd = new Random();
            Food[] order = new Food[foodCount];
            for (int i = 0; i < foodCount; i++)
            {
                order[i] = menu[rnd.Next(0, 8)];
            }
            Visitor visitor = new Visitor(name, order, money, hunger);
            if (visitor.Money < visitor.SumOrderCost())
            {
                Console.WriteLine("You don't have enough money");
                continue;
            }
            mainQueue.AddToQueue(visitor);
            while (mainQueue.GetQueueCount() != 0)
            {
                Console.WriteLine("Press a to to add new visitor, Press m to move Order");
                char c = Console.ReadKey().KeyChar;
                if (c == 'a') break;
                else if (c == 'm') Console.WriteLine(); mainQueue.MoveQueue();
            }
            Console.WriteLine();
            continue;
        }
    }
}

class Que
{
    private Queue<Visitor> queue = new Queue<Visitor>();

    public void MoveQueue()
    {
        Visitor visitor = queue.Dequeue();
        visitor.Eat();
        visitor.ShowVisitorInfo();
        ShowCurrentOrder();
    }

    public int GetQueueCount()
    {
        return queue.Count;
    }

    public void ShowCurrentOrder()
    {
        Console.WriteLine();
        foreach (var v in queue)
        {
            Console.WriteLine(v.Name, v.Money);
        }
    }

    public void AddToQueue(Visitor visitor)
    {
        queue.Enqueue(visitor);
        visitor.GiveMoney(visitor.SumOrderCost());
    }
}

class Visitor
{
    private Food[] order;
    private double hunger;

    internal string Name { get; private set; }
    internal double Money { get; private set; }

    public Visitor(string name1, Food[] order1, double money1, double hunger1)
    {
        Name = name1;
        order = order1;
        Money = money1;
        hunger = hunger1;
    }

    public void GiveMoney(double money1)
    {
        Money -= money1;
    }

    public void Eat()
    {
        hunger -= SumOrderKalorians();
    }

    public double SumOrderCost()
    {
        double s = 0;
        foreach (var o in order)
        {
            s += o.Cost;
        }
        return s;
    }

    public void ShowVisitorInfo()
    {
        Console.WriteLine($"{Name} - money: {Money}, order: {ShowOrder()}, hunger: {hunger}");
    }

    public string ShowOrder()
    {
        string s = "";
        foreach (var o in order)
        {
            s += o.Name;
            s += ", ";
        }
        return s;
    }

    public double SumOrderKalorians()
    {
        double s = 0;
        foreach (var o in order)
        {
            s += o.Kkalorians;
        }
        return s;
    }
}

class Food
{
    internal string Name { get; private set; }
    internal double Kkalorians { get; private set; }
    internal double Cost { get; private set; }

    public Food(string name1, double cost1, double kkal1)
    {
        Name = name1;
        Cost = cost1;
        Kkalorians = kkal1;
    }
}