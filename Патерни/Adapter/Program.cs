using Adapter;

float kg = 55;
float lb = 55;
IScales euroScales = new EuroScales(kg);
IScales britishScales = new AdapterForBritishScales(new BritishScales(lb));
Console.WriteLine(euroScales.GetWeight() + "kg");
Console.WriteLine(britishScales.GetWeight()+ "kg");