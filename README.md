# GPU Mining Rig Rebooter for Rpi - Windows IoT

Reboots PC / Mining rig when ping fails using relays.

![Screenshot](https://github.com/ColossusFX/RigRebooter-UWP/blob/master/Screenshot_1.jpg "GPU Rig Rebooter")

## Getting Started

Add your IP addresses of mining rigs to the collection.
Add corresponding GPIO pin
Deploy to Rpi
Set as startup app

```
MiningRigs.Add(new MiningRigs("192.168.0.24", 12));
            MiningRigs.Add(new MiningRigs("192.168.0.40", 16));
            MiningRigs.Add(new MiningRigs("192.168.0.47", 20));
            MiningRigs.Add(new MiningRigs("192.168.0.119", 21));
            MiningRigs.Add(new MiningRigs("192.168.0.246", 26));
            MiningRigs.Add(new MiningRigs("192.168.0.251", 13));
	    ```
