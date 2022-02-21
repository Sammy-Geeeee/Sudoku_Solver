// Sudoku Solver

// This program is to be able to take an input of a 9x9 Sudoku grid and solve it
// Other functions provided by this will be verification if the current board is okay, already complete, no good moves, or incorrect


using static System.Console;


void showSudoku(Dictionary<string, int> board)  // To print out the sudoku board, input is a board dictionary
{
    var brackets = new List<List<string>> {new List<string> {"9", "8", "7"}, new List<string> {"6", "5", "4"}, new List<string> {"3", "2", "1"},};  // This is a list of lists of strings
        // I've these as strings and not characters as it made subsequent code easier to write
    foreach (var bracket in brackets)  // To iterate through each of the inner lists
    {
        foreach (var row in bracket)  // To iterate through each item in each bracket
        {
            var columns = new List<string> {"A", "B", "C", "|", "D", "E", "F", "|", "G", "H", "I"};  // To make another list of all the columns
            foreach (var column in columns)  // Iterate through each of these columns
            {
                try  // Try block, to catch exceptions
                {
                    if (board[column + row] != 0)  // If the value of the square is not 0
                    {
                        Write(board[column + row] + " ");
                    }
                    else
                    {
                        Write(". ");
                    }
                }
                catch (KeyNotFoundException ex)  // If the key is not found in the dictionary(for the seperator lines)
                {
                    Write(column + " ");
                }
            }
            WriteLine();  // To start the new row
        }
        WriteLine("---------------------");  // To make the horizontal separators
    }
}


Dictionary<string, List<string>> conditions()  // Has a dictionary of all the conditions that need to be checked
{
    var checks = new Dictionary<string, List<string>>() {
        {"row9", new List<string>{"A9", "B9", "C9", "D9", "E9", "F9", "G9", "H9", "I9"}},
        {"row8", new List<string>{"A8", "B8", "C8", "D8", "E8", "F8", "G8", "H8", "I8"}},
        {"row7", new List<string>{"A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7", "I7"}},
        {"row6", new List<string>{"A6", "B6", "C6", "D6", "E6", "F6", "G6", "H6", "I6"}},
        {"row5", new List<string>{"A5", "B5", "C5", "D5", "E5", "F5", "G5", "H5", "I5"}},
        {"row4", new List<string>{"A4", "B4", "C4", "D4", "E4", "F4", "G4", "H4", "I4"}},
        {"row3", new List<string>{"A3", "B3", "C3", "D3", "E3", "F3", "G3", "H3", "I3"}},
        {"row2", new List<string>{"A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2", "I2"}},
        {"row1", new List<string>{"A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1", "I1"}},
        {"columnA", new List<string>{"A9", "A8", "A7", "A6", "A5", "A4", "A3", "A2", "A1"}},
        {"columnB", new List<string>{"B9", "B8", "B7", "B6", "B5", "B4", "B3", "B2", "B1"}},
        {"columnC", new List<string>{"C9", "C8", "C7", "C6", "C5", "C4", "C3", "C2", "C1"}},
        {"columnD", new List<string>{"D9", "D8", "D7", "D6", "D5", "D4", "D3", "D2", "D1"}},
        {"columnE", new List<string>{"E9", "E8", "E7", "E6", "E5", "E4", "E3", "E2", "E1"}},
        {"columnF", new List<string>{"F9", "F8", "F7", "F6", "F5", "F4", "F3", "F2", "F1"}},
        {"columnG", new List<string>{"G9", "G8", "G7", "G6", "G5", "G4", "G3", "G2", "G1"}},
        {"columnH", new List<string>{"H9", "H8", "H7", "H6", "H5", "H4", "H3", "H2", "H1"}},
        {"columnI", new List<string>{"I9", "I8", "I7", "I6", "I5", "I4", "I3", "I2", "I1"}},
        {"squareTopleft",      new List<string>{"A9", "B9", "C9", "A8", "B8", "C8", "A7", "B7", "C7"}},
        {"squareTopcentre",    new List<string>{"D9", "E9", "F9", "D8", "E8", "F8", "D7", "E7", "F7"}},
        {"squareTopright",     new List<string>{"G9", "H9", "I9", "G8", "H8", "I8", "G7", "H7", "I7"}},
        {"squareMidleft",      new List<string>{"A6", "B6", "C6", "A5", "B5", "C5", "A4", "B4", "C4"}},
        {"squareMidcentre",    new List<string>{"D6", "E6", "F6", "D5", "E5", "F5", "D4", "E4", "F4"}},
        {"squareMidright",     new List<string>{"G6", "H6", "I6", "G5", "H5", "I5", "G4", "H4", "I4"}},
        {"squareBottomleft",   new List<string>{"A3", "B3", "C3", "A2", "B2", "C2", "A1", "B1", "C1"}},
        {"squareBottomcentre", new List<string>{"D3", "E3", "F3", "D2", "E2", "F2", "D1", "E1", "F1"}},
        {"squareBottomright",  new List<string>{"G3", "H3", "I3", "G2", "H2", "I2", "G1", "H1", "I1"}},

    };
    return checks;
}


