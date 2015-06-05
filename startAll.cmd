start cmd.exe /e:on /k "cd redis1 && redis-server redis.windows.conf" 
start cmd.exe /e:on /k "cd redis2 && redis-server redis.windows.conf" 
start cmd.exe /e:on /k "cd redis3 && redis-server redis.windows.conf" 
start cmd.exe /e:on /k "cd redis1 && redis-server sentinel.conf --sentinel" 
start cmd.exe /e:on /k "cd redis2 && redis-server sentinel.conf --sentinel" 
start cmd.exe /e:on /k "cd redis3 && redis-server sentinel.conf --sentinel"

pause

redis-cli -p 21001 sentinel masters 
