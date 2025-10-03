using System;
using System.Text;
using System.Threading;

class GameOfLife
{
    static void Main(string[] args)
    {
        int width = 60;
        int height = 25;
        int delayMs = 80;

        bool[,] grid = new bool[height, width];
        bool[,] next = new bool[height, width];

        var rng = new Random();

      
        for (int r = 0; r < height; r++)
            for (int c = 0; c < width; c++)
                grid[r, c] = rng.NextDouble() < 0.2;

        Console.CursorVisible = false;
        bool paused = false;

        try
        {
            while (true)
            {
              
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Q) break;
                    if (key.Key == ConsoleKey.Spacebar) paused = !paused;
                }

                if (!paused)
                {
                  
                    for (int r = 0; r < height; r++)
                    {
                        for (int c = 0; c < width; c++)
                        {
                            int neighbors = 0;
                            for (int dr = -1; dr <= 1; dr++)
                            for (int dc = -1; dc <= 1; dc++)
                            {
                                if (dr == 0 && dc == 0) continue;
                                int rr = r + dr, cc = c + dc;
                                if (rr >= 0 && rr < height && cc >= 0 && cc < width && grid[rr, cc])
                                    neighbors++;
                            }

                            
                            next[r, c] = neighbors == 3 || (grid[r, c] && neighbors == 2);
                        }
                    }

                    
                    var temp = grid; grid = next; next = temp;
                }

             
                var sb = new StringBuilder(width * (height + 1));
                sb.AppendLine("Conway's Game of Life — Q: quit, Space: pause/resume");
                for (int r = 0; r < height; r++)
                {
                    for (int c = 0; c < width; c++)
                        sb.Append(grid[r, c] ? '█' : ' ');
                    sb.AppendLine();
                }

                Console.SetCursorPosition(0, 0);
                Console.Write(sb.ToString());

                Thread.Sleep(delayMs);
            }
        }
        finally
        {
            Console.CursorVisible = true;
            Console.WriteLine("\nExiting...");
        }
    }
}