bool checkBoard(Dictionary<string, int> board)  // To check if the board is valid or not
{
    Dictionary<string, List<string>> checks = conditions();  // To make a dictionary of all the conditions that need to be checked
    var list_items = new List<int> {};  // To make a new blank list
    foreach (var values in checks.Values)  // To iterate through each of the values in the checks dictionary
    {
        foreach (var square in values)  // To iterate through each of the square names in the values list
        {
            list_items.Add(board[square]);  // To add each of the board values to the blank list
        }
        var list_items_valid = (from item in list_items where item != 0 select item).ToList();  // To make the same list, but remove the 0 values(as they're empty in our situation)
        var set_items = new HashSet<int>(list_items_valid);  // To make a set of these values, removing the duplicates
        if (list_items_valid.Count != set_items.Count)  // Check if the number of elements in the two lists is the same
        {
            WriteLine("INVALID ENTRY");
            WriteLine($"Values: {String.Join(", ", list_items_valid)}");
            WriteLine($"Locations: {String.Join(", ", values)}");
            return false;
            
        }
        list_items.Clear();  // To clear the list of all existing entries, to empty it again
    }
    return true;
}


List<string> findEmpties(Dictionary<string, int> board)  // To make a list of all the empty squares within the board
{
    var empty_squares = new List<string> {};  // Blank list to store all the empty squares in
    foreach (var kvp in board)
    {
        if (kvp.Value == 0)  // If the value is 0, we treat it as empty
        {
            empty_squares.Add(kvp.Key);  // We can then add a string of the square location to our list
        }
    }
    return empty_squares;
}


List<int> squareOptions(Dictionary<string, int> board, string square)  // To make a list of all the integers that are allowed to be in the given square in the board
{
    var base_options = Enumerable.Range(1, 9).ToList();  // A list of integers 1-9
    var found_numbers = new List<int> {};  // A blank list to store all the numbers already relevant to that square
    var checks = conditions();  // To store all of our conditions here
    foreach (var kvp in checks)  // Iterate through each of the checks needed to test
    {
        if (kvp.Value.Contains(square))  // This will only look for potential conflicts with the current square
        {
            foreach (var other_square in kvp.Value)  // To loop through each square that  is relevant
            {
                if (board[other_square] != 0)  // If the square actually has a value
                {
                    found_numbers.Add(board[other_square]);  // The numbers found are all put in the found list
                }
            }
        }
    }
    var viable_numbers = (from num in base_options where !found_numbers.Contains(num) select num).ToList();  // This will make a new list of all the values from 1-9 that aren't found in other squares
    return viable_numbers;
}


Dictionary<string, int> squareFiller(Dictionary<string, int> board)  // To fill in all the empty squares on the board
{
    var new_board = new Dictionary<string, int>(board);  // To make a copy of the existing dictionary
    var empties = findEmpties(new_board);  // To find all the empty squares on the board
    foreach (var empty in empties)  // To iterate through each of the empty squares
    {
        var options = squareOptions(new_board, empty);  // To find all the viable options for that square    
                if (options.Count == 1)  // If there is only 1 option for that square
        {
            new_board[empty] = options[0];  // Change that square to the value it needs to be
        }
    }
    return new_board;
}


Dictionary<string, int> sudokuSolver(Dictionary<string, int> board)  // To combine all the above functions to actually solve the board
{
    var empties = findEmpties(board).Count();
    if (empties > 0)
    {
        var new_board = new Dictionary<string, int>(board);
        var count = 0;
        while (empties > 0 && count < 100)
        {
            new_board = squareFiller(new_board);
            empties = findEmpties(new_board).Count();
            count++;
        }
        return new_board;
    }
    return board;
}


// The below are a bunch of sample boards of various types that will allow the program functions to be demonstrated

