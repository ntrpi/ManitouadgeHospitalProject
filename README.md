<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Thanks again! Now go create something AMAZING! :D
***
***
***
*** To avoid retyping too much info. Do a search and replace for the following:
*** ntrpi, Manitouage1, twitter_handle, email, HTTP5204 Hospital Project, A proposed redesign of the Manitouage Hospital website.
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/ntrpi/Manitouage1">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">HTTP5204 Hospital Project</h3>

  <p align="center">
    A proposed redesign of the Manitouage Hospital website.s
    <br />
    <br />
    <a href="https://github.com/ntrpi/Manitouage1">View Demo</a>
    ·
    <a href="https://github.com/ntrpi/Manitouage1/issues">Report Bug</a>
    ·
    <a href="https://github.com/ntrpi/Manitouage1/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
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
    <li><a href="#contributing">Contributions</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://example.com)

Here's a blank template to get started:


### Built With

* [Visual Studio Community 2019](https://visualstudio.microsoft.com/vs/community/)


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* npm
  ```sh
  npm install npm@latest -g
  ```

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/ntrpi/Manitouage1.git
   ```
2. Install NPM packages
   ```sh
   npm install
   ```


<!-- USAGE EXAMPLES -->
## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

### <Feature Name 1>
<description, usage, images, etc.>

### <Feature Name 1>
<description, usage, images, etc.>

### Department Feature
- The departments feature is directories and provides information about each department.

Visitors to the website will see a link called, “department” under For Medical Professionals section in footer and be able to click on the link and view the department information, such as department neme, categories, phone number as well as extension numbers, email address, and FAX.

The department feature will have an administrative backend that will manage the departments. Administration staff can create, display, update, delete the contents by content management system. Administrative users will have to login to the administrative side of the application and then choose “Department” from the available links on the main dashboard. On the department page, user click one of the lists to editor delete, or if it was the first time to edit the Departments, there is no list so they can create a new department.

The Job posting entity is associated with the Departments entity. These have one to many relationships. department_id in Job posting will be a foreign key to retrieve department name (and other information) for job posting

### Job Posting Feature
- The Job posting feature is that the hospital can announce the job availability and recruit the public on the home page.

Visitors to the website will see the job posting under Recruitment on the main navigation. Each job posting may include the contents such as department information, job title, job description, salary, deadline for the application. Also, from this page, visitors to the website can apply for the job (“Apply for the job” is another feature).
The job posting feature will have an administrative backend that will manage the job posting. Administration staff can create, display, update, delete the contents by content management system. Administrative users will have to login to the administrative side of the application and then choose “Job Posting” from the available links on the main dashboard. On the Job posting page, user clicks one of the lists to edit or delete, or create a new job posting.

<!-- CONTRIBUTING -->
## Contributions

### Amanda
- I met with Christine to debug and work on git/migration issues and informed the team of what I learned so they can apply the same knowledge on 04/07/2021
- Assisted Miho in debugging on 4/8/2021
- Kyle, Miho and I communicated any changes that were made to models and worked together on 04/08/2021

### Farshan
Describe all the awesome stuff you did here.


### Kyle
- Testimonial Model
- Volunteer Model
- Testimonial Controller 
- Volunteer Controller
- Moral Support
- Positivity


### Miho
- All of the members has communicated well to solve the issues and helped each others.

### Sandra
Describe all the awesome stuff you did here.


### Wafa
Describe all the awesome stuff you did here.





<!-- CONTACT -->
## Contact

- Amanda - amanda.elias@live.ca
- Farshan - email
- Kyle - email
- Miho - mihoko.s0408@gmail.com
- Sandra - kupfer.sandra@gmail.com
- Wafa - email



Project Link: [https://github.com/ntrpi/Manitouage1](https://github.com/ntrpi/Manitouage1)


<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [Christine Bittle](https://github.com/christinebittle)
* [Dest README Template](https://github.com/othneildrew/Best-README-Template)




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/ntrpi/repo.svg?style=for-the-badge
[contributors-url]: https://github.com/ntrpi/repo/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/ntrpi/repo.svg?style=for-the-badge
[forks-url]: https://github.com/ntrpi/repo/network/members
[stars-shield]: https://img.shields.io/github/stars/ntrpi/repo.svg?style=for-the-badge
[stars-url]: https://github.com/ntrpi/repo/stargazers
[issues-shield]: https://img.shields.io/github/issues/ntrpi/repo.svg?style=for-the-badge
[issues-url]: https://github.com/ntrpi/repo/issues
[license-shield]: https://img.shields.io/github/license/ntrpi/repo.svg?style=for-the-badge
[license-url]: https://github.com/ntrpi/repo/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/ntrpi
