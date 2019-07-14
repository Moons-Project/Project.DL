rm \q "D:\Desktop\Android"
mkdir "D:\Desktop\Android"

# :: set your own Unity path
# set unity="D:\Program Files (x86)\Unity\Editor\Unity.exe"
# :: -debug or -release
# set debugParam=-debug

# set projectPath=%~dp0

# echo "Start Build Unity to Apk"

# %unity% -batchmode -projectPath %projectPath% -executeMethod CommandBuild.PreBuild %debugParam% -quit -logFile ./PreBuild.log
# %unity% -batchmode -projectPath %projectPath% -executeMethod CommandBuild.Build %debugParam% -android -quit -logFile ./BuildApk.log


echo "End Build,please see log PreBuild.log and BuildApk.log"
Pause