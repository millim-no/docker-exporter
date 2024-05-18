
echo Building for: $TARGETPLATFORM
if [ $TARGETPLATFORM = "linux/amd64" ]
then
  dotnet publish --self-contained -o /build --runtime linux-musl-x64
elif [ $TARGETPLATFORM = "linux/arm64" ]
then
  dotnet publish --self-contained -o /build --runtime linux-musl-arm64
fi