# From https://github.com/ebekker/ACMESharp/wiki/Quick-Start

$dns = "xxx.xxdev.me"
$alias = "xxxlocal"
$cert = "xxxcert"

Import-Module ACMESharp

# One time
Initialize-ACMEVault
New-ACMERegistration -Contacts mailto:xxxxxxx@gmail.com -AcceptTos

# Every time
# (4) Submit a new Domain Identifier
New-ACMEIdentifier -Dns $dns -Alias $alias

# (5) Handle the Challenge to Prove Domain Ownership
Complete-ACMEChallenge $alias -ChallengeType dns-01 -Handler manual

# (6) Submit the Challenge Response to Prove Domain Ownership
Submit-ACMEChallenge $alias -ChallengeType dns-01

# (6b) Check the Status of the Challenge
(Update-ACMEIdentifier $alias -ChallengeType dns-01).Challenges
Update-ACMEIdentifier $alias

# (7) Request and Retrieve the Certificate
New-ACMECertificate $alias -Generate -Alias $cert
Submit-ACMECertificate $cert

# (7a) Wait for serial number
Update-ACMECertificate $cert

# Export PKCS#12 (PFX) Archive
Get-ACMECertificate $cert -ExportPkcs12 "c:\temp\$cert.pfx"
