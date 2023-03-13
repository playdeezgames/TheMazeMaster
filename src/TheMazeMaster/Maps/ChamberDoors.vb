Friend Module ChamberDoors
    Friend CHAMBERDOOR_MAPS As New List(Of MapAssetData)
    Sub Initialize()
        CHAMBERDOOR_MAPS.Clear()
        CHAMBERDOOR_MAPS.Add(LOAD_RESOURCE("Assets/CHAMBERDOOR1.map"))
        CHAMBERDOOR_MAPS.Add(LOAD_RESOURCE("Assets/CHAMBERDOOR2.map"))
        CHAMBERDOOR_MAPS.Add(LOAD_RESOURCE("Assets/CHAMBERDOOR4.map"))
        CHAMBERDOOR_MAPS.Add(LOAD_RESOURCE("Assets/CHAMBERDOOR8.map"))
    End Sub
End Module