var grid_empty = new Dictionary<string, int>() {
    {"A9",0}, {"B9",0}, {"C9",0}, {"D9",0}, {"E9",0}, {"F9",0}, {"G9",0}, {"H9",0}, {"I9",0},
    {"A8",0}, {"B8",0}, {"C8",0}, {"D8",0}, {"E8",0}, {"F8",0}, {"G8",0}, {"H8",0}, {"I8",0},
    {"A7",0}, {"B7",0}, {"C7",0}, {"D7",0}, {"E7",0}, {"F7",0}, {"G7",0}, {"H7",0}, {"I7",0},
    {"A6",0}, {"B6",0}, {"C6",0}, {"D6",0}, {"E6",0}, {"F6",0}, {"G6",0}, {"H6",0}, {"I6",0},
    {"A5",0}, {"B5",0}, {"C5",0}, {"D5",0}, {"E5",0}, {"F5",0}, {"G5",0}, {"H5",0}, {"I5",0},
    {"A4",0}, {"B4",0}, {"C4",0}, {"D4",0}, {"E4",0}, {"F4",0}, {"G4",0}, {"H4",0}, {"I4",0},
    {"A3",0}, {"B3",0}, {"C3",0}, {"D3",0}, {"E3",0}, {"F3",0}, {"G3",0}, {"H3",0}, {"I3",0},
    {"A2",0}, {"B2",0}, {"C2",0}, {"D2",0}, {"E2",0}, {"F2",0}, {"G2",0}, {"H2",0}, {"I2",0},
    {"A1",0}, {"B1",0}, {"C1",0}, {"D1",0}, {"E1",0}, {"F1",0}, {"G1",0}, {"H1",0}, {"I1",0},
};

var grid_partial = new Dictionary<string, int>() {
    {"A9",5}, {"B9",3}, {"C9",0}, {"D9",0}, {"E9",7}, {"F9",0}, {"G9",0}, {"H9",0}, {"I9",0},
    {"A8",6}, {"B8",0}, {"C8",0}, {"D8",1}, {"E8",9}, {"F8",5}, {"G8",0}, {"H8",0}, {"I8",0},
    {"A7",0}, {"B7",9}, {"C7",8}, {"D7",0}, {"E7",0}, {"F7",0}, {"G7",0}, {"H7",6}, {"I7",0},
    {"A6",8}, {"B6",0}, {"C6",0}, {"D6",0}, {"E6",6}, {"F6",0}, {"G6",0}, {"H6",0}, {"I6",3},
    {"A5",4}, {"B5",0}, {"C5",0}, {"D5",8}, {"E5",0}, {"F5",3}, {"G5",0}, {"H5",0}, {"I5",1},
    {"A4",7}, {"B4",0}, {"C4",0}, {"D4",0}, {"E4",2}, {"F4",0}, {"G4",0}, {"H4",0}, {"I4",6},
    {"A3",0}, {"B3",6}, {"C3",0}, {"D3",0}, {"E3",0}, {"F3",0}, {"G3",2}, {"H3",8}, {"I3",0},
    {"A2",0}, {"B2",0}, {"C2",0}, {"D2",4}, {"E2",1}, {"F2",9}, {"G2",0}, {"H2",0}, {"I2",5},
    {"A1",0}, {"B1",0}, {"C1",0}, {"D1",0}, {"E1",8}, {"F1",0}, {"G1",0}, {"H1",7}, {"I1",9},
};

var grid_partial_wrong = new Dictionary<string, int>() {
    {"A9",5}, {"B9",3}, {"C9",0}, {"D9",0}, {"E9",7}, {"F9",0}, {"G9",0}, {"H9",0}, {"I9",0},
    {"A8",6}, {"B8",0}, {"C8",0}, {"D8",1}, {"E8",9}, {"F8",5}, {"G8",0}, {"H8",9}, {"I8",0},
    {"A7",0}, {"B7",9}, {"C7",8}, {"D7",0}, {"E7",0}, {"F7",0}, {"G7",0}, {"H7",6}, {"I7",0},
    {"A6",8}, {"B6",3}, {"C6",0}, {"D6",9}, {"E6",6}, {"F6",3}, {"G6",0}, {"H6",0}, {"I6",3},
    {"A5",4}, {"B5",9}, {"C5",0}, {"D5",3}, {"E5",0}, {"F5",3}, {"G5",0}, {"H5",0}, {"I5",3},
    {"A4",7}, {"B4",0}, {"C4",0}, {"D4",0}, {"E4",2}, {"F4",0}, {"G4",0}, {"H4",0}, {"I4",6},
    {"A3",0}, {"B3",6}, {"C3",0}, {"D3",0}, {"E3",0}, {"F3",9}, {"G3",2}, {"H3",8}, {"I3",9},
    {"A2",9}, {"B2",0}, {"C2",0}, {"D2",4}, {"E2",1}, {"F2",9}, {"G2",0}, {"H2",0}, {"I2",5},
    {"A1",0}, {"B1",0}, {"C1",0}, {"D1",0}, {"E1",8}, {"F1",0}, {"G1",0}, {"H1",7}, {"I1",9},
};

