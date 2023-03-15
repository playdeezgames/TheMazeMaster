Friend Module Creatures
    Friend AllCreatures As New List(Of Creature)
    Friend Sub Generate()
        CLEAR_CREATURES()
        For Each entry In AllCreatureTypes
            Dim SC = entry.Value.SpawnCount
            While SC > 0
                AllCreatureTypes(entry.Key).Generate()
                SC -= 1
            End While
        Next
    End Sub
    Private Sub CLEAR_CREATURES()
        AllCreatures.Clear()
    End Sub
End Module
