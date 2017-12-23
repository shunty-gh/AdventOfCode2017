<Query Kind="Program" />

void Main()
{
	// Advent of code 2017
	// Day 23, part 2
	
	// Instruction input rewritten slightly with added
	// short circuits to optimise the time
	
	Int64 b = (81 * 100) + 100_000, c = b + 17_000;
	Int64 g = 2, h = 0;

	while (true)   
	{
		Int64 f = 1, d = 2;
		while (g != 0 && f != 0)
		{
			Int64 e = 2;        
			Int64 emax = b / d; // short circuit the loop
			while (g != 0 && e <= emax)  
			{
				g = (d * e) - b;  
				if (g == 0)       
				{
					f = 0;
					break; // short circuit
				}
				e += 1;    
				g = e - b;
			}
			d += 1;        
			g = d - b;     
		}
		if (f == 0)        
			h += 1;        		
		g = b - c;         
		if (g == 0) break; 
		b += 17;            
	}
	Console.WriteLine($"Result {h}");
}
