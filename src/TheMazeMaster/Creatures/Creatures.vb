Friend Module Creatures
    Friend AllCreatures As New List(Of Creature)
    Friend Sub Generate(maze As Maze)
        CLEAR_CREATURES()
        For Each entry In AllCreatureTypes
            Dim SC = entry.Value.SpawnCount
            While SC > 0
                AllCreatureTypes(entry.Key).Generate(maze)
                SC -= 1
            End While
        Next
    End Sub
    Private Sub CLEAR_CREATURES()
        AllCreatures.Clear()
    End Sub
End Module
