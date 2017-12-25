<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 25
    var result = Solve();
    Console.WriteLine($"Result: {result}");
}

public int SlotValue(Dictionary<int, int> slots, int key)
{
	if (slots.ContainsKey(key))
		return slots[key];
	return 0;
}
public int Solve()
{
	var slots = new Dictionary<int, int>();
	var state = "A";
	var key = 0;
	var rounds = 12861455;
	while (rounds-- > 0)
	{
		var current = SlotValue(slots, key);
		switch (state)
		{
			case "A":
				if (current == 0)
				{
					slots[key] = 1;
					key += 1;
					state = "B";
				}
				else
				{
					slots[key] = 0;
					key -= 1;
					state = "B";
				}
				break;
			case "B":
				if (current == 0)
				{
					slots[key] = 1;
					key -= 1;
					state = "C";
				}
				else
				{
					slots[key] = 0;
					key += 1;
					state = "E";
				}
				break;
			case "C":
				if (current == 0)
				{
					slots[key] = 1;
					key += 1;
					state = "E";
				}
				else
				{
					slots[key] = 0;
					key -= 1;
					state = "D";
				}
				break;
			case "D":
				slots[key] = 1;
				key -= 1;
				state = "A";
				break;
			case "E":
				if (current == 0)
				{
					slots[key] = 0;
					key += 1;
					state = "A";
				}
				else
				{
					slots[key] = 0;
					key += 1;
					state = "F";
				}
				break;
			case "F":
				if (current == 0)
				{
					slots[key] = 1;
					key += 1;
					state = "E";
				}
				else
				{
					slots[key] = 1;
					key += 1;
					state = "A";
				}
				break;
		}
	}
    return slots.Count(s => s.Value == 1);
}
