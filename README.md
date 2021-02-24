# yak2d_android
# Error Description
Yak2d.droid project [launcher.cs] crashes when instantiating [styleeffectsstagereader]。
Crash when instantiating [styleeffectsstagereader].
crashed method: Initialise()=>CreatePipeline() Running this method crashed

# Debugging steps
1. Switch the yak2d nuget package in yak2d.droid project to the local source project
2. Prepare an Android device supporting Vulkan and run the testapp project