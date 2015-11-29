# UnityWebCamSample (Updated)

##How to use the code

The first 4 steps are exactly the same as written in the ["instructions on setting up development environment" issue](https://github.com/Sushisushi-sandesu/sushi/issues/4):

1. Download and install **Unity 5.1.2 for Mac** at [Unity Downloads](http://unity3d.com/get-unity/download/archive).
2. Go to [Oculus Developer Downloads](https://developer.oculus.com/downloads/). Download and install  
    1. For Runtime, download **Oculus Runtime for OS X V0.5.0.1-beta** [here](https://developer.oculus.com/downloads/pc/0.5.0.1-beta/Oculus_Runtime_for_OS_X/);
    1. For SDK, download **Oculus SDK for OS X 0.5.0.1-beta** [here](https://developer.oculus.com/downloads/pc/0.5.0.1-beta/Oculus_SDK_for_OS_X/);
    1. Now we don't need to download "Engine Integration" since Leep Motion SDK comes with one.
3. Download and install [**Leap Motion SDK for OSX**](https://developer.leapmotion.com/)
4. Install Unity, Oculus SDK and Runtime.

---
5. After the installation of Unity. Clone this repository

```
    $ git clone https://github.com/Sushisushi-sandesu/UnityWebCamSample
    $ cd UnityWebCamSample
```

6. Open Unity. At the welcome window, click "Open Other".
7. Switch to the cloned projects directory, then click "open".
8. Now, since the Leap Motion's assets are excluded from the repo, we need to download the assets. Go to "Asset Store", search for "Leap Motion Core Assets", then click "import". When asked to update the APIs, click **"I made a backup. Go ahead"**.
9. Now the project should be able to build.

##How to set up developement environment
1. Download and install **Unity 5.1.2 for Mac** at [Unity Downloads](http://unity3d.com/get-unity/download/archive).
2. Go to [Oculus Developer Downloads](https://developer.oculus.com/downloads/). Download and install  
    1. For Runtime, download **Oculus Runtime for OS X V0.5.0.1-beta** [here](https://developer.oculus.com/downloads/pc/0.5.0.1-beta/Oculus_Runtime_for_OS_X/);
    1. For SDK, download **Oculus SDK for OS X 0.5.0.1-beta** [here](https://developer.oculus.com/downloads/pc/0.5.0.1-beta/Oculus_SDK_for_OS_X/);
    1. Now we don't need to download "Engine Integration" since Leep Motion SDK comes with one.
3. Download and install [**Leap Motion SDK for OSX**](https://developer.leapmotion.com/)
4. Install Unity, Oculus SDK and Runtime.
5. Open Unity, go to Asset Store. Search and install "Leap Motion Core Assets". When asked to update the APIs, click **"I made a backup. Go ahead"**.
5. **IMPORTANT**: On step 5, make sure you clicked **"I made a backup. Go ahead"** or the code won't compile.
6. Now the installation of development environment is completed.

---

### Side notes:
1.  According to the [Leap Motion site](https://developer.leapmotion.com/downloads/unity). One of the requirements for using the Core Assets on OSX is that the Integrated VR Support must be **turned off**. To turn off the integrated VR support:
   1. Open Unity.
   2. Go to Edit -> Project Settings -> Player.
   3. Make sure the **Virtual Reality supported** checkbox is **unchecked**.  

2. Now because of the integrated VR support must be turned off. It is not possible to build and preview the VR app inside Unity. Therefore, building a standalone executable is necessary every time we want to try out the app.  
    To build a standalone executable:
    1. Open Unity, go to the project we are building.
    2. Go to File -> Build Settings.
    3. Add the scenes to be built. (Usually "Add current" would suffice"
    4. Choose the "Target Platform" as "OSX". Choose the "Architecture" according to your computer's spec.
    5. Click "Player settings" and make sure the "Virtual Reality supported" is **Unchecked**. Otherwise the app will appear to be a total blank.
    6. Click "Build" and choose the path you want to place the executable.

### Using Git with Unity
Please refer to [this page](http://unity3diy.blogspot.jp/2014/06/using-git-with-3d-games-source-control_8.html)