Friend Module Creatures
    Friend AllCreatures As New List(Of Creature)
    Friend Sub Generate()
        CLEAR_CREATURES()
        For Each entry In AllCreatureTypes
            Dim SC = entry.Value.SpawnCount
            While SC > 0
                GENERATE_CREATURE(entry.Key)
                SC -= 1
            End While
        Next
    End Sub
    'TODO: move to creature type
    Friend Function GENERATE_CREATURE(cT As CreatureTypeIdentifier) As Integer
        Dim L = AllCreatureTypes(cT).MinimumExitCount
        Dim H = AllCreatureTypes(cT).MaximumExitCount
        Dim LX = AllCreatureTypes(cT).MinimumX
        Dim LY = AllCreatureTypes(cT).MinimumY
        Dim HX = AllCreatureTypes(cT).MaximumX
        Dim HY = AllCreatureTypes(cT).MaximumY
        Dim e As Integer
        Dim x As Integer
        Dim y As Integer
        Dim mx As Integer
        Dim m_y As Integer
        Do
            mx = Rnd(0, MAZE_COLUMNS - 1)
            m_y = Rnd(0, MAZE_ROWS - 1)
            e = GET_MAZE_CELL_EXITS(mx, m_y)
            Dim ti As Integer
            Dim cti As Integer
            Do
                x = Rnd(0, ROOM_COLUMNS - 1)
                y = Rnd(0, ROOM_ROWS - 1)
                Dim RM = GET_ROOM_MAP(mx, m_y)
                ti = MGET(RM, 1, x, y)
                cti = MGET(RM, 2, x, y)
            Loop Until ti = TILE_FLOOR And cti = TILE_EMPTY And x >= LX And x <= HX And y >= LY And y <= HY
        Loop Until e >= L And e <= H
        Return AllCreatureTypes(cT).Create(mx, m_y, x, y)
    End Function
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
