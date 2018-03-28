using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading;

namespace SoD_Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);
            Console.Beep();
            Game.ListAdder();
            Game.Start();
        }
    }
    class Game
    {
        public static bool door;
        public static int monArea = 5;
        public static int playerHP = 37;
        public static int monsterHP = 100;
        public static int area = 0;
        public static bool sword;
        public static int revolver = 0;
        public static bool shield;
        public static bool vines;
        public static bool coin;
        public static int monster = 0;
        public static string commandIn = "";
        public static char delim = ' ';
        public static List<string> command = new List<string>();
        public static List<string> inv = new List<string>();
        public static List<string> vocab = new List<string>();
        public static List<string> hitable = new List<string>();
        public static List<string> hitter = new List<string>();
        public static List<string> gettable = new List<string>();
        public static List<string> seeable = new List<string>();
        public static List<string> giveable = new List<string>();
        public static void ListAdder()
        {
            //vocab
            //verbs
            vocab.Add("yourself");
            vocab.Add("help");
            vocab.Add("exit");
            vocab.Add("give");
            vocab.Add("yes");
            vocab.Add("no");
            vocab.Add("look");
            vocab.Add("get");
            vocab.Add("hit");
            vocab.Add("shoot");
            vocab.Add("die");
            vocab.Add("north");
            vocab.Add("n");
            vocab.Add("south");
            vocab.Add("s");
            vocab.Add("east");
            vocab.Add("e");
            vocab.Add("west");
            vocab.Add("w");
            vocab.Add("door");

            //nouns
            //player parts
            vocab.Add("fists");
            hitter.Add("fists");
            //objects
            vocab.Add("sword");
            hitter.Add("sword");
            gettable.Add("sword");
            seeable.Add("sword");
            seeable.Add("revolver");
            seeable.Add("vines");
            seeable.Add("door");
            seeable.Add("coin");
            seeable.Add("sign");
            vocab.Add("coin");
            vocab.Add("shield");
            vocab.Add("revolver");
            gettable.Add("revolver");
            gettable.Add("coin");
            vocab.Add("sign");
            vocab.Add("vines");
            vocab.Add("wait");
            //actors(can act as objects in certain conditions)
            vocab.Add("statue");
            giveable.Add("statue");
            seeable.Add("statue");
            vocab.Add("monster");
            giveable.Add("monster");
            //hittable
            hitable.Add("vines");
            hitable.Add("monster");
            hitable.Add("yourself");
        }
        public static void Parser()
        {
            commandIn = Console.ReadLine().ToLower();
            command = commandIn.Split(delim).ToList();
            while (command.Contains("the")) { command.Remove("the"); }
            while (command.Contains("with")) { command.Remove("with"); }
            while (command.Contains("my")) { command.Remove("my"); }
            while (command.Contains("at")) { command.Remove("at"); }
            while (command.Contains("to")) { command.Remove("to"); }
            while (command.Contains("up")) { command.Remove("up"); }
            while (command.Contains("go")) { command.Remove("go"); }
            for (int i = 0; i < command.Count; i++)
            {
                if (command[i].Contains("self") || command[i].Contains("myself") || command[i].Contains("me"))
                {
                    command[i] = "yourself";
                }
                if (command[i].Contains("cut") || command[i].Contains("attack") || command[i].Contains("stab"))
                {
                    command[i] = "hit";
                }
                if (command[i].Contains("grab") || command[i].Contains("take") || command[i].Contains("pick"))
                {
                    command[i] = "get";
                }
            }
            foreach (string s in command.ToList())
            {
                foreach (string t in vocab.ToList())
                {
                    if (s.ToLower() == t)
                    {
                        if (command.Count() == 1 && command.Intersect(seeable).Any())
                        {
                            Desc();                     
                        }
                        switch (s.ToLower())
                            { //switch statement to work out player input
                                case "help":
                                    Console.WriteLine("\nhelp output\n");
                                    break;
                                case "exit":
                                    Environment.Exit(0);
                                    break;
                                case "die":
                                    if (area > 0)
                                {
                                    Console.WriteLine("\nYour heart seizes, as if caught by some cosmic force.\n");
                                    Gover();
                                }
                                else
                                {
                                    Console.WriteLine("\nYou cannot die here. Are you ready to begin?\n");
                                }
                                    break;
                                case "shoot":
                                    if (area > 0)
                                    {
                                        if (revolver == 2)
                                        {
                                            Console.WriteLine("\nYou have no bullets.\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nYou don't have anything to shoot with.\n");
                                        }
                                        
                                    }
                                    else
                                    {
                                    Console.WriteLine("There's nothing to shoot. Are you ready to begin?");
                                    }
                                    break;
                                case "yes":
                                    if (area == 0 || area == 7)
                                    {
                                        if (command.Count() == 1)
                                        {
                                            if (area == 7)
                                            {
                                                Console.WriteLine("\nThen let us begin again.");
                                            }
                                        GameplayStart();
                                            
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("That is not a valid comand at this time.");
                                    }
                                    break;
                                case "no":
                                        if (area == 0 || area == 7)
                                    {
                                        if (command.Count() == 1)
                                        {
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("That is not a valid comand at this time.");
                                    }
                                    break;
                                case "give":
                                    Give();
                                    break;
                                case "wait":
                                    break;
                                case "get":
                                    Get();
                                    break; 
                                case "hit":
                                    Hit();
                                    break;
                                case "look":
                                    Look();
                                    break;
                                case "north": case "n":
                                    switch (area)
                                    {
                                        case 1:
                                            NorthernMost();
                                            break;
                                        case 3:
                                            GameplayStart();
                                            break;
                                        case 5:
                                            TreasureRoom();
                                            break;
                                        default:
                                            Console.WriteLine("\nYou cannot go north.");
                                            break;
                                    }
                                    break;
                                case "south": case "s":
                                    switch (area)
                                    {
                                        case 1:
                                            SouthernMost();
                                            break;
                                        case 2:
                                            GameplayStart();
                                            break;
                                        case 6:
                                            EasternMost();
                                            break;
                                        default:
                                            Console.WriteLine("\nYou cannot go south.\n");
                                            break;
                                    }
                                    break;
                                case "west": case "w":
                                    switch (area)
                                    {
                                        case 1:
                                            WesternMost();
                                            break;
                                        case 5:
                                            GameplayStart();
                                            break;
                                        case 6:
                                            Console.WriteLine("\nThe way west is blocked by stones.\n");
                                            break;
                                        default:
                                            Console.WriteLine("\nYou cannot go west.\n");
                                            break;
                                    }
                                    break;
                                case "east": case "e":
                                    switch (area)
                                    {
                                        case 1:
                                            if (!door)
                                            {
                                                Console.WriteLine("\nThere is a closed door blocking your path.\n");
                                            }
                                            else
                                            {
                                                EasternMost();
                                            }
                                            break;
                                        case 2:
                                            Console.WriteLine("\nThe way east is blocked by stones.\n");
                                            break;
                                        case 4:
                                            GameplayStart();
                                            break;
                                        default:
                                            Console.WriteLine("\nYou cannot go east.\n");
                                            break;
                                    }
                                    break;
                                case "win":
                                    Console.WriteLine("\nNice try.\n");
                                    break;
                                default:
                                    break; 
                            }
                    }
                    }
                if (!vocab.Contains(s.ToLower()))
                {
                    Console.WriteLine("\n\"" + s + "\" is not a recognized input.\n");  
                }
                if (playerHP <= 0)
                {
                    Gover();
                }
                var mongen = new Random();
                int monnum = mongen.Next(3);
                if (!door)
                {
                    if (monnum == 2)
                    {
                        monArea = 6;
                    }
                    else if (monnum < 2)
                    {
                        monArea = 5;
                        
                    }
                }
            }
        }
        
        public static void Start()
        {
            area = 0;
            Console.WriteLine( "Welcome." );
            Console.WriteLine("\nAre you ready to begin?\n");
            while (true)
            {
                Parser();
            }
        }
        public static void GameplayStart()
        {
            area = 1;
            if (sword)
            {
                Console.WriteLine("\nYou are in a small, barren room. There is a sign that reads \"CAVE\" on the wall \nand a statue in the corner.\n");
                
            }
            else
            {
                Console.WriteLine("\nYou are in a small, barren room. There is a sign that reads \"CAVE\" on the wall, a statue in the corner, and a sword on the floor.\n");
            }
            while (true)
            {
                Parser();
            }
        }
        public static void NorthernMost()
        {
            area = 2;
            if (revolver == 1)
            {
                Console.WriteLine("\nThere is an excess of vegetation in this room. There is a doorway to the east, \nbut it is blocked by stones. Vine fluid is everywhere.\n");
            }
            else if (revolver == 2)
            {
                Console.WriteLine("\nThere is an excess of vegetation in this room. There is a doorway to the east, \nbut it is blocked by stones. Vine fluid is everywhere. The revolver is on the floor.\n");
            }
            else 
            {
                Console.WriteLine("\nThere is an excess of vegetation in this room. There is a doorway to the east, \nbut it is blocked by stones. A revolver is caught in some vines on the wall.\n");
            }
            while (true)
            {
                Parser();
            }
        }
        public static void SouthernMost()
        {
            Console.WriteLine("\nYou are in the south.\n");
            area = 3;
            while (true)
            {
                Parser();
            }
        }
        public static void WesternMost()
        {
            area = 4;
            if (coin) { }
            Console.WriteLine("\nThis room's ceiling is far too high to see, if it even has one. A small blue\ncoin lies on the floor.\n");
            while (true)
            {
                Parser();
            }
        }
        public static void EasternMost()
        {
            area = 5;
            Console.WriteLine("\nThis room reeks of evil and blood. The bones of fallen adventurers litter the \nfloor.\n");
            if (monArea == area)
            {
                Console.WriteLine("There is a monster in the room, snarling menacingly at you.\n");
            }
            while (true)
            {
                Parser();
            }
        }
        public static void TreasureRoom()
        {
            area = 6;
            Console.WriteLine("\nThis room is overflowing with gold and jewels. If such riches were liquid, \nyou could swim in them.\n");
            if (monArea == area)
            {
                Console.WriteLine("There is a monster in the room, snarling menacingly at you.\n");
            }
            while (true)
            {
                Parser();
            }
        }
        public static void Give()
        {
            //grammar is "give [item] to [reciever]"
            string reciever = command.Last();
            if (area != 0)
            {
                if (command.Count() == 3)
                {
                    if (giveable.Contains(reciever))
                    {
                        command.Remove(command.Last());
                        string given = command.Last();
                        if (inv.Contains(given) && area > 0)
                        {
                            if (area == 1)
                            {
                                if (reciever == "statue")
                                {
                                    switch (given)
                                    {
                                        case "coin":
                                            Console.WriteLine("\nThe statue's hand closes around the coin, and the door barring your eastward \nprogress opens.\n");
                                            door = true;
                                            coin = false;
                                            break;
                                        default:
                                            Console.WriteLine("\nYou give the " + given + " to the statue, but nothing happens. You take it back.\n");
                                            break;
                                    }
                                }
                            }
                            else if (area == 5 || area == 6)
                            {
                                if (reciever == "monster")
                                {
                                    switch (given)
                                    {
                                        default:
                                            Console.WriteLine("\nThe monster will not take the " + given + " from you.\n");
                                            break;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("\nYou cannot give things to a " + reciever + " .\n");
                    }
                }
                else if (command.Count() == 2)
                {
                    Console.WriteLine("\nWhat do you want to give the " + command.Last() + " to?\n");
                }
                else if (command.Count() == 1)
                {
                    Console.WriteLine("What do you want to give?");
                }
            }
            else
            {
                Console.WriteLine("\nThere's nothing to give, and no one to give it to. Are you ready to begin?\n");
            }
        }
        public static void Monster()
        {
            
        }
        public static void Hit()
        {
            string hitr = command.Last();
            if (command.Count() == 3)
            {
                if (hitter.Contains(hitr))
                {
                    command.Remove(command.Last());
                    string hite = command.Last();
                    if (hitable.Contains(hite) && area > 0)
                    {
                        if (area == 2)
                        {
                            if (hite == "vines")
                            {
                                switch (hitr)
                                {
                                    case "sword":
                                        if (sword)
                                        {
                                            vines = true;
                                            Console.WriteLine("\nThe vines are easily cut by the sword, and begin spewing a strange liquid.\nThe revolver is now on the floor. Vine fluid is everywhere.\n");
                                            revolver = 1;
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nYou do not have a sword.\n");
                                        }
                                        break;
                                    case "fists":
                                        Console.WriteLine("\nThe vines are unimpressed by the blunt force trauma your fists inflict.\n");
                                        break;
                                    default:
                                        Console.WriteLine("\nYou cannot hit with the "+ hitr +".\n");
                                        break;
                                }
                            }
                            
                            else { Console.WriteLine("\nhitting the " + hite + " does nothing.\n"); }
                        }
                        else if (hite == "yourself")
                        {
                            switch (hitr)
                            {
                                case "sword":
                                    if (sword)
                                    {
                                        Console.WriteLine("\nYou stab yourself in the stomach and cut sideways, causing your internal \norgans to spill out onto the cold ground.\n");
                                        Gover();
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nYou do not have a sword.\n");
                                    }
                                    break;
                                case "fists":
                                    Console.WriteLine("\nYou punch yourself. Beyond giving yourself a bruise, nothing is accomplished.\n");
                                    break;
                            }
                        }
                        else if (hite == "monster")
                        {
                            switch (hitr)
                            {
                                case "sword":
                                    if (sword)
                                    {
                                        if (monsterHP <= 10)
                                        {
                                            Console.WriteLine("\nWith one mighty swing, you cut the beast's head off. It dissolves into a \nmurky black substance.\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nYou swing your sword at the monster. The blow lands quite heavily, but the \nmonster continues to stand.\n");
                                            monsterHP = monsterHP - 10;
                                            Monhit();
                                        }
                                    }
                                    break;
                                case "fists":
                                    Console.WriteLine("\nThe monster seems unimpressed by your fisticuffs.\n");
                                    break;
                                default:
                                    Console.WriteLine("\nYou don't have a " + hitr + " to hit with.\n");
                                    break;
                            }
                        }
                        else if (area == 1)
                        {
                            Console.WriteLine("\nhitting the " + hite + " does nothing.\n");
                        }

                        else
                        {
                            Console.WriteLine("\nThere is no " + hite + " to hit.\n");
                        }
                    }
                    else if (area > 0)
                    {
                        Console.WriteLine("\nHitting the " + hite + " does nothing.\n");
                    }
                }
                else { Console.WriteLine("\nYou cannot hit with the " + command.Last() + ".\n"); }
            }
            else if (command.Count() == 2)
            {
                Console.WriteLine("\nWhat do you want to hit the " + command.Last() + " with?\n");
            }
            else if (command.Count() == 1)
            {
                if (area == 0)
                {
                    Console.WriteLine("\nThere's nothing to hit. Are you ready to begin?\n");
                }
                else
                {
                    Console.WriteLine("\nWhat do you want to hit?\n");
                }
            }
        }
        public static void Get()
        {
            string getted = "";
            if (area == 0)
            {
                Console.WriteLine("\nYou cannot get anything. Are you ready to begin?\n");
            }
            else if (area == 7)
            {
                Console.WriteLine("\nYou cannot get anything, as you are dead.\n");
            }
            else if (command.Count() > 1)
            {
                getted = command.Last();
            }
            else
            {
                Console.WriteLine("\nGet what?\n");
            }
            if (vocab.Contains(getted))
            {
                if (inv.Contains(getted))
                {
                    Console.WriteLine("\nYou already have a " + getted + ".\n");
                }
                if (!gettable.Contains(getted))
                {
                    Console.WriteLine("\nYou cannot get the " + getted + ".\n");
                }
                if (gettable.Contains(getted))
                {
                    switch (area)
                    {
                        case 1:
                            if (getted == "sword" && sword == false)
                            {
                                Console.WriteLine("\nYou picked up the sword.\n");
                                sword = true;
                                inv.Add("sword");
                            }
                            break;
                        case 2:
                            if (getted == "revolver" && revolver == 0 || revolver == 1)
                            {
                                if (!vines)
                                {
                                    Console.WriteLine("\nThe revolver is lodged in the vines and will not budge.\n");
                                }
                                else if (vines)
                                {
                                    Console.WriteLine("\nYou picked the revolver up off the ground. It is coated in vine fluid.\n");
                                    revolver = 2;
                                    inv.Add("revolver");
                                }
                            }
                            break;
                        case 4:
                            if (getted == "coin" && coin == false)
                            {
                                Console.WriteLine("\nYou picked up the coin. It was covering a tiny hole in the floor, and now water is slowly seeping into the cave.\n");
                                coin = true;
                                inv.Add("coin");
                            }
                            break;
                        default:
                            Console.WriteLine("\nThere's not a " + getted + " to get.\n");
                            break;
                    }
                }
                else if (command.Count() == 1)
                {
                    Console.WriteLine("\nGet what?\n");
                }
            }                        
        }
        public static void Desc()
        {
            switch (command.Last())
            {
                case "statue":
                    Items statue = new Items("statue", "\nThe statue depicts a vagabond reaching a hand out, begging. What for, you can\nnot tell.\n");
                    break;
                case "sword":
                    Items schvert = new Items("sword", "\nIt's a sword.\n");
                    break;
                case "revolver":
                    Items spinn = new Items("revolver", "\nIt looks to be fairly typical. It has no bullets in it.\n");
                    break;
                case "vines":
                    Items vines = new Items("vines", "\nThey're long, green, and stringy. The fluid that runs through them makes a \ngood machine lubricant.\n");
                    break;
                case "door":
                    Items doore = new Items("door", "\nThe door is made of solid stone, and doesn't seem to have any locking mechanism,or indeed any distinguishing features.\n");
                    break;
                case "coin":
                    Items geld = new Items("coin", "\nThe coin seems to be made of cobalt. An eagle is printed on one side.\n");
                    break;
                case "sign":
                    Items sigint = new Items("sign", "\nThe word \"cave\" is painted in large, red letters upon it. A small disclaimer \nbelow, also written in red paint, reads:\n\n\"You actually can't win this game. You're in for a bad time.\"\n");
                    break;
                default:
                    Console.WriteLine("\nErr no. *#.. : undefined item\n");
                    break;
            } 
        }
        public static void Look()
        {
            if (command.Count() >= 2 && command.Last().ToLower() != "look")
            {
                Console.WriteLine("\nThe look command is not defined for this. To look at an item, use the\nitem's name. In this case, use \"" + command.Last() + "\".\n");
            }
            else if (command.Count() < 2)
            {
                switch (area)
                {
                    case 0:
                        Console.WriteLine("\nThere's nothing to see. Are you ready to begin?\n");
                        break;
                    case 1:
                        if (sword)
                        {
                            Console.WriteLine("\nYou are in a small, barren room. There is a sign that reads \"CAVE\" on the wall \nand a statue in the corner.\n");

                        }
                        else
                        {
                            Console.WriteLine("\nYou are in a small, barren room. There is a sign that reads \"CAVE\" on the wall, a statue in the corner, and a sword on the floor.\n");
                        }
                        break;
                    case 2:
                        if (revolver == 0)
                        {
                            Console.WriteLine("\nThere is an excess of vegetation in this room. There is a doorway to the east, \nbut it is blocked by stones. Vine fluid is everywhere.\n");
                        }
                        else if (revolver == 1)
                        {
                            Console.WriteLine("\nThere is an excess of vegetation in this room. There is a doorway to the east, \nbut it is blocked by stones. Vine fluid is everywhere. The revolver is on the floor.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nThere is an excess of vegetation in this room. There is a doorway to the east, \nbut it is blocked by stones. A revolver is caught in some vines on the wall.\n");
                        }
                        break;
                    case 3:
                        Console.WriteLine("This room is completely empty.");
                        break;
                    case 4:
                        if (!coin)
                        {
                            Console.WriteLine("\nThis room's ceiling is far too high to see, if it even has one. A small blue\ncoin lies on the floor.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nThis room's ceiling is far too high to see, if it even has one.\n");
                        }
                        break;
                    case 5:
                        Console.WriteLine("\nThis room reeks of evil and blood. The bones of fallen adventurers litter the \nfloor.\n");
                        if (monArea == area)
                        {
                            Console.WriteLine("There is a monster in the room, snarling menacingly at you.\n");
                        }
                        break;
                    case 6:
                        Console.WriteLine("\nThis room is overflowing with gold and jewels. If such riches were liquid, \nyou could swim in them.\n");
                        if (monArea == area)
                        {
                            Console.WriteLine("There is a monster in the room, snarling menacingly at you.\n");
                        }
                        break;
                    default:
                        Console.WriteLine("\nErr no. *#.$ : undefined area in \"Look\"\n");
                        break;
                }
            }
        }
        public static void Gover()
        {
            area = 7;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have died.\n");
            Console.ResetColor();
            Console.WriteLine("Would you like to try again?\n");
        }
        public static void Win()
        {
            //The only winning move is not to play. I don't know why you'd even want to play, though. This game is utter garbage.
        }
        public static void Monhit()
        {
            if (monArea == area)
            {
                if (playerHP <= 7)
                {
                    Console.WriteLine("With a final blow, the monster cleaves you in twain.\n");
                    Gover();
                }
                else if (playerHP > 7)
                {
                    playerHP = playerHP - 15;
                    Console.WriteLine("The monster strikes you, digging its claws into your flesh. You cannot take \nmany hits such as this before you die.\n");
                }
            }
        }
    }//end of Game
    class Items 
    {
        public Items() : this("", "")
        {
            //initializer
        }
        public Items(string name, string desc)
        {       //generic versions of things needed in this if part
            // else if (name == "" && (Game.name || game.area == areanum)){ Console.WriteLine("{0}", desc); }
            // else if (name == "" && (Game.name || game.area != areanum)){ Console.WriteLine("I don't see any " + name + "."); }
            if (name == "revolver" && (Game.revolver == 2 || Game.area == 2))
            {
                Console.WriteLine("{0}", desc);
            }
            else if (name == "revolver" && (Game.revolver != 2 || Game.area != 2))
            {
                Console.WriteLine("\nI don't see any " + name + ".\n");
            }
            else if (name == "sword" && (Game.sword || Game.area == 1))
            {
                Console.WriteLine("{0}", desc);
            }
            else if (name == "sword" && (!Game.sword || Game.area != 1))
            {
                Console.WriteLine("\nI don't see any " + name + ".\n");
            }
            else if (name == "vines" && Game.area == 2)
            {
                Console.WriteLine("{0}", desc);
            }
            else if (name == "vines" && Game.area != 2)
            {
                Console.WriteLine("\nI don't see any " + name + ".\n");
            }
            else if (name == "door" && Game.area == 1)
            {
                Console.WriteLine("{0}", desc);
            }
            else if (name == "door" && Game.area != 1)
            {
                Console.WriteLine("I don't see any " + name + ".");
            }
            else if (name == "sign" && Game.area == 1)
            {
                Console.WriteLine("{0}", desc);
            }
            else if (name == "sign" && Game.area != 1)
            {
                Console.WriteLine("I don't see any " + name + ".");
            }
            else if (name == "coin" && (Game.coin || Game.area == 4))
            {
                Console.WriteLine("{0}", desc);
            }
            else if (name == "coin" && (!Game.coin || Game.area != 4))
            {
                Console.WriteLine("I don't see any " + name + ".");
            }
            else if (name == "statue" && (Game.area == 1))
            {
                Console.WriteLine("{0}", desc);
            }
            else if (name == "statue" && (Game.area != 1))
            {
                Console.WriteLine("I don't see any " + name + ".");
            }
        }

    }
}