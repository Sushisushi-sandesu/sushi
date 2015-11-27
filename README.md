# UnityWebCamSample (Updated)

How to use the code
======
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