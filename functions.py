# All the functions to make this program work go in here


conditions = {  
    # This is a dictionary of all the sections we need to check on the board to see if it has been solved or not
    'row1': ['A1', 'B1', 'C1', 'D1', 'E1', 'F1', 'G1', 'H1', 'I1'],
    'row2': ['A2', 'B2', 'C2', 'D2', 'E2', 'F2', 'G2', 'H2', 'I2'],
    'row3': ['A3', 'B3', 'C3', 'D3', 'E3', 'F3', 'G3', 'H3', 'I3'],
    'row4': ['A4', 'B4', 'C4', 'D4', 'E4', 'F4', 'G4', 'H4', 'I4'],
    'row5': ['A5', 'B5', 'C5', 'D5', 'E5', 'F5', 'G5', 'H5', 'I5'],
    'row6': ['A6', 'B6', 'C6', 'D6', 'E6', 'F6', 'G6', 'H6', 'I6'],
    'row7': ['A7', 'B7', 'C7', 'D7', 'E7', 'F7', 'G7', 'H7', 'I7'],
    'row8': ['A8', 'B8', 'C8', 'D8', 'E8', 'F8', 'G8', 'H8', 'I8'],
    'row9': ['A9', 'B9', 'C9', 'D9', 'E9', 'F9', 'G9', 'H9', 'I9'],
    'columnA': ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'A8', 'A9'],
    'columnB': ['B1', 'B2', 'B3', 'B4', 'B5', 'B6', 'B7', 'B8', 'B9'],
    'columnC': ['C1', 'C2', 'C3', 'C4', 'C5', 'C6', 'C7', 'C8', 'C9'],
    'columnD': ['D1', 'D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D8', 'D9'],
    'columnE': ['E1', 'E2', 'E3', 'E4', 'E5', 'E6', 'E7', 'E8', 'E9'],
    'columnF': ['F1', 'F2', 'F3', 'F4', 'F5', 'F6', 'F7', 'F8', 'F9'],
    'columnG': ['G1', 'G2', 'G3', 'G4', 'G5', 'G6', 'G7', 'G8', 'G9'],
    'columnH': ['H1', 'H2', 'H3', 'H4', 'H5', 'H6', 'H7', 'H8', 'H9'],
    'columnI': ['I1', 'I2', 'I3', 'I4', 'I5', 'I6', 'I7', 'I8', 'I9'],
    'quadtopleft':      ['A1', 'B1', 'C1', 'A2', 'B2', 'C2', 'A3', 'B3', 'C3'],
    'quadtopcentre':    ['D1', 'E1', 'F1', 'D2', 'E2', 'F2', 'D3', 'E3', 'F3'],
    'quadtopright':     ['G1', 'H1', 'I1', 'G2', 'H2', 'I2', 'G3', 'H3', 'I3'],
    'quadmidleft':      ['A4', 'B4', 'C4', 'A5', 'B5', 'C5', 'A6', 'B6', 'C6'],
    'quadmidcentre':    ['D4', 'E4', 'F4', 'D5', 'E5', 'F5', 'D6', 'E6', 'F6'],
    'quadmidright':     ['G4', 'H4', 'I4', 'G5', 'H5', 'I5', 'G6', 'H6', 'I6'],
    'quadbottomleft':   ['A7', 'B7', 'C7', 'A8', 'B8', 'C8', 'A9', 'B9', 'C9'],
    'quadbottomcentre': ['D7', 'E7', 'F7', 'D8', 'E8', 'F8', 'D9', 'E9', 'F9'],
    'quadbottomright':  ['G7', 'H7', 'I7', 'G8', 'H8', 'I8', 'G9', 'H9', 'I9'],
}


