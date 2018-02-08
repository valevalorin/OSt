cd ost-hook-actor
npm install
pkg -t latest-windows-x64 -o ../ost-hook-actor ./index.js

cd ../ost-sound-agent
npm install
pkg -t latest-windows-x64 -o ../ost-sound-agent ./app.js 

cd ..