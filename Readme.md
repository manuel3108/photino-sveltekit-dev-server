# Photiono Sveltekit Dev Server

> This is a showcase repo to demonstrate how you could start your dev server for your application framework directly from C#.

## Prerequisites

-   .Net6 installed
-   Node.js / npm installed
-   Your dependencies for your user interface are already installed (see below)

## Install user interface dependencies

Currently this demo application does not install your dependencies for your frontend framework automatically, although you might be easily able to add this in the future if you want.

In order to install them, just `cd UserInterface` and after that `npm install`.

## Run the app!

From now on, after starting the app, your app will automatically start your dev server through npm. If possible, the app detects the from url the app is running from the stdout of npm and loads the appropriate page afterwards.

**Note**: The dev server will only be started by default if you run in dev mode. In release mode, the app will use the build output from the wwwroot directory, that you can generate normally with `npm run build` (assuming you are already in the correct directory)

# Important Note:

This is a POC, so please expect that the code could be better, but I have added some comments to the tricky part.

###### Not to myself:

Here are the typings for typescript for the exposed methods, that we can use with photino:

```ts
declare global {
    interface External {
        receiveMessage: (callback: (message: string) => void) => void;
        sendMessage: (message: string) => void;
    }
}

export {};
```
