// See https://aka.ms/new-console-template for more information

using LabCM;

NotLinearFunction a = new NotLinearFunction(-1f, 3f, 0.00001f, (float x) =>
{
    return (float)(Math.Pow(x, 3) + 1.5 * Math.Pow(x, 2) - 0.7 * x - 2.3);
} );

Console.WriteLine(a.DychotomyMethod());
Console.WriteLine(a.HordMethod());
