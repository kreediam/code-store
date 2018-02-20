vmx="/users/kevinreed/VMs/Windows 10 x64.vmwarevm/Windows 10 x64.vmx"
name=`date "+%Y-%m-%d"`

"/Applications/VMware Fusion.app/Contents/Library/vmrun" snapshot "$vmx" $name
