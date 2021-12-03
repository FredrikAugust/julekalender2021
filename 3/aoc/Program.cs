using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadLines("/Users/fredrik-alv/IdeaProjects/julekalender2021/3/aoc/input").ToList();

List<int> gammaRate = new();
List<int> epsilonRate = new();

foreach (var pos in Enumerable.Range(0, input[0].Length))
{
    var zeros = 0;
    var ones = 0;
    
    foreach (var line in input)
    {
        var num = Int32.Parse(line[pos].ToString());

        if (num == 0)
            zeros++;
        else
            ones++;
    }

    if (zeros > ones)
    {
        gammaRate.Add(0);
        epsilonRate.Add(1);
    }
    else
    {
        gammaRate.Add(1);
        epsilonRate.Add(0);
    }
}

Console.WriteLine(string.Join("", gammaRate.Select(num => num.ToString()).ToList()));
Console.WriteLine(string.Join("", epsilonRate.Select(num => num.ToString()).ToList()));
