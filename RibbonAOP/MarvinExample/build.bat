echo Goto folder
%~d1
cd %1
echo current folder %1
echo .............
echo delete old exe
del /F /Q %2
echo ..............
echo Run Marvin
Marvin /debug+  *.cs -out:%2
echo .............
echo build debug pdb
MyMBDTesst.exe %2
