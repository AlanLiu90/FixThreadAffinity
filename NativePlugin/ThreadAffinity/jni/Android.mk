LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)
LOCAL_ARM_MODE := arm
LOCAL_MODULE := ThreadAffinity
LOCAL_CPPFLAGS := -Wall -Wextra
LOCAL_SRC_FILES := ../ThreadAffinity.cpp

include $(BUILD_SHARED_LIBRARY)
