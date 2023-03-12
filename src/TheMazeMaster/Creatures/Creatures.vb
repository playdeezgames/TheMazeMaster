Friend Module Creatures
    Friend AllCreatures As New List(Of Creature)
    Friend CREATURE_MAZE_COLUMN As New List(Of Integer)
    Friend CREATURE_MAZE_ROW As New List(Of Integer)
    Friend CREATURE_ROOM_COLUMN As New List(Of Integer)
    Friend CREATURE_ROOM_ROW As New List(Of Integer)
    Friend CREATURE_TYPE As New List(Of CreatureTypeIdentifier)
    Friend CREATURE_HITPOINTS As New List(Of Integer)
    Friend CREATURE_WOUNDS As New List(Of Integer)
    Friend CREATURE_WEAPONS As New Dictionary(Of Integer, Integer)
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
        Return CREATE_CREATURE(cT, mx, m_y, x, y)
    End Function

    Private Function CREATE_CREATURE(cT As CreatureTypeIdentifier, mX As Integer, m_y As Integer, x As Integer, y As Integer) As Integer
        'TODO: REUSE DEAD CREATURES WHEN POSSIBLE
        Dim I = AllCreatures.Count
        AllCreatures.Add(New Creature(mX, m_y, x, y))
        CREATURE_TYPE.Add(cT)
        CREATURE_MAZE_COLUMN.Add(mX)
        CREATURE_MAZE_ROW.Add(m_y)
        CREATURE_ROOM_COLUMN.Add(x)
        CREATURE_ROOM_ROW.Add(y)
        CREATURE_HITPOINTS.Add(AllCreatureTypes(cT).HitPoints)
        CREATURE_WOUNDS.Add(0)
        Dim WT = AllCreatureTypes(cT).DefaultWeaponType
        CREATURE_WEAPONS(I) = AllItemTypes(WT).Create
        PLACE_CREATURE(I)
        Return I
    End Function

    Private Sub PLACE_CREATURE(i As Integer)
        Dim MX = CREATURE_MAZE_COLUMN(i)
        Dim My = CREATURE_MAZE_ROW(i)
        Dim RM = GET_ROOM_MAP(MX, My)
        Dim TI = AllCreatureTypes(CREATURE_TYPE(i)).TileIndex
        MSET(RM, 2, CREATURE_ROOM_COLUMN(i), CREATURE_ROOM_ROW(i), TI)
    End Sub

    Friend Sub REMOVE_CREATURE(i As Integer)
        Dim MX = CREATURE_MAZE_COLUMN(i)
        Dim My = CREATURE_MAZE_ROW(i)
        Dim RM = GET_ROOM_MAP(MX, My)
        Dim TI = TILE_EMPTY
        MSET(RM, 2, CREATURE_ROOM_COLUMN(i), CREATURE_ROOM_ROW(i), TI)
    End Sub

    Private Sub CLEAR_CREATURES()
        AllCreatures.Clear()
        CREATURE_MAZE_COLUMN.Clear()
        CREATURE_MAZE_ROW.Clear()
        CREATURE_ROOM_COLUMN.Clear()
        CREATURE_ROOM_ROW.Clear()
        CREATURE_TYPE.Clear()
        CREATURE_HITPOINTS.Clear()
        CREATURE_WOUNDS.Clear()
        CREATURE_WEAPONS.Clear()
    End Sub
    Friend Const MOVE_SUCCESS = "SUCCESS"
    Friend Const MOVE_FIGHT = "FIGHT"
    Friend Const MOVE_PICKUP = "PICKUP"
    Friend Const MOVE_BLOCKED = "BLOCKED"
    Function MOVE_CREATURE(i As Integer, d As Integer) As String
        Dim R = MOVE_BLOCKED
        If AllCreatures(i).Alive Then
            REMOVE_CREATURE(i)
            Dim X = CREATURE_ROOM_COLUMN(i)
            Dim Y = CREATURE_ROOM_ROW(i)
            Dim NX = STEP_X(d, X, Y)
            Dim NY = STEP_Y(d, X, Y)
            Dim MX = CREATURE_MAZE_COLUMN(i)
            Dim M_Y = CREATURE_MAZE_ROW(i)
            If NX < 0 OrElse NY < 0 OrElse NX >= ROOM_COLUMNS OrElse NY >= ROOM_ROWS Then
                CREATURE_MAZE_COLUMN(i) = STEP_X(d, MX, M_Y)
                CREATURE_MAZE_ROW(i) = STEP_Y(d, MX, M_Y)
                If NX < 0 Then
                    CREATURE_ROOM_COLUMN(i) = NX + ROOM_COLUMNS
                ElseIf NX >= ROOM_COLUMNS Then
                    CREATURE_ROOM_COLUMN(i) = NX - ROOM_COLUMNS
                Else
                    CREATURE_ROOM_COLUMN(i) = NX
                End If
                If NY < 0 Then
                    CREATURE_ROOM_ROW(i) = NY + ROOM_ROWS
                ElseIf NY >= ROOM_ROWS Then
                    CREATURE_ROOM_ROW(i) = NY - ROOM_ROWS
                Else
                    CREATURE_ROOM_ROW(i) = NY
                End If
            Else
                Dim TL = GET_ROOM_TILE(MX, M_Y, NX, NY)
                If CAN_WALK_ON_TILE(TL) Then
                    TL = GET_ROOM_CREATURE_TILE(MX, M_Y, NX, NY)
                    If TL = TILE_EMPTY Then
                        CREATURE_ROOM_COLUMN(i) = NX
                        CREATURE_ROOM_ROW(i) = NY
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
            PLACE_CREATURE(i)
        End If
        Return R
    End Function
    Friend Function FIND_CREATURE(MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        For I = 0 To AllCreatures.Count - 1
            If AllCreatures(I).Alive Then
                If MX = CREATURE_MAZE_COLUMN(I) AndAlso M_Y = CREATURE_MAZE_ROW(I) AndAlso X = CREATURE_ROOM_COLUMN(I) AndAlso Y = CREATURE_ROOM_ROW(I) Then
                    Return I
                End If
            End If
        Next
        Return -1
    End Function
    Friend Function GET_CREATURE_NAME(I As Integer) As String
        Dim CT = CREATURE_TYPE(I)
        Return AllCreatureTypes(CT).Name
    End Function
    Friend Function GET_CREATURE_HEALTH(I As Integer) As Integer
        Return CREATURE_HITPOINTS(I) - CREATURE_WOUNDS(I)
    End Function
    Friend Function GET_CREATURE_XP(I As Integer) As Integer
        Dim CT = CREATURE_TYPE(I)
        Return AllCreatureTypes(CT).XP
    End Function
    Friend Function CREATURE_ROLL_ATTACK(I As Integer) As Integer
        If CREATURE_WEAPONS.ContainsKey(I) Then
            Dim W = CREATURE_WEAPONS(I)
            Return AllItems(W).RollAttack
        End If
        Return 0
    End Function
    Friend Function CREATURE_ROLL_DEFEND(I As Integer) As Integer
        'ARMOR
        Return 0
    End Function
    Friend Sub WOUND_CREATURE(I As Integer, D As Integer)
        If AllCreatures(I).Alive Then
            CREATURE_WOUNDS(I) = CREATURE_WOUNDS(I) + D
            If CREATURE_WOUNDS(I) >= CREATURE_HITPOINTS(I) Then
                AllCreatures(I).Alive = False
                CREATURE_WOUNDS(I) = CREATURE_HITPOINTS(I)
            End If
        End If
    End Sub
    Friend Sub CREATURE_DROP_ITEM(I As Integer)
        Dim CT = CREATURE_TYPE(I)
        'TODO: CHANCE OF NOT DROPPING ITEM?
        'TODO: WEIGHTED GENERATOR FOR WHAT ITEM GETS DROPPED?
        Dim IT = AllCreatureTypes(CT).Drop
        If IT = ItemTypeIdentifier.None Then
            Return
        End If
        Dim MX = CREATURE_MAZE_COLUMN(I)
        Dim M_Y = CREATURE_MAZE_ROW(I)
        Dim X = CREATURE_ROOM_COLUMN(I)
        Dim Y = CREATURE_ROOM_ROW(I)
        Dim II = AllItemTypes(IT).CreateInRoom(MX, M_Y, X, Y)
        AllItems(II).Place()
    End Sub
End Module
