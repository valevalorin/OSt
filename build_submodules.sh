cd ost-hook-actor
npm install
pkg -t latest-windows-x64 -o ../OSt/bin/Debug/ost-hook-actor ./index.js

cd ../ost-sound-agent
npm install
pkg -t latest-windows-x64 -o ../OSt/bin/Debug/ost-sound-agent ./app.js