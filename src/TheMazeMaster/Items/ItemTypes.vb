Friend Module ItemTypes
    Friend AllItemTypes As IReadOnlyDictionary(Of String, ItemType) =
        New Dictionary(Of String, ItemType) From
        {
            {
                ITEMTYPE_FIST,
                New ItemType("Fist")
            },
            {
                ITEMTYPE_BITE,
                New ItemType("Bite")
            },
            {
                ITEMTYPE_RATTAIL,
                New ItemType(
                    "Rat Tail",
                    stacks:=True,
                    tileIndex:=TILE_RATTAIL)
            }
        }
    Friend Const ITEMTYPE_NONE = ""
    Friend Const ITEMTYPE_FIST = "FIST"
    Friend Const ITEMTYPE_BITE = "BITE"
    Friend Const ITEMTYPE_RATTAIL = "RATTAIL"
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
    Friend ReadOnly ALL_ITEMTYPES As IReadOnlyList(Of String) =
        New List(Of String) From
        {
            ITEMTYPE_FIST,
            ITEMTYPE_BITE,
            ITEMTYPE_RATTAIL
        }
End Module

