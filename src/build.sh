
echo Build platform: $TARGETPLATFORM
if [ $TARGETPLATFORM = "linux/amd64" ]
then
  dotnet publish --self-contained -o /build --runtime linux-x64
elif [ $TARGETPLATFORM = "linux/arm64" ]
then
  dotnet publish --self-contained -o /build --runtime linux-arm64
fi