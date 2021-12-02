<div id="top"></div>
<!--

<!-- PROJECT SHIELDS -->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
<h3 align="center">HomeDevices.Net Server</h3>

  <p align="center">
    This is the device server project. Server aims to read data from various devices and save data to database. It also includes charts to report data from database and tools to edit data in database.
    <br />
    <a href="https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/issues">Report Bug</a>
    ·
    <a href="https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

![Server Screen Shot][product-screenshot]

### Charts

![Server Screen Shot][product-screenshot-2]

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

* [.NET](https://dotnet.microsoft.com/)
* [Chart.js](https://www.chartjs.org/)
* [Bootstrap](https://getbootstrap.com)
* [JQuery](https://jquery.com)
* [BleReader.Net](https://github.com/ilpork/BleReader.Net)
* [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

#### Hardware

You need the computer, that can run Linux and has Bluetooth and network connections. You also need atleast one [RuuviTag](https://ruuvi.com/ruuvitag/) to get data. Server currently supports only RuuviTags.

#### Software

For working server enviroment you need:

* Linux opereting system
* Webserver software that can run `ASP.NET` application (NGINX, Apache)
* MySql database server
* `ASP.NET` Core Runtime 6

For building the code you need .NET SDK 6.0 or Visual Studio 2021.

### Installation

1. Clone the repo

   ```sh
   git clone https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net.git
   ```

2. Build the code with commandline or build solution with Visual Studio 2021

 ```sh
   dotnet build 
   ```

3. Publish solution from commandline or with Visual Studio Publish...

 ```sh
   dotnet publish -c release
   ```

4. Copy publish folder to your server, in the folder that is configured to run your ASP.NET server

5. Create MySQL database and run [sql script](../sql/server_db.sql) to create tables.

6. Open the website and register as new user

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Jari Kaipio - jari@kaipio.com

Project Link: [https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net](https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/T-mi-Jari-Kaipio/HomeDevices.Net.svg?style=for-the-badge
[contributors-url]: https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/T-mi-Jari-Kaipio/HomeDevices.Net.svg?style=for-the-badge
[forks-url]: https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/network/members
[stars-shield]: https://img.shields.io/github/stars/T-mi-Jari-Kaipio/HomeDevices.Net.svg?style=for-the-badge
[stars-url]: https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/stargazers
[issues-shield]: https://img.shields.io/github/issues/T-mi-Jari-Kaipio/HomeDevices.Net.svg?style=for-the-badge
[issues-url]: https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/issues
[license-shield]: https://img.shields.io/github/license/T-mi-Jari-Kaipio/HomeDevices.Net.svg?style=for-the-badge
[license-url]: https://github.com/T-mi-Jari-Kaipio/HomeDevices.Net/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[product-screenshot]: images/ui_1.png
[product-screenshot-2]: images/ui_2.png
