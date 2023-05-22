

using System.ComponentModel;
using System.Runtime.InteropServices;

class Program
{
    
    static void Main()
    {
        bool check = true, check1 = true;
        string name1 = string.Empty;
        string name2 = string.Empty;
        int rinput;
        while (true)
        {
            if (check)
            { 
            Console.Write("insert Player1 name : ");
             name1 = Console.ReadLine();
                 if (String.IsNullOrEmpty(name1))
                {
                Console.WriteLine("Invalid name");
                continue;
                 }
                 check = false;
            }
            if (check1)
            {
                Console.Write("insert Player2 name : ");
                name2 = Console.ReadLine();
                if (String.IsNullOrEmpty(name2))
                {
                    Console.WriteLine("Invalid name");
                    continue;
                }
                check1 = false;
            }
            Console.Write("best of ? : ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out rinput))
            {
                Console.WriteLine("invalid number");
                continue;
            }
                

            Game game = new Game(name1, name2,rinput);
            Console.Clear();
            while (true)
            {
                game.CleanBoard(0);
                Console.Write($@"

                    
                                                                                            Best of {game.Bestof}


               BlocNum enabled : {Console.NumberLock}                         player {game.Playerturnlogic()}   Turn{game.x}
                   
                   keybindings                                 {game.Sign[0]} | {game.Sign[1]} | {game.Sign[2]}               {game.Winprint()}
                                                              ---+---+---                             {game.Errorz}
                  7     8     9                                {game.Sign[3]} | {game.Sign[4]} | {game.Sign[5]}               {game.Winprint()}
                                                              ---+---+---                             {game.Errorz} 
                  4     5     6                                {game.Sign[6]} | {game.Sign[7]} | {game.Sign[8]}
                                              
                  1     2     3                                  SCORE 
                                                          {game.Playerstats[0].Item1}:{game.Playerstats[0].Item3}        {game.Playerstats[1].Item1}:{game.Playerstats[1].Item3}
                  p to restart
                  q to quit
                                                                                                       ");


                Char inchar = Console.ReadKey().KeyChar;
                game.Update(inchar);

                Console.Clear();
            }

        }
    }
}
class Game
{
    public String?[] Sign { get; private set; }

    public int Bestof { get; private set; }
    public (String, String, int)[] Playerstats { get; private set; }

    public int x { get; private set; } = 1;

    private bool[] Already { get; set; }

    public String Errorz { get; private set; } = "";


    public Game(String _player1, String _player2, int _bestof)
    {
        Sign = new String?[9];
        Already = new bool[9];
        for (int i = 0; i < Sign.Length; i++) Sign[i] = " ";

        Playerstats = new[]
        {
            (_player1,"X",0),          // name ,symboll assign , score
            (_player2,"O",0),                // player 1 = Playerstats[0]  player 2 = Playerstats[1]
        };

        Bestof = _bestof;
    }

    public void Update(Char _input)
    {
        if (_input == 'p') restart();
        if (Win()) return;
        Errcpy(false);

        switch (_input)
        {
            case '7':
                if ((x % 2) == 0 && !Already[0]) {Sign[0] = "O"; Already[0] = true;}
                else if ((x % 2) != 0 && !Already[0]){ Sign[0] = "X"; Already[0] = true;}
                else { Errcpy(true); return; }
                break;

            case '8':
                if ((x % 2) == 0 && !Already[1]) { Sign[1] = "O"; Already[1] = true; }
                else if ((x % 2) != 0 && !Already[1]) { Sign[1] = "X"; Already[1] = true; }
                else { Errcpy(true); return; }
                break;
            case '9':
                if ((x % 2) == 0 && !Already[2]) { Sign[2] = "O"; Already[2] = true; }
                else if ((x % 2) != 0 && !Already[2]) { Sign[2] = "X"; Already[2] = true; }
                else { Errcpy(true); return; }
                break;
            case '4':
                if ((x % 2) == 0 && !Already[3]) { Sign[3] = "O"; Already[3] = true; }
                else if ((x % 2) != 0 && !Already[3]) { Sign[3] = "X"; Already[3] = true; }
                else { Errcpy(true); return; }
                break;
            case '5':
                if ((x % 2) == 0 && !Already[4]) { Sign[4] = "O"; Already[4] = true; }
                else if ((x % 2) != 0 && !Already[4]) { Sign[4] = "X"; Already[4] = true; }
                else { Errcpy(true); return; }
                break;
            case '6':
                if ((x % 2) == 0 && !Already[5]) { Sign[5] = "O"; Already[5] = true; }
                else if ((x % 2) != 0 && !Already[5]) { Sign[5] = "X"; Already[5] = true; }
                else { Errcpy(true); return; }
                break;
            case '1':
                if ((x % 2) == 0 && !Already[6]) { Sign[6] = "O"; Already[6] = true; }
                else if ((x % 2) != 0 && !Already[6]) { Sign[6] = "X"; Already[6] = true; }
                else { Errcpy(true); return; }
                break;
            case '2':
                if ((x % 2) == 0 && !Already[7]) { Sign[7] = "O"; Already[7] = true; }
                else if ((x % 2) != 0 && !Already[7]) { Sign[7] = "X"; Already[7] = true; }
                else { Errcpy(true); return; }
                break;
            case '3':
                if ((x % 2) == 0 && !Already[8]) { Sign[8] = "O"; Already[8] = true; }
                else if ((x % 2) != 0 && !Already[8]) { Sign[8] = "X"; Already[8] = true; }
                else { Errcpy(true); return; }
                break;
            case 'p': restart();return;
            case 'q': Environment.Exit(0);break;
            default: return; 
        }
        

        if (x > 4) ScoreLogic();
        x++;
    }


    public string Playerturnlogic() => ((x % 2) == 0) ? Playerstats[0].Item1 + ": " + Playerstats[1].Item2 : Playerstats[1].Item1 + ": " + Playerstats[0].Item2;


    public void CleanBoard(int _Option)
    {
        if (x == 10 || _Option == 1)
        {
            Array.Fill(Sign, " ");
            Array.Fill(Already, false);
            x = 0;
        }


    }

    private void ScoreLogic()
    {
        for (int i = 1; i <= 4; i++)
        {

            if (Sign[4 - i] == "O" && Sign[4] == "O" && Sign[4 + i] == "O") Afterm(false);
            if (Sign[0] == "O" && Sign[1] == "O" && Sign[2] == "O") Afterm(false);
            if (Sign[0] == "O" && Sign[3] == "O" && Sign[6] == "O") Afterm(false);
            if (Sign[2] == "O" && Sign[5] == "O" && Sign[8] == "O") Afterm(false);
            if (Sign[6] == "O" && Sign[7] == "O" && Sign[8] == "O") Afterm(false);

            if (Sign[4 - i] == "X" && Sign[4] == "X" && Sign[4 + i] == "X") Afterm(true);
            if (Sign[0] == "X" && Sign[1] == "X" && Sign[2] == "X") Afterm(true);
            if (Sign[0] == "X" && Sign[3] == "X" && Sign[6] == "X") Afterm(true);
            if (Sign[2] == "X" && Sign[5] == "X" && Sign[8] == "X") Afterm(true);
            if (Sign[6] == "X" && Sign[7] == "X" && Sign[8] == "X") Afterm(true);



        }
    }
    private void Afterm(bool _player1)
    {
        if (_player1 == true)
        {
            Playerstats[1].Item3++;
            CleanBoard(1);
            Array.Fill(Already, false);
        }
        else
        {
            Playerstats[0].Item3++;
            CleanBoard(1);
            Array.Fill(Already, false);
        }
    }

    private bool Win()
    {
        if(Playerstats[1].Item3 + Playerstats[0].Item3 == Bestof ) return true;
        return false;
    }

    public string Winprint()
    {
        if ((Playerstats[1].Item3 + Playerstats[0].Item3) == Bestof)
        {
            if (Playerstats[0].Item3 < Playerstats[1].Item3)
                return Playerstats[1].Item1 + "  WON ";

            else return Playerstats[0].Item1 + "  WON ";
        }
        
        else return "";
    }


    private void restart()
    {
        CleanBoard(1);
        x = 1;
        Playerstats[0].Item3 = 0;
        Playerstats[1].Item3 = 0;
        Array.Fill(Already, false);
    }

    public void Errcpy(bool _check)
    {
        if (_check) Errorz = "already assigned";
        else Errorz = "";

    }



}