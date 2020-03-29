Write-Host "Cleaning up..."

$foldersToRemove =
	 "bin",
	 "obj",
	 "TestResults",
	 "_ReSharper.*"

$filesToRemove = 
	"Thumbs.db",
	"*.suo",
	"*.user",
	"*.cache",
	"*.scc",
	"*.vssscc",
	"*.vspscc"

#Remove Folders
Get-ChildItem .\ -include $foldersToRemove -force -recurse |
	where { $_.PsIsContainer } |
	foreach ($_) {
		Write-Host "  Removing folder ./$($_.Name)"
		Write-Host "  Removing folder ./$($_.Name)"
		Remove-Item $_.FullName -force -recurse
	}

#Remove Files
Get-ChildItem .\ -include $filesToRemove -force -recurse |
	foreach ($_) {
		Write-Host "  Removing file ./$($_.Name)"
		Remove-Item $_.FullName -force -recurse
	}

Write-Host "Done. Press any key to close..."
[void][System.Console]::ReadKey($true)