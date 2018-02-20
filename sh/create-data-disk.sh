# https://kb.vmware.com/s/article/2097401

diskutil list
/Applications/VMware\ Fusion.app/Contents/Library/vmware-rawdiskCreator print /dev/disk0
/Applications/VMware\ Fusion.app/Contents/Library/vmware-rawdiskCreator create /dev/disk0 3 ~/VMs/Windows\ 10\ x64.vmwarevm/data-disk scsi
