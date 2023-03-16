Friend Module Mazes
    Friend Const MAZE_COLUMNS = 8
    Friend Const MAZE_ROWS = 8
    Friend maze As New Maze(MAZE_COLUMNS, MAZE_ROWS)

    Friend Sub Generate()
        maze.Generate()
    End Sub
    Friend Function GET_MAZE_CELL_EXITS(MX As Integer, MY As Integer) As Integer
        Dim C As Integer = 0
        For Each d In AllDirections
            If maze.GetCell(MX, MY).HasDoor(d) Then
                C += 1
            End If
        Next
        Return C
    End Function
End Module


