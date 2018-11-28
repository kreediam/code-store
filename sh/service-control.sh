sc queryex type= service state= all | find /i "fusion"
sc queryex “Fusion ..."
sc stop “Fusion ...”

# https://stackoverflow.com/questions/13878921/how-to-get-all-windows-service-names-starting-with-a-common-word

