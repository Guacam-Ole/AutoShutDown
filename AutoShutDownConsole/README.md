# AutoShutDown
AutoShutDown is a simple (and I _mean_ simple) tool that allows you to do the following:
* ShutDown Computer when no MouseMove detected
* ShutDown Computer when Downloads are below a threshold

(You can replace "ShutDown" by "Beep" and by "Execute a program of my choice")

The idea behind this is to automatically shutdown the computer when it isn't actively used anymore (mousemove) or all downloads are completed

## Tech
Written in .NET 6. Should work on Windows (tested) and Linux (not tested). 

## Running AutoShutDown
Configuration is done by StartupParameters:

`autoshutdown /mouse=[time] /down=[minspeed] /processes=[process1,process2] /command=[beep|(command)] /params=[parameters]`


### Examples:

`autoshutdown /mouse=10`  - Shutdown if mouse hasn't been moved for 10 minutes

`autoshutdown /down=5MB`  - Shutdown id downloadspeed drops below 5 MB per second

`autoshutdown /mouse=10 /processes=NordVPN,Azureus,Notepad` - Shutdown if mouse haven't been moved for 10 minutes EXCEPT when one of the three processes is running

`autoshutdown /down=2MB /command=beep` - Beep if download falls below 2MB

`autoshutdown /mouse=3 /command=notepad /params=this is a dummy.txt` - Open 'this is a dummy.txt' with notepad after 3 minutes without mousemovements

If you combine `/mouse` and `/down` autoshutdown _first_ waits for mousemovements to stop and _after that_ checks if downloads are below the threshold



