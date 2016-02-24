# Steep
Steep is a social network for writers.
The idea behind it is the users are given the opportunity to determine how a story ends. People can create a story and assign it to a particular genre. After that everybody who likes the story can extend it as he pleases. For example, say somebody writes a great book about adventure. If someone doesn't enjoy the way it ends, they can make up they own ending and gain a following. The result is a tree-like structure of various different endings and choices characters make.

# Features:
- The application features a public (available for everyone), private (available for register users) and administrator part.
- The public part allows users to view the latest posted stories and their chapter extensions.
- The private part allow for stories and chapters to be created and also allows commenting on other posts.
- The administrator part allows for managing the database models by deleting or updating them.
- When creating a new story, the user can assign it name and genres.
- After that the user can create different chapters and set the story they are from as well as the chapter they inherit.
- When viewing story details, the user sees statistics for that story such as number of views, number of chapters etc.
- This also applies for chapter details.
- Chapter details also give comments unique for that place in time.
- Users can list all chapters.
- Data display is done mainly through Kendo UI.
- Users can leave comments on different stories and chapters.
- Users can manage their profile and view the stories / chapters they have created.
# Project Architecture
- The solution is seperated in the cohesive and loosely coupled modules. That include a data layer, a service layer and the MVC application.

## Data layer
- Data - Holds the database context, migrations plus their configuration
- Data.Common - Holds data common for multiple projects (for example the Database Repositories)
- Data.Models - Holds the database models. The database was constructed using the code-first approach with Entity Framework.

## Service layer
- Services.Data - Holds a set of services that query the database models through the repository pattern.
- Services.Web - Service helpers unique for the web part of the application.

## Web
- Web - Contains the web application logic.
- Web.Infrastructure - Contains classes the aid the infrastructure of the MVC application and don't affect it from a user point of view.
- Common - Holds a set of global variables and constants used in all projects.

## Tests
- Projects that contain the the unit tests.
