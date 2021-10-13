max=$(git log --oneline|wc -l); for i in $(seq  $max -100 1); do echo $i; g=$(git log --reverse --oneline --skip $i -n1|perl -alne'print $F[0]'); git push origin $g:refs/heads/master; done

# https://stackoverflow.com/questions/15125862/github-remote-push-pack-size-exceeded
# https://docs.github.com/en/github/authenticating-to-github/connecting-to-github-with-ssh/about-ssh
# https://superuser.com/questions/988185/how-to-avoid-being-asked-enter-passphrase-for-key-when-im-doing-ssh-operatio
# https://docs.github.com/en/github/authenticating-to-github/connecting-to-github-with-ssh/working-with-ssh-key-passphrases
# https://docs.github.com/en/github/authenticating-to-github/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent
# https://stackoverflow.com/questions/17846529/could-not-open-a-connection-to-your-authentication-agent/4086756#4086756