var grid_full = new Dictionary<string, int>() {
    {"A9",5}, {"B9",3}, {"C9",4}, {"D9",6}, {"E9",7}, {"F9",8}, {"G9",9}, {"H9",1}, {"I9",2},
    {"A8",6}, {"B8",7}, {"C8",2}, {"D8",1}, {"E8",9}, {"F8",5}, {"G8",3}, {"H8",4}, {"I8",8},
    {"A7",1}, {"B7",9}, {"C7",8}, {"D7",3}, {"E7",4}, {"F7",2}, {"G7",5}, {"H7",6}, {"I7",7},
    {"A6",8}, {"B6",5}, {"C6",9}, {"D6",7}, {"E6",6}, {"F6",1}, {"G6",4}, {"H6",2}, {"I6",3},
    {"A5",4}, {"B5",2}, {"C5",6}, {"D5",8}, {"E5",5}, {"F5",3}, {"G5",7}, {"H5",9}, {"I5",1},
    {"A4",7}, {"B4",1}, {"C4",3}, {"D4",9}, {"E4",2}, {"F4",4}, {"G4",8}, {"H4",5}, {"I4",6},
    {"A3",9}, {"B3",6}, {"C3",1}, {"D3",5}, {"E3",3}, {"F3",7}, {"G3",2}, {"H3",8}, {"I3",4},
    {"A2",2}, {"B2",8}, {"C2",7}, {"D2",4}, {"E2",1}, {"F2",9}, {"G2",6}, {"H2",3}, {"I2",5},
    {"A1",3}, {"B1",4}, {"C1",5}, {"D1",2}, {"E1",8}, {"F1",6}, {"G1",1}, {"H1",7}, {"I1",9},
};

var grid_full_wrong = new Dictionary<string, int>() {
    {"A9",5}, {"B9",3}, {"C9",4}, {"D9",6}, {"E9",7}, {"F9",8}, {"G9",9}, {"H9",1}, {"I9",2},
    {"A8",6}, {"B8",7}, {"C8",2}, {"D8",3}, {"E8",9}, {"F8",5}, {"G8",3}, {"H8",4}, {"I8",8},
    {"A7",9}, {"B7",9}, {"C7",8}, {"D7",3}, {"E7",4}, {"F7",9}, {"G7",5}, {"H7",6}, {"I7",7},
    {"A6",8}, {"B6",5}, {"C6",9}, {"D6",7}, {"E6",6}, {"F6",1}, {"G6",4}, {"H6",2}, {"I6",3},
    {"A5",4}, {"B5",2}, {"C5",9}, {"D5",8}, {"E5",5}, {"F5",3}, {"G5",7}, {"H5",9}, {"I5",9},
    {"A4",7}, {"B4",3}, {"C4",3}, {"D4",9}, {"E4",9}, {"F4",4}, {"G4",8}, {"H4",5}, {"I4",6},
    {"A3",9}, {"B3",6}, {"C3",1}, {"D3",5}, {"E3",3}, {"F3",7}, {"G3",2}, {"H3",3}, {"I3",4},
    {"A2",2}, {"B2",8}, {"C2",7}, {"D2",4}, {"E2",1}, {"F2",9}, {"G2",3}, {"H2",3}, {"I2",5},
    {"A1",3}, {"B1",4}, {"C1",5}, {"D1",2}, {"E1",8}, {"F1",6}, {"G1",1}, {"H1",7}, {"I1",9},
};


WriteLine();
foreach (var grid in new List<Dictionary<string, int>> {grid_empty, grid_partial, grid_partial_wrong, grid_full, grid_full_wrong})
{
    // This loop is to go through each above board as a demonstration
    WriteLine("*******************************************");
    showSudoku(grid);  // To show each board in it's initial state
    bool valid = checkBoard(grid);  // To check if it is in a valid state to be solved
    if (valid == true)
    {
        var solved = sudokuSolver(grid);  // To solve the board
        WriteLine("Completed board:");
        showSudoku(solved);  // To show the solved board again
        checkBoard(solved);  // double check to make sure the solved board is valid
    }
    WriteLine("*******************************************");
}
