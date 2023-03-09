Friend Module Maze
    Friend Const MAZE_COLUMNS = 8
    Friend Const MAZE_ROWS = 8
    Friend Const MAZE_CELL_OUTSIDE = 0
    Friend Const MAZE_CELL_FRONTIER = 1
    Friend Const MAZE_CELL_INSIDE = 2
    Friend MAZE_CELL_DOORS(MAZE_COLUMNS - 1, MAZE_ROWS - 1, DIRECTION_COUNT - 1) As Boolean
    Private MAZE_CELL_STATES(MAZE_COLUMNS - 1, MAZE_ROWS - 1) As Integer
    Private CANDIDATES(DIRECTION_COUNT - 1) As Boolean

    Friend Sub Generate()
        Clear()
        Dim COLUMN = Rnd(0, MAZE_COLUMNS - 1)
        Dim ROW = Rnd(0, MAZE_ROWS - 1)
        Dim d As Integer
        Dim NEXT_COL As Integer
        Dim NEXT_ROW As Integer
        MAZE_CELL_STATES(COLUMN, ROW) = MAZE_CELL_INSIDE
        For d = DIRECTION_FIRST To DIRECTION_LAST
            NEXT_COL = STEP_X(d, COLUMN, ROW)
            NEXT_ROW = STEP_Y(d, COLUMN, ROW)
            If NEXT_COL >= 0 AndAlso NEXT_ROW >= 0 AndAlso NEXT_COL < MAZE_COLUMNS AndAlso NEXT_ROW < MAZE_ROWS Then
                MAZE_CELL_STATES(NEXT_COL, NEXT_ROW) = MAZE_CELL_FRONTIER
            End If
        Next
        Do
            Do
                COLUMN = Rnd(0, MAZE_COLUMNS - 1)
                ROW = Rnd(0, MAZE_ROWS - 1)
            Loop Until MAZE_CELL_STATES(COLUMN, ROW) = MAZE_CELL_FRONTIER
            For d = DIRECTION_FIRST To DIRECTION_LAST
                CANDIDATES(d) = False
                NEXT_COL = STEP_X(d, COLUMN, ROW)
                NEXT_ROW = STEP_Y(d, COLUMN, ROW)
                If NEXT_COL >= 0 AndAlso NEXT_ROW >= 0 AndAlso NEXT_COL < MAZE_COLUMNS AndAlso NEXT_ROW < MAZE_ROWS Then
                    If MAZE_CELL_STATES(NEXT_COL, NEXT_ROW) = MAZE_CELL_INSIDE Then
                        CANDIDATES(d) = True
                    End If
                End If
            Next
            Do
                d = Rnd(DIRECTION_FIRST, DIRECTION_LAST)
            Loop Until CANDIDATES(d) = True
            MAZE_CELL_STATES(COLUMN, ROW) = MAZE_CELL_INSIDE
            MAZE_CELL_DOORS(COLUMN, ROW, d) = True
            NEXT_COL = STEP_X(d, COLUMN, ROW)
            NEXT_ROW = STEP_Y(d, COLUMN, ROW)
            MAZE_CELL_DOORS(NEXT_COL, NEXT_ROW, OPPOSITE_DIRECTION(d)) = True
            For d = DIRECTION_FIRST To DIRECTION_LAST
                NEXT_COL = STEP_X(d, COLUMN, ROW)
                NEXT_ROW = STEP_Y(d, COLUMN, ROW)
                If NEXT_COL >= 0 AndAlso NEXT_ROW >= 0 AndAlso NEXT_COL < MAZE_COLUMNS AndAlso NEXT_ROW < MAZE_ROWS Then
                    If MAZE_CELL_STATES(NEXT_COL, NEXT_ROW) = MAZE_CELL_OUTSIDE Then
                        MAZE_CELL_STATES(NEXT_COL, NEXT_ROW) = MAZE_CELL_FRONTIER
                    End If
                End If
            Next
        Loop Until IsGenerated()
        DumpMaze()
    End Sub

    Private Sub DumpMaze()
        For row = 0 To MAZE_ROWS - 1
            For column = 0 To MAZE_COLUMNS - 1
                Console.Write("#")
                If MAZE_CELL_DOORS(column, row, DIRECTION_NORTH) Then
                    Console.Write(" ")
                Else
                    Console.Write("#")
                End If
                Console.Write("#")
            Next
            Console.WriteLine()
            For column = 0 To MAZE_COLUMNS - 1
                If MAZE_CELL_DOORS(column, row, DIRECTION_WEST) Then
                    Console.Write(" ")
                Else
                    Console.Write("#")
                End If
                Console.Write(" ")
                If MAZE_CELL_DOORS(column, row, DIRECTION_EAST) Then
                    Console.Write(" ")
                Else
                    Console.Write("#")
                End If
            Next
            Console.WriteLine()
            For column = 0 To MAZE_COLUMNS - 1
                Console.Write("#")
                If MAZE_CELL_DOORS(column, row, DIRECTION_SOUTH) Then
                    Console.Write(" ")
                Else
                    Console.Write("#")
                End If
                Console.Write("#")
            Next
            Console.WriteLine()
        Next
        Console.ReadLine()
    End Sub

    Private Function IsGenerated() As Boolean
        For COLUMN = 0 To MAZE_COLUMNS - 1
            For ROW = 0 To MAZE_ROWS - 1
                If MAZE_CELL_STATES(COLUMN, ROW) <> MAZE_CELL_INSIDE Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function

    Private Sub Clear()
        For COLUMN = 0 To MAZE_COLUMNS - 1
            For ROW = 0 To MAZE_ROWS - 1
                For d = DIRECTION_FIRST To DIRECTION_LAST
                    MAZE_CELL_DOORS(COLUMN, ROW, d) = False
                Next
            Next
        Next
    End Sub
    Friend Function GET_MAZE_CELL_EXITS(MX As Integer, MY As Integer) As Integer
        Dim C As Integer = 0
        For d = DIRECTION_FIRST To DIRECTION_LAST
            If MAZE_CELL_DOORS(MX, MY, d) Then
                C += 1
            End If
        Next
        Return C
    End Function

End Module


