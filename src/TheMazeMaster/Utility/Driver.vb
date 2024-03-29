﻿Friend Module Driver
    Private ReadOnly random As New Random()
    Friend Sub UpdateWith(updater As Action(Of World))
        Dim world = New World()
        Do
            updater(world)
        Loop
    End Sub
    Friend Function Rnd(low As Integer, high As Integer) As Integer
        Return random.Next(low, high + 1)
    End Function
    Friend Function Rnd(Of T)(items As IReadOnlyList(Of T)) As T
        Dim i = Rnd(0, items.Count - 1)
        Return items(i)
    End Function
    Friend Function CLONE(map As MapAssetData) As MapAssetData
        Return JsonSerializer.Deserialize(Of MapAssetData)(JsonSerializer.Serialize(map))
    End Function
    Friend Function LOAD_RESOURCE(filename As String) As MapAssetData
        Return JsonSerializer.Deserialize(Of MapAssetData)(File.ReadAllText(filename))
    End Function
    Friend Sub DrawMap(world As World, map As Map)
        For y = 0 To map.Rows - 1
            For x = 0 To map.Columns - 1
                Dim cell = map.GetCell(x, y)
                Dim ti As Integer
                If cell.Creature(world) IsNot Nothing Then
                    ti = cell.Creature(world).CreatureType.TileIndex
                ElseIf cell.Feature(world) IsNot Nothing Then
                    ti = cell.Feature(world).FeatureType.TileIndex
                ElseIf cell.ItemIndex.HasValue Then
                    ti = world.GetItem(cell.ItemIndex.Value).ItemType.TileIndex
                Else
                    ti = AllTerrains(cell.Terrain).TileIndex
                End If
                AnsiConsole.Markup(TILE_TABLE(ti))
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
