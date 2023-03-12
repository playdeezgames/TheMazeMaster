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
    Friend Function FIND_CREATURE(MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        For I = 0 To AllCreatures.Count - 1
            If AllCreatures(I).Alive Then
                If MX = AllCreatures(I).MazeColumn AndAlso M_Y = AllCreatures(I).MazeRow AndAlso X = AllCreatures(I).RoomColumn AndAlso Y = AllCreatures(I).RoomRow Then
                    Return I
                End If
            End If
        Next
        Return -1
    End Function
End Module
