
# https://stackoverflow.com/questions/521382/command-line-tool-to-delete-folder-with-a-specified-name-recursively-in-windows

for /d /r . %d in (_svn) do @if exist "%d" echo "%d"

for /d /r . %d in (_svn) do @if exist "%d" rd /s/q "%d"

