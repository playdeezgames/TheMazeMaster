Friend Class World
    Friend Sub Start()
        Dim maze As New Maze(MAZE_COLUMNS, MAZE_ROWS)
        maze.Generate()
        Rooms.Generate(maze)
        Items.Generate()
        Creatures.Generate(maze)
        Player.Generate(maze)
    End Sub
End Class
