using System;
using System.Collections.Generic;
using System.Linq;

// Abstract Base Class
abstract class Vehicle
{
    private string make;
    private string model;
    private int year;
    private double dailyRate;

    public string Make
    {
        get { return make; }
        set { make = value; }
    }

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    public double DailyRate
    {
        get { return dailyRate; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Daily rate must be greater than 0.");
            dailyRate = value;
        }
    }

    // Constructor
    public Vehicle(string make, string model, int year, double dailyRate)
    {
        Make = make;
        Model = model;
        Year = year;
        DailyRate = dailyRate;
    }

    // Abstract method
    public abstract double CalculateRentalCost(int days);

    // Virtual method
    public virtual string GetDescription()
    {
        return $"{Year} {Make} {Model} — RM {DailyRate:F2}/day";
    }
}

// Car Class
class Car : Vehicle
{
    public int NumPassengers { get; set; }

    public Car(string make, string model, int year, double dailyRate, int numPassengers)
        : base(make, model, year, dailyRate)
    {
        NumPassengers = numPassengers;
    }

    public override double CalculateRentalCost(int days)
    {
        return DailyRate * days;
    }

    public override string GetDescription()
    {
        return $"{Year} {Make} {Model} ({NumPassengers} passengers) — RM {DailyRate:F2}/day";
    }
}

// Motorcycle Class
class Motorcycle : Vehicle
{
    public bool HasSidecar { get; set; }

    public Motorcycle(string make, string model, int year, double dailyRate, bool hasSidecar)
        : base(make, model, year, dailyRate)
    {
        HasSidecar = hasSidecar;
    }

    public override double CalculateRentalCost(int days)
    {
        double cost = DailyRate * days;

        if (!HasSidecar)
            cost *= 0.9; // 10% discount

        return cost;
    }

    public override string GetDescription()
    {
        string sidecar = HasSidecar ? "with sidecar" : "no sidecar";
        return $"{Year} {Make} {Model} ({sidecar}) — RM {DailyRate:F2}/day";
    }
}

// Truck Class
class Truck : Vehicle
{
    public double PayloadTons { get; set; }

    public Truck(string make, string model, int year, double dailyRate, double payloadTons)
        : base(make, model, year, dailyRate)
    {
        PayloadTons = payloadTons;
    }

    public override double CalculateRentalCost(int days)
    {
        double surcharge = 30 * PayloadTons * days;
        return (DailyRate * days) + surcharge;
    }

    public override string GetDescription()
    {
        return $"{Year} {Make} {Model} ({PayloadTons} tons) — RM {DailyRate:F2}/day";
    }
}

// Main Program
class Program
{
    static void Main()
    {
        List<Vehicle> vehicles = new List<Vehicle>
        {
            new Car("Toyota", "Camry", 2022, 150, 5),
            new Motorcycle("Honda", "CBR", 2021, 80, false),
            new Truck("Volvo", "FH", 2020, 200, 8.5)
        };

        int days = 5;

        foreach (var v in vehicles)
        {
            Console.WriteLine(v.GetDescription());
            Console.WriteLine($"  {days}-day rental cost: RM {v.CalculateRentalCost(days):F2}");
            Console.WriteLine();
        }

        // Find most expensive
        var mostExpensive = vehicles
            .OrderByDescending(v => v.CalculateRentalCost(days))
            .First();

        Console.WriteLine($"Most expensive: {mostExpensive.Year} {mostExpensive.Make} {mostExpensive.Model} — RM {mostExpensive.CalculateRentalCost(days):F2}");
    }
}