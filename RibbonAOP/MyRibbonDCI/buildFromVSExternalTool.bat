@echo off
echo Goto folder
%~d1
cd %1
echo current folder %1
echo .............
echo delete old exe
del /F /Q %2
echo ..............
echo Run Marvin
Marvin /debug+ -lib:Lib -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:WindowsBase.dll *.cs ".\01 Domain\00 Values\*.cs" ".\01 Domain\01 Entities\*.cs" ".\01 Domain\02 Aggregates\*.cs" ".\01 Domain\03 Context\*.cs"  ".\02 Presentation\00 View\*.cs"  ".\02 Presentation\01 Controller\*.cs"  ".\02 Presentation\02 Context\*.cs" -out:%2
echo .............
echo build debug pdb
MyMBDTesst.exe %2
echo DONE ---------------------
