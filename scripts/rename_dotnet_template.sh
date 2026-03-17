#!/bin/bash
# rename_dotnet_template.sh

if [ "$#" -ne 1 ]; then
  echo "Usage: $0 NewName"
  exit 1
fi

OLD="Stambat"
NEW="$1"
IGNORE_PATHS="-not -path '*/.*' -not -path '*/bin/*' -not -path '*/obj/*'"

echo "Replacing '$OLD' with '$NEW'..."

########################################
# 1. Replace text inside files (Do this FIRST)
########################################
echo "Updating file contents..."
find . -type f $IGNORE_PATHS -print0 | xargs -0 grep -lZ "$OLD" | xargs -0 sed -i "s/${OLD}/${NEW}/g"

########################################
# 2. Rename EVERYTHING (Deepest first)
########################################
# By using -depth and processing both files and dirs in one pass,
# we ensure we rename "Stambat.Domain/Stambat.Domain.csproj" 
# to "Stambat.Domain/NewName.Domain.csproj" BEFORE 
# we rename the parent folder to "NewName.Domain/".
echo "Renaming files and directories..."
find . -depth $IGNORE_PATHS -name "*${OLD}*" -exec bash -c '
  for item; do
    dir=$(dirname "$item")
    base=$(basename "$item")
    new_base="${base//'$OLD'/'$NEW'}"
    new_path="${dir}/${new_base}"
    
    if [ "$item" != "$new_path" ]; then
      # Ensure we dont move a folder into itself if it already exists
      if [ -d "$new_path" ] && [ -d "$item" ]; then
        echo "Merging/Updating: $item -> $new_path"
        cp -rl "$item"/* "$new_path/" 2>/dev/null
        rm -rf "$item"
      else
        mv "$item" "$new_path"
      fi
    fi
  done
' _ {} +

########################################
# 3. Rename the .sln file
########################################
slnNewName=$(echo "$NEW" | tr '[:upper:]' '[:lower:]' | sed 's/\./-/g')
find . -maxdepth 1 -name "*.sln" -exec bash -c 'mv "$1" "${2}.sln"' _ {} "$slnNewName" \;

echo "Renaming complete. Check your Solution Explorer!"