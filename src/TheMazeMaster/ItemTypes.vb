Friend Module ItemTypes
    Friend Const ITEMTYPE_NONE = ""
    Friend Const ITEMTYPE_FIST = "FIST"
    Friend Const ITEMTYPE_BITE = "BITE"
    Friend Const ITEMTYPE_RATTAIL = "RATTAIL"
    Friend ReadOnly ITEMTYPE_TILE_INDICES As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {ITEMTYPE_FIST, TILE_EMPTY},
            {ITEMTYPE_BITE, TILE_EMPTY},
            {ITEMTYPE_RATTAIL, TILE_RATTAIL}
        }

    Friend ReadOnly ITEMTYPE_ATTACK_VALUE As IReadOnlyDictionary(Of String, Integer) = New Dictionary(Of String, Integer) From
        {
            {ITEMTYPE_FIST, 1},
            {ITEMTYPE_BITE, 1}
        }
    Friend ReadOnly ITEMTYPE_ATTACK_MAXIMUM As IReadOnlyDictionary(Of String, Integer) = New Dictionary(Of String, Integer) From
        {
            {ITEMTYPE_FIST, 1},
            {ITEMTYPE_BITE, 1}
        }
    Friend Function ITEMTYPE_ROLL_ATTACK(IT As String) As Integer
        Dim D = 0
        If ITEMTYPE_ATTACK_VALUE.ContainsKey(IT) Then
            D = ITEMTYPE_ATTACK_VALUE(IT)
        End If
        Dim M = 0
        If ITEMTYPE_ATTACK_MAXIMUM.ContainsKey(IT) Then
            M = ITEMTYPE_ATTACK_MAXIMUM(IT)
        End If
        Return ROLL_DICE(D, M)
    End Function
End Module
' ALL_ITEMTYPES=LIST(ITEMTYPE_FIST,ITEMTYPE_BITE,ITEMTYPE_RATTAIL)
' ITEMTYPE_NAMES=DICT(ITEMTYPE_FIST,"FIST",ITEMTYPE_BITE,"BITE",ITEMTYPE_RATTAIL,"RAT TAIL")
' ITEMTYPE_STACKS=LIST(ITEMTYPE_RATTAIL)
