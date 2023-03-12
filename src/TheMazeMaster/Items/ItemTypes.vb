Friend Module ItemTypes
    Friend AllItemTypes As IReadOnlyDictionary(Of ItemTypeIdentifier, ItemType) =
        New Dictionary(Of ItemTypeIdentifier, ItemType) From
        {
            {
                ItemTypeIdentifier.Fist,
                New ItemType(
                    ItemTypeIdentifier.Fist,
                    "Fist",
                    attackValue:=1,
                    attackMaximum:=1)
            },
            {
                ItemTypeIdentifier.Bite,
                New ItemType(
                    ItemTypeIdentifier.Bite,
                    "Bite",
                    attackValue:=1,
                    attackMaximum:=1)
            },
            {
                ItemTypeIdentifier.RatTail,
                New ItemType(
                    ItemTypeIdentifier.RatTail,
                    "Rat Tail",
                    stacks:=True,
                    tileIndex:=TILE_RATTAIL)
            }
        }
End Module

