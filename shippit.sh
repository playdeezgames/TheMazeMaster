dotnet publish ./src/TheMazeMaster/TheMazeMaster.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./src/TheMazeMaster/TheMazeMaster.vbproj -o ./pub-windows -c Release --sc -r win-x64
butler push pub-windows thegrumpygamedev/the-maze-master:windows
butler push pub-linux thegrumpygamedev/the-maze-master:linux
git add -A
git commit -m "shipped it!"