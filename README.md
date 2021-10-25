# in-team #

## Introduction ##
Welcome to this web application. Code developed by Andrew Sturman for his SENG402 final year project at the University of Canterbury. The goal of this
web application is to add game elements to software development project courses to improve team effectiveness. 

### Who will use this application ###
There are two distinct roles that will use this application. There first are project course students which have the "student" role. The second are lecturers and members of project course teaching teams, which are given the "lecturer" role. A user is able to have both of these roles.

### Basic functionality ###

#### Students ####
Students can create custom profiles, change their avatars and give badges to other team members to reward positive team attributes. They can also earn points and improving their team's position on a leaderboard. Students also answer surveys created by the teaching team to understand how they are progressing in relation to essence kernel alphas.

#### Lecturers ####
Lecturers can create courses, and teams within those courses. Courses are assigned iterations which is a general term for a period of work. For example, an iteration in the scrum proccess would be a sprint. Teachers then assign students to teams. Lecturers are able to add specific Alphas and States to the application which they can then use to create surveys with. Surveys can be customized per team and are answered by students using a Likert scale. Using the results of these surveys, teachers can award students points, and determine what states they currently meet.

### What are Alphas and States ###
Throughout the codebase you will see the terms **Alphas** and **States** mentioned. These are related to Essence Kernel Alphas, which are a way of measuring team effectiveness. To put it simply, an Alpha measures a specific area of team effectiveness. For example, the _team_ alpha measures how well a team is communicating and collaborating together, while the _stakeholders_ alpha measures how well a team is involing and utilizing their stakeholders. Each **Alpha** has a set of associated states, which track how the team is performing related to that alpha. As a team improves in relation to an Alpha, they progress their state. Each State of an alpha comes with a set of questions to determine if a team meets that state. For a more detailed explanation please see
https://www.omg.org/spec/Essence/1.0/PDF

## Technology stack ##

### Frontend ###
React and TypeScript are used to create modular components. Components live in the **client** subfolder are split into folders based on specific components of the application. ChakraUI is used as a Component/CSS framework to provide consistent styling to the application. React router is used for navigation, and Axios is used for networking. A global authentication store is used to determine the users role and authentication status on the front end.

### Backend ###
.NET 5 is used for the backend of this application. This application follows a three layer architectture. Controller classes are used to specify endpoints which the client can interact with. Service classes contain business logic, and repository classes contain methods to interact with the PostegreSQL database. Identity Framework is used with Json Web Tokens (JWTs) to control authentication and authorization.

### Database ###
PostgreSQL is used for the database. All interaction with the database is accomplished with EntityFrameworkCore (EFCore) through the .NET backend. Changes made to the structure of the database through code can be made through .NET migrations. These migrations will automatically update the production and development databases once pushed.

## Development Environment ##
This application can be run using the provided docker-compose script, or by running each piece individually. Running the docker compose script is the simplest, however running each piece individually will allow easier debugging and hot realoading. Instructions for both ways are provided below.

### Prerequisites ###
 - Docker desktop version 3.5.2.
 - Dotnet version 5.0.301
 - Node.js version 16.8.0

#### Development Environment variables ####
Provide the following user secrets using .NET secret manager
PostgresSettings:Password - The database password for local development
LecturerPassword:Password = The password required to register an account as a **Lecturer** for local development
JwtConfig:Secret = The private key the backend uses for JWT encryption for local development

#### Production Environment variables ####
If rehosting the application, you will need to provide values for the following ENV variables
DATABASE_URL - The URL string to connect to your database
JWT_KEY - The private key the backend uses for JWT encryption
LECTURER_PASSWORD - The password required to register an account as a **Lecturer**

REACT_APP_API_URL - Configure this where your frontend is deployed (Netlify in this case) to point to your production API server

### Running development without Docker Compose ###
1) Open the client directory and run the command npm run start
2) Start the database docker container through the desktop application or command line
3) Open the server/server.api directory and run the command dotnet watch run

### Running development with Docker Compose ###
1) Run the command docker-compose up from the root directory


Deployment: Docker, Github actions

## Deployment Environment ##
The frontend of this application is currently deployed to Netlify using the URL https://in-team.netlify.app/. The backend of this application is deployed to Heroku, and the PostgreSQL database instance is hosted via the Heroku-Postgres plugin. Deployments are triggered upon any **push** to the master branch of the application. When pushing, GitLab actions are also triggered that run both front and backend unit tests.

 