def checkBoardValid(board):
    # This function is to find out if the board is even valid to be solved right at the start, by ensuring no duplicate values are present
    # Input will be the sudoku board, outputs will be true if okay and false plus some error message if not okay
    value_list = []  # A blank list to store all the square values in
    for set in conditions.values():  # To iterate through all of the condition sets
        for square in set:  # To iterate through all of the squares in each set
            value_list.append(board[square])  # This will add all the values of each square to this list

        value_list_entered = [value for value in value_list if value != ' '] # This will make a list of all the filled in values in the set, removes single spaces as these are considered empty here
        value_list_nonduplicates = list(dict.fromkeys(value_list_entered))  # This will remove all the duplicate values from the entered list, so we only have 1 of each number
        value_list_duplicates = value_list_entered.copy()  # To make this list again but as a new object so they don't impact one another
        for value in value_list_nonduplicates:
            value_list_duplicates.remove(value)  # This will give us the same list but only show the duplicates this time, since that is what we want to look out for

        square_list_duplicates = [square for square in board if board[square] in value_list_duplicates]  # This will find all the squares in the board that contain the duplicate value
        square_list_duplicates = [square for square in square_list_duplicates if square in set]  # This will only show the squares containing the duplicate value in the currently open set
        
        if len(value_list_entered) != len(value_list_nonduplicates): # To determine if the board is setup properly,
            print('INVALID ENTRY')
            print(f'Values: {", ".join([str(value) for value in value_list_duplicates])}')
            print(f'Locations: {", ".join(square_list_duplicates)}')
            return False

        value_list = []  # To reset the values list so we can start again with a clean list
    
    return True


def findEmpties(board):
    # This function it to collate a list of all the empty squares in the sudoku board
    # Input will be the sudoku board and the output will be a list of locations of all the empty squares
    empty_squares = []  # Empty list to store all the empty locations in
    for square, value in board.items():  # To iterate through all the squares and values in the board
        if value == ' ':  # Items filled in with a single space are treated as an empty square
            empty_squares.append(square)
    
    return empty_squares


def squareOptions(board, square):
    # This is to  find all the number options that can validly fill a square
    # Input will be the board and a square in the board, output will be all the valid options that can go in the square
    all_numbers = range(1,9+1)  # To create a list of all the possible numbers that can go in the square
    found_numbers = []  # A blank list to store all the numbers already relevant to that square
    for set_name, set in conditions.items():  # This will iterate through all the sets for related values in a sudoku board
        if square in set:  # This will check if the square listed is in a set and only go through those ones relevant to that square
            for other_square in set:  # To look through each square in the set that is relevant
                if board[other_square] != ' ':  # To only be concerned with the "filled" squares
                    found_numbers.append(board[other_square])  # This will add the value of the found squares to this list
    
    viable_numbers = [number for number in all_numbers if number not in found_numbers]  # This will make another list of all the numbers from all the options that aren't in the found numbers list
    return viable_numbers


def squareFiller(board):
    # This function is to fill in all the empty squares on the board that can be filled
    # Input will be a board object, output will also be a board, but slightly more full (Note this is done in passes, so will need to be run several times to fill a board usually)
    new_board = board.copy()  # To make a new copy of the board as it currently is
    empties = findEmpties(new_board)  # To find all the empty squares in the new board, as a list of square locations
    for empty in empties:
        options = squareOptions(new_board, empty)  # To find all the viable options for values of each square
        if len(options) == 1:  # If there is only one viable option for that square
            new_board[empty] = options[0]  # This will fill in that square on the board with the value
    
    return new_board


def sudokuSolver(board):
    # This is to fully solve the entire board with all the of other functions combined into this
    # Input will be a board object, output will be another board object but this time completed
    board_valid = checkBoardValid(board)  # To validate if the board is worth solving
    old_empties_count = len(findEmpties(board))  # To count the number of empty squares in the board
    if (board_valid == True) and (old_empties_count > 0):  # To check if the board is solvable and if there is squares to solve
        new_board = board.copy()  # To copy the previous board (base board in this case)
        new_empties_count = 100  # Initialising this variable so it can be used the first time
        while (old_empties_count > 0) and (old_empties_count != new_empties_count):  # To continue while there is more empties to fill, and the new and old boards aren't the same
            old_empties_count = len(findEmpties(new_board))
            new_board = squareFiller(new_board)  # To use the square filler function to fill squares
            new_empties_count = len(findEmpties(new_board))  # To recount how many empty squares there are

        return new_board  # This will return the new board if something was done to it
    return board  # This will just return the initial board otherwise
