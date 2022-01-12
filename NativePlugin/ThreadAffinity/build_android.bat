@echo off
set NDK=E:\Android\android-ndk-r14b-windows-x86_64\android-ndk-r14b\build\ndk-build.cmd

cmd /c %NDK% APP_STL=gnustl_static APP_ABI=armeabi-v7a,arm64-v8a NDK_PROJECT_PATH="%cd%"

mkdir publish\armeabi-v7a
copy /Y libs\armeabi-v7a\libThreadAffinity.so publish\armeabi-v7a

mkdir publish\arm64-v8a
copy /Y libs\arm64-v8a\libThreadAffinity.so publish\arm64-v8a

pause
