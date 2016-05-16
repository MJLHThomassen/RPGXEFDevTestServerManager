RPGXEFGamePath=/home/vagrant/RPGXEF
RPGXEFSrcPath=/vagrant/RPGXEFSrc
RPGXEFBuidHistoryPath=/vagrant/RPGXEFBuildHistory
BuildMode=release

# Navigate to the source directory
cd $RPGXEFSrcPath 

# Create build history directory
CommitHash=$(git rev-parse --short HEAD)
CommitPath=$RPGXEFBuidHistoryPath/$CommitHash
if [ -d "$CommitPath" ]; then
    rm -rf "$CommitPath"
fi

mkdir $CommitPath

# Create a directory for the binaries
HistoryBinariesPath=$CommitPath/binaries
mkdir $HistoryBinariesPath
mkdir $HistoryBinariesPath/linux_x64
mkdir $HistoryBinariesPath/linux_x64/RPG-X2
mkdir $HistoryBinariesPath/linux_x86
mkdir $HistoryBinariesPath/linux_x86/RPG-X2
mkdir $HistoryBinariesPath/windows_x86
mkdir $HistoryBinariesPath/windows_x86/RPG-X2

########################################################
#
# Build 64-Bits Linux Binaries
#
########################################################
echo "Building 64-Bits Linux Binaries" | tee $CommitPath/status.txt
make $BuildMode 2>&1 | tee $HistoryBinariesPath/buildlog.txt

RESULT="${PIPESTATUS[0]}"

echo "Building Linux Binaries exited with code $RESULT"

if [ "$RESULT" -ne 0 ]; then
    echo "Building 64-Bits Linux Binaries Failed" | tee $CommitPath/status.txt
    exit $RESULT # Only exit here, no linux binaries means failed build since the server can't start
else
    # Copy binaries to the RPGXEF binaries history directory
    OS=linux
    ARCH=x86_64
    EXE=
    DLL=.so
    BinDir=$RPGXEFSrcPath/build/$BuildMode-$OS-$ARCH
    cp -f $BinDir/{rpgxEF.$ARCH$EXE,rpgxEFded.$ARCH$EXE,renderer_opengl1_$ARCH$DLL} $HistoryBinariesPath/linux_x64/
    cp -f $BinDir/rpgxEF/{cgame$ARCH$DLL,qagame$ARCH$DLL,ui$ARCH$DLL} $HistoryBinariesPath/linux_x64/RPG-X2/

    # Zip the binaries for easy distribution
    pushd $HistoryBinariesPath/linux_x64/
    zip -r ../linux_x64.zip ./
    popd
fi

########################################################
#
# Build 32-Bits Linux Binaries
#
########################################################
echo "Building 32-Bits Linux Binaries" | tee $CommitPath/status.txt
./make-i386.sh 2>&1 | tee $HistoryBinariesPath/buildlog.txt

RESULT="${PIPESTATUS[0]}"

echo "Building 32-Bits Linux Binaries exited with code $RESULT"

if [ "$RESULT" -eq 0 ]; then
    # Copy binaries to the RPGXEF binaries history directory
    OS=linux
    ARCH=x86
    EXE=
    DLL=.so
    BinDir=$RPGXEFSrcPath/build/$BuildMode-$OS-$ARCH
    cp -f $BinDir/{rpgxEF.$ARCH$EXE,rpgxEFded.$ARCH$EXE,renderer_opengl1_$ARCH$DLL} $HistoryBinariesPath/linux_x86/
    cp -f $BinDir/rpgxEF/{cgame$ARCH$DLL,qagame$ARCH$DLL,ui$ARCH$DLL} $HistoryBinariesPath/linux_x86/RPG-X2/

    # Zip the binaries for easy distribution
    pushd $HistoryBinariesPath/linux_x86/
    zip -r ../linux_x86.zip ./
    popd
fi

########################################################
#
# Build 32-Bits Windows Binaries
#
########################################################
echo "Building 32-Bits Windows Binaries" | tee $CommitPath/status.txt
./cross-make-mingw.sh 2>&1 | tee -a $HistoryBinariesPath/buildlog.txt

RESULT="${PIPESTATUS[0]}"

echo "Building 32-Bits Windows Binaries exited with code $RESULT"

if [ "$RESULT" -eq 0 ]; then
    # Copy binaries to the RPGXEF binaries history directory
    OS=mingw32
    ARCH=x86
    EXE=.exe
    DLL=.dll
    BinDir=$RPGXEFSrcPath/build/$BuildMode-$OS-$ARCH
    cp -f $BinDir/{rpgxEF.$ARCH$EXE,rpgxEFded.$ARCH$EXE,renderer_opengl1_$ARCH$DLL} $HistoryBinariesPath/windows_x86/
    cp -f $BinDir/rpgxEF/{cgame$ARCH$DLL,qagame$ARCH$DLL,ui$ARCH$DLL} $HistoryBinariesPath/windows_x86/RPG-X2/

    # Zip the binaries for easy distribution
    pushd $HistoryBinariesPath/windows_x86/
    zip -r ../windows_x86.zip ./
    popd
fi

########################################################
#
# Start game server with new binaries
#
########################################################

echo "Starting Server" | tee $CommitPath/status.txt

# Stop game server
PID=$(pidof -x rpgxEFded.x86_64)
kill $PID

# Copy new binaries
OS=linux
ARCH=x86_64
EXE=
DLL=.so
BinDir=$RPGXEFSrcPath/build/$BuildMode-$OS-$ARCH
cp -f $BinDir/{rpgxEF.$ARCH$EXE,rpgxEFded.$ARCH$EXE,renderer_opengl1_$ARCH$DLL} $RPGXEFGamePath/
cp -f $BinDir/rpgxEF/{cgame$ARCH$DLL,qagame$ARCH$DLL,ui$ARCH$DLL} $RPGXEFGamePath/RPG-X2/

# Start game server in seperate window
daemon -- xfce4-terminal -x $RPGXEFGamePath/rpgxEFded.x86_64

echo "Server Running" | tee $CommitPath/status.txt