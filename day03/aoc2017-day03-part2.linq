<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	// Advent of code 2017
	// Day 3, part 2
	// Is this over engineered?
	
	int input = 265149;
	var grid = new Grid();
	while (grid.CurrentValue < input)
	{
		grid.Move();
		//Console.WriteLine($"X: {grid.CurrentPosition.X}; Y: {grid.CurrentPosition.Y}; Value: {grid.CurrentValue}");
	}
	Console.WriteLine($"Result is {grid.CurrentValue}");
}

public enum MoveDirection
{
	East = 0,	
	North,
	West,
	South
}

public class Grid
{
	private Dictionary<Point, int> _cells = new Dictionary<Point, int>();
	private Point _current = new Point(0, 0);
	private MoveDirection _lastmove = MoveDirection.East;

	public int Count => _cells.Count;
	public Point CurrentPosition { get { return _current; } }
	public int CurrentValue { get { return _cells.Count == 0 ? 0 : _cells[_current]; } }
	
	public Grid()
	{
		Reset();
	}
	
	public void Reset() 
	{
		_current = new Point(0, 0);
		_cells.Clear();		
		_cells.Add(new Point(0, 0), 1);
	}
	
	public void Move()
	{
		if (_current.X == _current.Y || _current.X == _current.Y * -1)
		{
			// It's a corner
			if (_lastmove == MoveDirection.East)
			{
				Move(MoveDirection.East);
				_lastmove = MoveDirection.North;
			}
			else if (_lastmove == MoveDirection.North)
				Move(MoveDirection.West);
			else if (_lastmove == MoveDirection.West)
				Move(MoveDirection.South);
			else // if (_lastmove == MoveDirection.North)
				Move(MoveDirection.East);
		}
		else 
		{
			Move(_lastmove);
		}
		CalcAndStore();
	}
	
	private int CalcAndStore()
	{
		// Sum all adjacent cell values. 8 adjacent cells...
		int x = _current.X, y = _current.Y;
		var result = GetCellValue(x + 1, y)
			+ GetCellValue(x + 1, y + 1)
			+ GetCellValue(x, y + 1)
			+ GetCellValue(x - 1, y + 1)
			+ GetCellValue(x - 1, y)
			+ GetCellValue(x - 1, y - 1)
			+ GetCellValue(x, y - 1)
			+ GetCellValue(x + 1, y - 1);
			
		_cells.Add(new Point(x, y), result);
		return result;
	}
	
	private int GetCellValue(int x, int y)
	{
		int result;
		return _cells.TryGetValue(new Point(x, y), out result) ? result : 0;
	}
	
	private void Move(MoveDirection direction)
	{
		switch (direction)
		{
			case MoveDirection.East:
				_current.X += 1;
				break;
			case MoveDirection.North:
				_current.Y += 1;
				break;
			case MoveDirection.West:
				_current.X -= 1;
				break;
			case MoveDirection.South:
				_current.Y -= 1;
				break;
		}
		_lastmove = direction;
	}
}
