diskutil list
sudo umount /dev/disk3s2
# sudo diskutil unmountdisk force disk3s2
sudo dd if=~/Desktop/Windows10_x64_EN-US.iso of=/dev/rdisk3s2 bs=1m
diskutil eject /dev/(IDENTIFIER)

