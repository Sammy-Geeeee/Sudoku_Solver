# Sudoku Solver

"""
This program will take an input sudoku board and output a solved board

The purpose of this program was just to see if I could implement this in a program successfully
It came out of a challenge at the end of some tutorial I was doing
"""


import tkinter as tk
from frameMain import *
from functions import *


class Window:
    def __init__(self, root, title, geometry):
        # This will set all the base info for the main window
        self.root = root
        self.root.title(title)
        self.root.geometry(geometry)

        # To allow the expandability of all the rows and columns needed
        root.columnconfigure([0], weight=1)
        root.rowconfigure([0], weight=1)

        FrameMain(root)  # To make the Main frame

        self.root.mainloop()  # To actually run the program loop


def main():
    window = Window(tk.Tk(), 'Sudoku Solver', '1200x1000')  # Main window defined here


main()


# Future Improvements
    # Would be nice to have some kind of separation between quadrants that is a little more obvious than a gap
        # The separator lines in Tkinter won't overlap one another(no display), and the canvas lines don't expand with the window
    # Need to find a way to differentiate between the numbers that are given at the start and your input numbers and incorrect entries too
