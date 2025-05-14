// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;

int value = 1000;
Console.WriteLine("Hello, World!");
Console.WriteLine("Press the Escape (Esc) key to stop the loop.");

while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Escape)
{
    Console.WriteLine($"Value: {value}");
    value++;
    Thread.Sleep(1000); // Wait for 1 second
}

