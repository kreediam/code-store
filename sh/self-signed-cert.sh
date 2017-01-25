# see http://stackoverflow.com/questions/19441155/how-to-create-a-self-signed-certificate-for-a-domain-name-for-development

makecert.exe -n "CN=Kreed Dev Root CA,O=ST, OU=Dev,L=GR,S=MI,C=US" -pe -ss Root -sr LocalMachine -sky exchange -m 120 -a sha1 -len 2048 -r

makecert.exe -n "CN=local.abc.com" -pe -ss My -sr LocalMachine -sky exchange -m 120 -in "Kreed Dev Root CA" -is Root -ir LocalMachine -a sha1 -eku 1.3.6.1.5.5.7.3.1
