# Native AoT (Used in current pipeline)
dotnet publish "src/RetroEmu/RetroEmu.csproj" \
  -c Release \
  -r osx-arm64 \
  -o publish-aot-test \
  /p:PublishAot=true \
  /p:SelfContained=true \
  /p:InvariantGlobalization=true \
  /p:PublishTrimmed=true \
  /p:TrimMode=full \
  /p:StripSymbols=true \
  /p:DebugType=none \
  /p:DebugSymbols=false
