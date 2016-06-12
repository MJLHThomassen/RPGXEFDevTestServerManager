RPGXEFGamePath=/home/vagrant/RPGXEF
RPGXEFSrcPath=/vagrant/RPGXEFSrc
RPGXEFBuidHistoryPath=/vagrant/RPGXEFBuildHistory

# Navigate to the source directory
cd $RPGXEFSrcPath

CommitHash=$(git rev-parse --short HEAD)
CommitPath=$RPGXEFBuidHistoryPath/$CommitHash

########################################################
#
# Restart game server
#
########################################################

echo 0"Restarting Server" | tee $CommitPath/status.txt

# Stop game server
PID=$(pidof -x rpgxEFded.x86_64)
kill $PID

# Start game server in seperate window
daemon -- xfce4-terminal -x $RPGXEFGamePath/rpgxEFded.x86_64 +exec server.cfg

echo "Server Running" | tee $CommitPath/status.txt