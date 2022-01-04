# git push 失败

## OpenSSL SSL_read: Connection was reset, errno 10054

```shell
wei@DESKTOP-Q4DR7HN MINGW64 /e/Kzone/CodeLib/ZeroToOne/DotNet/DotNetArchitector (master)

$ git push
fatal: unable to access 'https://github.com/K-Artisan/dotnet-architect-course.git/': OpenSSL SSL_read: Connection was reset, errno 10054

$ git config --global http.sslVerify "false"

$ git push
warning: ┌──────────────── SECURITY WARNING ───────────────┐
warning: │ TLS certificate verification has been disabled! │
warning: └─────────────────────────────────────────────────┘
warning: HTTPS connections may not be secure. See https://aka.ms/gcmcore-tlsverify for more information.
warning: ┌──────────────── SECURITY WARNING ───────────────┐
warning: │ TLS certificate verification has been disabled! │
warning: └─────────────────────────────────────────────────┘
warning: HTTPS connections may not be secure. See https://aka.ms/gcmcore-tlsverify for more information.
Enumerating objects: 211, done.
Counting objects: 100% (211/211), done.
Delta compression using up to 8 threads
Compressing objects: 100% (178/178), done.
Writing objects: 100% (201/201), 1.72 MiB | 1.79 MiB/s, done.
Total 201 (delta 65), reused 12 (delta 7), pack-reused 0
remote: Resolving deltas: 100% (65/65), completed with 7 local objects.
To https://github.com/K-Artisan/dotnet-architect-course.git
   2eaea28..f3fc7d8  master -> master

```

