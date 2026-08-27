#!/bin/sh
printf '\033c\033]0;%s\a' TheFall
base_path="$(dirname "$(realpath "$0")")"
"$base_path/TheFall_demo_build_0.1.5.x86_64" "$@"
