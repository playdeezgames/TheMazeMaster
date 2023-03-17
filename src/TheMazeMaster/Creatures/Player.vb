Friend Module Player
    Friend character As Character
    Friend Sub GeneratePlayer(maze As Maze)
        character = New Character(AllCreatureTypes(CreatureTypeIdentifier.Dude).Generate(maze))
    End Sub
End Module
