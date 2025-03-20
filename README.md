# Minerva

Imager for Habbo Hotel releases Shockwave and Flash. It uses the two projects (Badger, and Avatara) as libraries combined into one, to make the web server.

It supports rendering both figures/avatars and group badges.

When used, the following endpoints will be available:

``/habbo-imaging/avatarimage``

``/habbo-imaging/badge/{badge-code}.gif``

(This is for the client **only** due to the way Shockwave handles transparency)

``/habbo-imaging/badge-fill/{badge-code}.gif``

## Download

The latest builds for Linux and Windows are found on the [latest](https://github.com/Quackster/Minerva/releases/tag/latest) tag.

| OS | Download |
|---|---|
| Linux (64-bit) | [Minerva-linux-x64.zip](https://github.com/Quackster/Minerva/releases/download/latest/Minerva-linux-x64.zip) |
| Windows (64-bit) | [Minerva-win-x64.zip](https://github.com/Quackster/Minerva/releases/download/latest/Minerva-win-x64.zip) |

This project is using both [Avatara](https://github.com/Quackster/Avatara) and [Badger](https://github.com/Quackster/Badger) as dependencies.

## Setup

To run Minerva, you need to install .NET 8 [runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) for your operating system.

Once downloaded, you may execute ``./Minerva`` (Linux) or ``Minerva.exe`` (Windows).

As soon as it starts, it will extract all the SWFS in /figuredata/ and build the XML and image files, all this will be located in /xml/ and /images/ inside /figuredata/.

Once that's done, you should see the following console output.

```
Loading flash assets...
9762 flash assets loaded
Loading figure data...
301 figure sets loaded
11 figure set types loaded
3 figure palettes loaded
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path...
```

The the following path should give the expected output:

```
http://localhost:5000/habbo-imaging/avatarimage?figure=hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80&size=b&direction=4&head_direction=4&crr=0&gesture=sml&frame=1
```

![image](https://user-images.githubusercontent.com/1328523/212457834-67011df8-db85-41da-ad71-129bd1fadd33.png)

By default the port it listens on will be 5000 but this can be changed to 8080 (or any other port for your liking) with the command line arguments ``--urls=http://*:8080/``

#### Group badges in-client setup

Minerva has a special path to use for the Shockwave client, ensure that "badge-fill" is being used.

In external_texts.txt (yes, the client reads from this file...)

Make sure the line is (with port 5000 being changed to what you are using):

```
group_logo_url_template=http://localhost:5000/habbo-imaging/badge-fill/%imagerdata%.gif
```

**Note:** if you are using Havana with the latest version (at minimum, released 14th Janurary 2023), then set as line below:

```
group_logo_url_template=http://localhost/habbo-imaging/badge-fill/%imagerdata%.gif
```

Group badges should now work in-game.

![image](https://user-images.githubusercontent.com/1328523/212458081-6f3d390b-48de-4401-bb56-8beab3250269.png)


### Adding custom clothes

1. Adding more clothes is easy, ensure /xml/ and /images/ are either empty or don't exist in /figuredata/

2. Put the SWFs that have these clothes inside /figuredata/compiled/

3. Update figuredata.xml with the one that has the new clothes inside of it

Run Minerva and it should be supported.

### How do I use it for my site written in a different language?

You can proxy it.

An example in PHP:

```php
<?php
header ('Content-Type: image/png');
echo file_get_contents("http://127.0.0.1:8090/?" . $_SERVER['QUERY_STRING']);
?>
```

An example with Nginx:

```nginx
location /imaging/ {
        proxy_pass http://127.0.0.1:8090;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        }
```
### Cloning this repository

```
$ git clone --recursive https://github.com/Quackster/Minerva
```

**or**

```
$ git clone https://github.com/Quackster/Minerva
$ git submodule update --init --recursive

