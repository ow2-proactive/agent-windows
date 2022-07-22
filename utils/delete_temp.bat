echo Deleting temporary files left by ProActive Runtime PID: %1 RANK: %2 

cd %TMP%\%2
FOR /D /R %%X IN (PA_JVM*) DO RD /S /Q "%%X"