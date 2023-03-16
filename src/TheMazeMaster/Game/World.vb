Friend Class World
    Private rooms As List(Of Room)
    Friend Sub Start()
        Dim maze As New Maze(MAZE_COLUMNS, MAZE_ROWS)
        maze.Generate()
        rooms = GenerateRooms(maze)
        Items.Generate(maze)
        Creatures.Generate(maze)
        Player.Generate(maze)
    End Sub
    Friend Function GetRoom(column As Integer, row As Integer) As Room
        If column < 0 OrElse row < 0 OrElse column >= MAZE_COLUMNS OrElse row >= MAZE_ROWS Then
            Return Nothing
        End If
        Return rooms(column + row * MAZE_COLUMNS)
    End Function
    Private Function GenerateRooms(maze As Maze) As List(Of Room)
        rooms = New List(Of Room)
        rooms.Clear()
        Dim TEMP As Integer = 0
        For ROW = 0 To MAZE_ROWS - 1
            For COLUMN = 0 To MAZE_COLUMNS - 1
                TEMP = 0
                For Each d In AllDirections
                    If maze.GetCell(COLUMN, ROW).HasDoor(d) Then
                        TEMP = TEMP Or (1 << d)
                    End If
                Next
                Dim EXIT_COUNT As Integer = maze.GetCell(COLUMN, ROW).ExitCount
                Dim IS_CHAMBER As Boolean = False
                If EXIT_COUNT = 1 Then
                    IS_CHAMBER = True
                ElseIf EXIT_COUNT = 2 Then
                    If Rnd(0, 99) < 50 Then
                        IS_CHAMBER = True
                    End If
                ElseIf EXIT_COUNT = 3 Then
                    If Rnd(0, 99) < 75 Then
                        IS_CHAMBER = True
                    End If
                Else
                    If Rnd(0, 99) < 25 Then
                        IS_CHAMBER = True
                    End If
                End If
                Dim ROOM_MAP As MapAssetData
                If IS_CHAMBER Then
                    ROOM_MAP = CLONE(CHAMBER_MAPS(TEMP))
                Else
                    ROOM_MAP = CLONE(PASSAGEWAY_MAPS(TEMP))
                End If
                rooms.Add(New Room(ROOM_MAP.ToMap, IS_CHAMBER))
            Next
        Next
        PLACE_ROOM_DOORS(maze)
        Return rooms
    End Function
    Private Sub PLACE_ROOM_DOORS(maze As Maze)
        For MX = 0 To MAZE_COLUMNS - 1
            For M_y = 0 To MAZE_ROWS - 1
                Dim EXIT_COUNT = maze.GetCell(MX, M_y).ExitCount
                If EXIT_COUNT = 1 Then
                    Dim D As DirectionIdentifier = DirectionIdentifier.North
                    While Not maze.GetCell(MX, M_y).HasDoor(D)
                        D = D.NextDirection
                    End While
                    Dim FM As MapAssetData

                    If GetRoom(MX, M_y).IsChamber Then
                        FM = CHAMBERDOOR_MAPS(D)
                    Else
                        FM = PASSAGEWAYDOOR_MAPS(D)
                    End If
                    Dim toMap = Worlds.world.GetRoom(MX, M_y).Map
                    BlitTerrain(FM.ToMap, toMap, TerrainIdentifier.EMPTY)

                    Dim NX = D.StepX(MX)
                    Dim NY = D.StepY(M_y)
                    D = D.Opposite
                    If GetRoom(NX, NY).IsChamber Then
                        FM = CHAMBERDOOR_MAPS(D)
                    Else
                        FM = PASSAGEWAYDOOR_MAPS(D)
                    End If
                    toMap = Worlds.world.GetRoom(NX, NY).Map
                    BlitTerrain(FM.ToMap, toMap, TerrainIdentifier.EMPTY)
                End If
            Next
        Next
    End Sub
    Private Shared Sub BlitTerrain(fromMap As Map, toMap As Map, transparent As TerrainIdentifier)
        For column = 0 To toMap.Columns - 1
            For row = 0 To toMap.Rows - 1
                Dim terrain = fromMap.GetCell(column, row).Terrain
                If terrain = transparent Then
                    Continue For
                End If
                toMap.GetCell(column, row).Terrain = terrain
            Next
        Next
    End Sub
End Class
