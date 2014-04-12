using System;
using System.IO;

class Calculator
{
    public static void Main( )
    {
        Console.WriteLine("Please read the following instructions carefully.");
        Console.WriteLine("");
        Console.WriteLine("Name the info file info.txt.");
        Console.WriteLine("The info file should be in the following format: ");
        Console.WriteLine("");
        Console.WriteLine("Mike,John,Jay <---: line 1 Names, separated " + 
            "by commas.");
        Console.WriteLine("23.22,13.23,8.99,Mike <--- line 2 and beyond: " + 
            "Contribution of each person to the receipt followed by the " +
            "owner of the receipt, separated by commas.");
        Console.WriteLine("Contribution should follow the same order as the " +
            "names in line 2. (i.e. $8.99 is contributed by Jay.");
        Console.WriteLine("");
        Console.WriteLine("Press Enter once you have prepared the info file " +
            "according to the instructions.");
        Console.ReadLine( );
        
        StreamReader readIn;
        int numbP;
        string line;
        string[ ] names;
        double[ , ] payment;
        try
        {
            readIn = new StreamReader("info.txt");
            line = readIn.ReadLine( );
            string[ ] partition = line.Split(',');
            names = partition;
            numbP = partition.Length;
            payment = new double [numbP,numbP]; 
            for(int i = 0; i < numbP; i++)  // Initialize the array according to number of participant.
            {
                for(int j = 0; j < numbP; j++)
                {
                    payment[i,j] = 0d;
                }
            }
            line = readIn.ReadLine( );
            while(line != null) // Read each line and add to the corresponding grid slot.
            {
                partition = line.Split(',');
                int t = Search(names, partition[partition.Length - 1]); // Find the owner of the bill.
                for(int i = 0; i < numbP; i++)
                {
                    payment[i,t] += double.Parse(partition[i]); // Add to the grid slot for how much each person owns the owner of the bill.
                }
                line = readIn.ReadLine( );
            }
            Display(names,payment);
            for(int i = 0; i < numbP - 1; i++)  // Go through the grid in an efficient manner to determine and display net payment 
                                                // between each participant.
            {
                for(int j = i; j < numbP; j++)
                {
                    if(i==j){}
                    else if(payment[i,j] > payment[j,i])
                    {
                        Console.WriteLine("{0} needs to pay {1} {2:C}", 
                            names[i], names[j], payment[i,j] - payment[j,i]);
                    }
                    else if(payment[i,j] < payment[j,i])
                    {
                        Console.WriteLine("{0} needs to pay {1} {2:C}", 
                            names[j], names[i], payment[i,j] - payment[j,i]);
                    }
                    else
                    {
                        Console.WriteLine("{0} and {1} don't need to pay " +
                        "each other", names[j], names[i]);
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Error! The program is exiting.");
            Console.WriteLine("Something went wrong when processing the " + 
                "info file.");
        }
    }
    // Search method that will return the index value of the target inside an array.
    public static int Search(string[ ] items, string target)
    {
        for(int i =0; i < items.Length; i++)
        {
            if(items[i] == target)
            {
                return i;
            }
        }
        return -1;
    }
    // Method that will printout the grid system used to calculate net payment for verification purposes.
    public static void Display(string[ ] names, double[ , ] payment) 
    {
        Console.Write("          ");
        for(int i = 0; i < names.Length; i++)
        {
            Console.Write("{0,-12}", names[i]);
        }
        Console.WriteLine( );
        for(int i = 0; i < names.Length; i++)
        {
            Console.Write("{0,-10}", names[i]);
            for(int j = 0; j < names.Length; j++)
            {
                Console.Write("{0,-12:C}", payment[i,j]);
            }
            Console.WriteLine( );
        }
        Console.WriteLine( );
    }
}