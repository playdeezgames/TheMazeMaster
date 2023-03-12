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
    'TODO: move to creature
    Friend Sub REMOVE_CREATURE(i As Integer)
        Dim MX = AllCreatures(i).MazeColumn
        Dim My = AllCreatures(i).MazeRow
        Dim RM = GET_ROOM_MAP(MX, My)
        Dim TI = TILE_EMPTY
        MSET(RM, 2, AllCreatures(i).RoomColumn, AllCreatures(i).RoomRow, TI)
    End Sub

    Private Sub CLEAR_CREATURES()
        AllCreatures.Clear()
    End Sub
    'TODO: make into enum
    Friend Const MOVE_SUCCESS = "SUCCESS"
    Friend Const MOVE_FIGHT = "FIGHT"
    Friend Const MOVE_PICKUP = "PICKUP"
    Friend Const MOVE_BLOCKED = "BLOCKED"
    'TODO: move to creature
    Function MOVE_CREATURE(i As Integer, d As Integer) As String
        Dim R = MOVE_BLOCKED
        If AllCreatures(i).Alive Then
            REMOVE_CREATURE(i)
            Dim X = AllCreatures(i).RoomColumn
            Dim Y = AllCreatures(i).RoomRow
            Dim NX = STEP_X(d, X, Y)
            Dim NY = STEP_Y(d, X, Y)
            Dim MX = AllCreatures(i).MazeColumn
            Dim M_Y = AllCreatures(i).MazeRow
            If NX < 0 OrElse NY < 0 OrElse NX >= ROOM_COLUMNS OrElse NY >= ROOM_ROWS Then
                AllCreatures(i).MazeColumn = STEP_X(d, MX, M_Y)
                AllCreatures(i).MazeRow = STEP_Y(d, MX, M_Y)
                If NX < 0 Then
                    AllCreatures(i).RoomColumn = NX + ROOM_COLUMNS
                ElseIf NX >= ROOM_COLUMNS Then
                    AllCreatures(i).RoomColumn = NX - ROOM_COLUMNS
                Else
                    AllCreatures(i).RoomColumn = NX
                End If
                If NY < 0 Then
                    AllCreatures(i).RoomRow = NY + ROOM_ROWS
                ElseIf NY >= ROOM_ROWS Then
                    AllCreatures(i).RoomRow = NY - ROOM_ROWS
                Else
                    AllCreatures(i).RoomRow = NY
                End If
            Else
                Dim TL = GET_ROOM_TILE(MX, M_Y, NX, NY)
                If CAN_WALK_ON_TILE(TL) Then
                    TL = GET_ROOM_CREATURE_TILE(MX, M_Y, NX, NY)
                    If TL = TILE_EMPTY Then
                        AllCreatures(i).RoomColumn = NX
                        AllCreatures(i).RoomRow = NY
                        R = MOVE_SUCCESS
                    Else
                        If IS_TILE_CREATURE(TL) Then
                            R = MOVE_FIGHT
                        Else
                            R = MOVE_PICKUP
                        End If
                    End If
                End If
            End If
            AllCreatures(i).Place()
        End If
        Return R
    End Function
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
    'TODO: move to creature
    Friend Function CREATURE_ROLL_ATTACK(I As Integer) As Integer
        If AllCreatures(I).Weapon.HasValue Then
            Dim W = AllCreatures(I).Weapon.Value
            Return AllItems(W).RollAttack
        End If
        Return 0
    End Function
    'TODO: move to creature
    Friend Function CREATURE_ROLL_DEFEND(I As Integer) As Integer
        'ARMOR
        Return 0
    End Function
    'TODO: move to creature
    Friend Sub WOUND_CREATURE(I As Integer, D As Integer)
        If AllCreatures(I).Alive Then
            AllCreatures(I).Wounds = AllCreatures(I).Wounds + D
            If AllCreatures(I).Wounds >= AllCreatures(I).HitPoints Then
                AllCreatures(I).Alive = False
                AllCreatures(I).Wounds = AllCreatures(I).HitPoints
            End If
        End If
    End Sub
    'TODO: move to creature
    Friend Sub CREATURE_DROP_ITEM(I As Integer)
        Dim CT = AllCreatures(I).CreatureType
        'TODO: CHANCE OF NOT DROPPING ITEM?
        'TODO: WEIGHTED GENERATOR FOR WHAT ITEM GETS DROPPED?
        Dim IT = CT.Drop
        If IT = ItemTypeIdentifier.None Then
            Return
        End If
        Dim MX = AllCreatures(I).MazeColumn
        Dim M_Y = AllCreatures(I).MazeRow
        Dim X = AllCreatures(I).RoomColumn
        Dim Y = AllCreatures(I).RoomRow
        Dim II = AllItemTypes(IT).CreateInRoom(MX, M_Y, X, Y)
        AllItems(II).Place()
    End Sub
End Module
