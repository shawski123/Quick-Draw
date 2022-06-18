using System;
using System.Diagnostics;

while (true)
{
    Console.Clear();

    const string menu = @"In this game, your reaction time is tested. Press [spacebar] when you are told to fire.
if you do not do it fast enough, you lose.

Choose Your Opponent:
  [1] Easy....1000 milliseconds
  [2] Medium...500 milliseconds
  [3] Hard.....250 milliseconds
  [4] Harder...125 milliseconds
  [escape] give up";



    const string wait = @"
  Quick Draw
                                                        
                                                        
                                                        
              _O                          O_            
             |/|_          wait          _|\|           
             /\                            /\           
            /  |                          |  \          
  ------------------------------------------------------";

    const string fire = @"
  Quick Draw
                                                        
                         ********                       
                         * FIRE *                       
              _O         ********         O_            
             |/|_                        _|\|           
             /\          spacebar          /\           
            /  |                          |  \          
  ------------------------------------------------------";

    const string loseTooSlow = @"
  Quick Draw
                                                        
                                                        
                                                        
                                        > ╗__O          
           //            Too Slow           / \         
          O/__/\         You Lose          /\           
               \                          |  \          
  ------------------------------------------------------";

    const string loseTooFast = @"
  Quick Draw
                                                        
                                                        
                                                        
                         Too Fast       > ╗__O          
           //           You Missed          / \         
          O/__/\         You Lose          /\           
               \                          |  \          
  ------------------------------------------------------";

    const string win = @"
  Quick Draw
                                                        
                                                        
                                                        
            O__╔ <                                      
           / \                               \\         
             /\          You Win          /\__\O        
            /  |                          /             
  ------------------------------------------------------";
    TimeSpan reactionSpeed = TimeSpan.Zero;
    bool escapePressed = false;
    string result = null!;
    int shoot;
    Random random = new();
    Console.WriteLine(menu);
    int reactionTime;

    WhenToShoot();

    if (escapePressed)
    {
        return;
    }

    if (result is null)
    {
        Combat();
    }
    Console.Clear();
    Console.Write(result);
    Console.WriteLine("\nDo you want to play again [Enter] or exit [Escape].");
    if (result == win)
    {
        Console.WriteLine("Your reaction speed was " + reactionSpeed);
    }

    if (Console.ReadKey(true).Key is ConsoleKey.Enter)
    {
        continue;
    }
    else if (Console.ReadKey(true).Key is ConsoleKey.Escape)
    {
        return;
    }

    void WhenToShoot()
    {
        reactionTime = 0;
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.D1 or ConsoleKey.NumPad1: reactionTime = 1000; break;
            case ConsoleKey.D2 or ConsoleKey.NumPad2: reactionTime = 500; break;
            case ConsoleKey.D3 or ConsoleKey.NumPad3: reactionTime = 250; break;
            case ConsoleKey.D4 or ConsoleKey.NumPad4: reactionTime = 125; break;
            case ConsoleKey.Escape: escapePressed = true; return;
        }
        Console.Clear();
        Console.WriteLine(wait);
        Stopwatch sw = Stopwatch.StartNew();
        shoot = random.Next(3000, 10000);
        while (sw.ElapsedMilliseconds < shoot)
        {
            while (Console.KeyAvailable && Console.ReadKey(true).Key is ConsoleKey.Spacebar)
            {
                result = loseTooFast;
                return;
            }
        }
    }

    void Combat()
    {
        Console.Clear();
        Console.WriteLine(fire);
        Stopwatch sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < reactionTime)
        {
            while (Console.KeyAvailable && Console.ReadKey(true).Key is ConsoleKey.Spacebar)
            {
                sw.Stop();
                reactionSpeed = sw.Elapsed;
                result = win;
                return;
            }
        }
        result = loseTooSlow;
        return;

    }
}
