Friend Module Worlds
    Friend world As New World
    Friend Sub Start()
        world.Start()
        Mazes.maze.Generate()
        Rooms.Generate(Mazes.maze)
        Items.Generate()
        Creatures.Generate(Mazes.maze)
        Player.Generate(Mazes.maze)
    End Sub
End Module
