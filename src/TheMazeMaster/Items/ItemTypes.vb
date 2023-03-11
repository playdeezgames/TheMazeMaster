Friend Module ItemTypes
    Friend AllItemTypes As IReadOnlyDictionary(Of String, ItemType) =
        New Dictionary(Of String, ItemType) From
        {
            {
                ITEMTYPE_FIST,
                New ItemType(
                    "Fist",
                    attackValue:=1,
                    attackMaximum:=1)
            },
            {
                ITEMTYPE_BITE,
                New ItemType(
                    "Bite",
                    attackValue:=1,
                    attackMaximum:=1)
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
    Friend ReadOnly ALL_ITEMTYPES As IReadOnlyList(Of String) =
        New List(Of String) From
        {
            ITEMTYPE_FIST,
            ITEMTYPE_BITE,
            ITEMTYPE_RATTAIL
        }
End Module

