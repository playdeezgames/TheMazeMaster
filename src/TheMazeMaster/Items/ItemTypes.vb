Friend Module ItemTypes
    Friend AllItemTypes As IReadOnlyDictionary(Of ItemTypeIdentifier, ItemType) =
        New Dictionary(Of ItemTypeIdentifier, ItemType) From
        {
            {
                ItemTypeIdentifier.Fist,
                New ItemType(
                    "Fist",
                    attackValue:=1,
                    attackMaximum:=1)
            },
            {
                ItemTypeIdentifier.Bite,
                New ItemType(
                    "Bite",
                    attackValue:=1,
                    attackMaximum:=1)
            },
            {
                ItemTypeIdentifier.RatTail,
                New ItemType(
                    "Rat Tail",
                    stacks:=True,
                    tileIndex:=TILE_RATTAIL)
            }
        }
End Module

