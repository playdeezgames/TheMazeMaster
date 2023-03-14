Friend Module Driver
    Private ReadOnly random As New Random()
    Friend Sub UpdateWith(updater As Action)
        Do
            updater()
        Loop
    End Sub
    Friend Function Rnd(low As Integer, high As Integer) As Integer
        Return random.Next(low, high + 1)
    End Function
    Friend Function CLONE(map As MapAssetData) As MapAssetData
        Return JsonSerializer.Deserialize(Of MapAssetData)(JsonSerializer.Serialize(map))
    End Function
    Friend Function LOAD_RESOURCE(filename As String) As MapAssetData
        Return JsonSerializer.Deserialize(Of MapAssetData)(File.ReadAllText(filename))
    End Function
    Friend Function MGET(map As MapAssetData, layer As Integer, x As Integer, y As Integer) As Integer
        Return map.Layers(layer).Data.Data(x + y * map.Layers(layer).Data.Width)
    End Function
    Friend Sub MSET(map As MapAssetData, layer As Integer, x As Integer, y As Integer, t As Integer)
        map.Layers(layer).Data.Data(x + y * map.Layers(layer).Data.Width) = t
    End Sub
    Friend Sub DrawMap(map As Map)
        For y = 0 To map.Rows - 1
            For x = 0 To map.Columns - 1
                Dim cell = map.GetCell(x, y)
                Dim ti As Integer
                If cell.Creature.HasValue Then
                    ti = AllCreatures(cell.Creature.Value).CreatureType.TileIndex
                ElseIf cell.Item.HasValue Then
                    ti = AllItems(cell.Item.Value).ItemType.TileIndex
                Else
                    ti = AllTerrains(cell.Terrain).TileIndex
                End If
                AnsiConsole.Markup(TILE_TABLE(Ti))
            Next
            AnsiConsole.WriteLine()
        Next
    End Sub
    Friend Sub MAP(map As MapAssetData, offsetx As Integer, offsety As Integer)
        For y = 0 To map.Layers(0).Data.Height - 1
            For x = 0 To map.Layers(0).Data.Width - 1
                Dim ch As String = "?"
                For l = 0 To map.Layers.Count - 1
                    If map.Layers(l).Type = "render" Then
                        Dim ti = MGET(map, l, x, y)
                        If ti <> TILE_EMPTY Then
                            ch = TILE_TABLE(ti)
                        End If
                    End If
                Next
                AnsiConsole.Markup(ch)
            Next
            AnsiConsole.WriteLine()
        Next
    End Sub
    Friend Function ROLL_DICE(D As Integer, M As Integer) As Integer
        Dim R = 0
        While D > 0 And R < M
            D = D - 1
            If Rnd(1, 6) = 6 Then
                R = R + 1
            End If
        End While
        Return R
    End Function
End Module
