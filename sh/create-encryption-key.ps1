[Byte[]] $bytes = 1..32
$rng = [System.Security.Cryptography.RNGCryptoServiceProvider]::new()
$rng.getbytes($bytes)
$sb = [System.Text.StringBuilder]::new()
foreach($b in $bytes)
{
    $sb.Append($b.ToString("X2"))
}
echo $sb.ToString()
