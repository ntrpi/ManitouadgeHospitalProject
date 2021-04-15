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
    A proposed redesign of the Manitouadge General Hospital website.
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
    <li><a href="#features">Features</a>
      <ul>
        <li><a href="#feature-name-1">Feature Name 1</a></li>
        <li><a href="#donations-feature">Donations</a></li>
        <li><a href="#department-feature">Departments</a></li>
        <li><a href="#job-posting-feature">Job Postings</a></li>
        <li><a href="#invoices">Invoices</a></li>
      </ul>
    </li>
    <li><a href="#contributing">Contributions</a>
          <ul>
        <li><a href="#amanda">Amanda</a></li>
        <li><a href="#farshan">Farshan</a></li>
        <li><a href="#kyle">Kyle</a></li>
        <li><a href="#miho">Miho</a></li>
        <li><a href="#sandra">Sandra</a></li>
        <li><a href="#wafa">Wafa</a></li>
      </ul>
    </li>
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


## Features


### Feature Name 1
<description, usage, images, etc.>

### Donations Feature
The donation feature will let users (visitors) send in a donation amount to the hospital. There will be a donation button present on the top right corner of the homepage. It will also be present on the bottom of the homepage to make it easier and quick for the user to navigate to the donations page. Once redirected, the user will be sent to the main donations page where the user will be asked to fill out their general information. This is also where users are able to donate to a an event or just donate towards the hospital. 

I would have liked to implement a captcha feature to validated the user donating once the database in able to hold the data I would also like to add in a paypall API to direct users to a payment page with the values inputted in the donation form fields to create a personalized payment page giving user a feel good experience while donating.

### Department Feature
- The departments feature is directories and provides information about each department.

Visitors to the website will see a link called, “department” under For Medical Professionals section in footer and be able to click on the link and view the department information, such as department neme, categories, phone number as well as extension numbers, email address, and FAX.

The department feature will have an administrative backend that will manage the departments. Administration staff can create, display, update, delete the contents by content management system. Administrative users will have to login to the administrative side of the application and then choose “Department” from the available links on the main dashboard. On the department page, user click one of the lists to editor delete, or if it was the first time to edit the Departments, there is no list so they can create a new department.

The Job posting entity is associated with the Departments entity. These have one to many relationships. department_id in Job posting will be a foreign key to retrieve department name (and other information) for job posting

### Job Posting Feature
- The Job posting feature is that the hospital can announce the job availability and recruit the public on the home page.

Visitors to the website will see the job posting under Recruitment on the main navigation. Each job posting may include the contents such as department information, job title, job description, salary, deadline for the application. Also, from this page, visitors to the website can apply for the job (“Apply for the job” is another feature).
The job posting feature will have an administrative backend that will manage the job posting. Administration staff can create, display, update, delete the contents by content management system. Administrative users will have to login to the administrative side of the application and then choose “Job Posting” from the available links on the main dashboard. On the Job posting page, user clicks one of the lists to edit or delete, or create a new job posting.

### Invoices

In Canada we are lucky to have the amazing, publicly funded health care system that we have. Unfortunately, there are times when a visit to the hospital incurs expenses that are not covered by that system and must be paid by an individual. Some of those expenses are services, like ambulance rides or having a private room. Others can be physical products like post operative braces or supports.

Regardless of the type of expense, it makes sense to have a list of the services and products that have an additional cost. It also makes sense to have the ability to easily create invoices for those services and products, issue the invoices to a recipient, and keep track of whether they have been paid or not. This feature provides that ability.

<!-- CONTRIBUTING -->
## Contributions

### Amanda
- I met with Christine to debug and work on git/migration issues and informed the team of what I learned so they can apply the same knowledge on 04/07/2021
- Assisted Miho in debugging on 4/8/2021
- Kyle, Miho and I communicated any changes that were made to models and worked together on 04/08/2021
- I have contributed the Alerts and Events models, controllers and views

### Farshan
- Contact us model
- contact us controller
- Almost deleted wafa's work for good


### Kyle
- Testimonial Model
- Volunteer Model
- Testimonial Controller 
- Volunteer Controller
- Moral Support
- Positivity


### Miho
Department and Job Posting feature
- Department Model
- Job Posting Model
- Department Controller and Department Data Controller
- Job Posting Controller and Job Posting Data Controller
- Views and ViewModels for Department and Job Posting

All of the members have communicated well to solve the issues and helped each others.

### Sandra
- Created project and initialized repo.
- Set up everything so Entity Framework would work.
- Determined a strategy to prevent database conflicts while the models were in constant flux.
- CRUD for Products and Invoices.
- Created a ControllersHelper to reduce errors and create consistency in the interactions between view and data controllers without obscuring access to either of those endpoints.
- Changes to the IdentityModel to be able to access the Users managed by the framework.
- Created the many-to-many relationship between Products and Invoices.
- Created a RolesController to help automate the addition of roles to the database.
- Added some database seeding for Roles so that they will be consistent for everyone.
- Created readme with sections for everyone to fill out.
- Started adding views to manage the relationship between Users and Invoices.
- Successfully added client-side functionality to dynamically update the invoice view as products are added.
- Added links in the readme to the features that have been added.
- Recovered the nav that disappeared after a bunch of NuGet packages were updated.
- Added some stuff to the layout.

### Wafa
Donations Feature
- Donation Model
- Added foreign key in the events table to create one to many relation between events and donations
- Donation Data controller, Views and ViewModel connecting to Events 
- Worked with Amanda with pull and pushing issues
- Constant communication with the team about any new migrations
- Communicated with the team about code back ups in case any issues arise
- Communicated with Amanda to create a database model that will hold FK's in Events and Donations model


<!-- CONTACT -->
## Contact

- Amanda - amanda.elias@live.ca
- Farshan - farshanaslam@gmail.com
- Kyle - Cheung.kyle1@gmail.com
- Miho - mihoko.s0408@gmail.com
- Sandra - kupfer.sandra@gmail.com
- Wafa - wafamustafak@gmail.com



Project Link: [https://github.com/ntrpi/Manitouage1](https://github.com/ntrpi/Manitouage1)


<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [Christine Bittle](https://github.com/christinebittle)
* [Best README Template](https://github.com/othneildrew/Best-README-Template)




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
