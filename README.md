# FixThreadAffinity
该库可用于修复部分华为机型升到鸿蒙系统后，Unity游戏出现严重掉帧的问题。

掉帧的原因是Unity 2018及以下的版本在安卓（鸿蒙）上创建了等于小核数量的工作线程，但把它们绑到了大核。在大核数量较少的设备（比如荣耀9X）上，容易出现多个线程（主线程+渲染线程+n个工作线程）抢占大核的情况，从而导致掉帧。

使用该库可以将工作线程设置为不绑核，避免它们和主线程、渲染线程抢占大核。我自己在用Unity 2017.4.40开发的游戏中测试过，理论上，用Unity 2018及以下的版本开发的游戏都有该问题。

注意：Unity 2018在安卓平台上，游戏切到后台再切回来会重新绑核，游戏需要再次使用这里的代码将工作线程设置为不绑核。
