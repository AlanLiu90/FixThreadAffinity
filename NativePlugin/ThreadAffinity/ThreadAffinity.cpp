#ifdef __ANDROID__
#include <sys/syscall.h>

extern "C" int NativeSetThreadAffinity(int tid, int mask)
{
    return syscall(__NR_sched_setaffinity, static_cast<pid_t>(tid), sizeof(mask), &mask);
}

#endif
