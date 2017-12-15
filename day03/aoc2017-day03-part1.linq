<Query Kind="Program" />

void Main()
{
	// Advent of code 2017
	// Day 3, part 1
	
	int result;
	//var start = 1; // == 0;
	//var start = 12; // == 3;
	//var start = 23; // == 2;
	//var start = 46; // == 3;
	//var start = 1024; // == 31;
	var start = 265149; // == 438
	if (start == 1)
	{
		result = 1;
	}
	else
	{
		int sqrt = (int)Math.Truncate(Math.Sqrt(start));
		var oddsqrt = (sqrt % 2) == 0 ? sqrt - 1 : sqrt;
		oddsqrt.Dump("Lower root");
		var remain = start - (oddsqrt * oddsqrt);
		remain.Dump("Remain");
		if (remain == 0)
		{
			result = oddsqrt - 1;
		}
		else
		{
			int levelsdeep = ((oddsqrt - 1) / 2) + 1;
			levelsdeep.Dump("Levels");

			var fromcorner = (remain % (oddsqrt + 1));
			fromcorner.Dump("From corner");

			var fromcentre = Math.Abs(((oddsqrt + 1) / 2) - fromcorner);
			fromcentre.Dump("From centre");

			result = levelsdeep + fromcentre;
		}
	}
	result.Dump("Result");
}

