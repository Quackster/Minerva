# Helios.Imager
 Imager for Habbo Hotel releases Shockwave and Flash. It uses the two projects (Badger, and Avatara) as libraries combined into one, to make the web server.

# Installing

Locate the .zip files in /tools/

Extract in same directory as what the project is running on.

Extract *figuredata-shockwave.zip* if you plan to use 2012-2007 era clothing. 

Extract *figuredata-2013.zip* if you plan to use 2013 era clothing.

You can add your own SWFs by simply replacing the SWFS in /figuredata/compiled/ and also replace the figuredata.xml.

If the **xml** and **images** folder doesn't exist, Avatara will automatically create the folders and extract the SWFs on first run, so that each subsequent run is quicker.

Run the project.

(On Linux for example)

``./AvataraWebApp --urls=http://*:8090/``

(On Windows for example)

``AvataraWebApp.exe --urls=http://*:8090/``


The endpoints will be available:

``/habbo-imaging/avatarimage``

``/habbo-imaging/badge/{badge-code}.gif``

``/habbo-imaging/badge-fill/{badge-code}.gif``

## How do I use it for my site?

You can proxy it.

An example in PHP:

```php
<?php
header ('Content-Type: image/png');
echo file_get_contents("http://127.0.0.1:8090/?" . $_SERVER['QUERY_STRING']);
?>
```
## Cloning this repository

```
$ git clone --recursive https://github.com/Quackster/Helios.Imager
```

**or**

```
$ git clone https://github.com/Quackster/Helios.Imager
$ git submodule update --init --recursive

