Technical Test – Full-Stack developer (Frontend oriented)

Dear candidate,

As part of the recruitment process at Bagira Systems, you are required to complete this assignment. You will receive the assignment at the time of your choice (e.g., 9 AM) and must submit your solution within 24 hours (e.g., by 9 AM the next day). Please read the description carefully before starting.

Objectives:
Complete the backend API layer on a provided partial backend, then design and build a frontend from scratch for a small web application used to create training scenarios and manage entities inside each scenario.

Instructions:
1.	Clone this repository – Bagira-Assignment and create a fork with the following structure – 'BagiraAssignment_<YourName>'.
2.	Make sure to work on the folder named 'Frontend-Oriented'.
3.	The backend contains pre-implemented in-memory repositories and domain models. You must implement the API controllers and complete the backend wiring.
4.	Build a new frontend application from scratch using React or Angular (your choice) and specify which one you chose.
5.	Follow the README instructions in the backend folder for detailed backend setup.

Requirements:
1.	Data rules:
•	EntityType must be one of the following - Soldier, Tank, Drone, Aircraft, Vehicle, Civilian.
•	TaskForce must be one of the following - Friendly, Enemy.
•	Latitude must be between -90 and 90.
•	Longitude must be between -180 and 180.
•	Entities always belong to exactly one Scenario (no orphan entities).
2.	Screens:
•	Scenario List – view all scenarios, navigate to details, create.
•	Create Scenario – the form should contain name and description.
•	Scenario Details – view scenario info and manage its entities.
•	Create Entity – the form must include:
o	EntityType
o	TaskForce
o	Name/Callsign
o	Latitude
o	Longitude
3.	Backend Features (partial backend provided):
•	The following are already implemented – do not modify them:
o	Domain models (Scenario, Entity, EntityType, TaskForce)
o	Repository interfaces (IRepository, IScenarioRepository, IEntityRepository)
o	DTO structure (CreateScenarioRequest, CreateEntityRequest, ScenarioListItemDto, ScenarioDetailsDto, EntityDto, ErrorResponse)
•	You must implement:
o	Data Access Layer – implement ScenarioRepository and EntityRepository with chosen data storage (In-Memory or Database). Stub implementations are provided.
o	If using database: set up DbContext, migrations, and connection strings.
o	API Controllers – implement all endpoints on ScenariosController and EntitiesController (stubs are provided).
o	DTO Validation – add validation attributes to request DTOs and other DTOs as needed.
o	Error Handling – proper HTTP status codes and ErrorResponse for validation errors.
o	Validation – server-side data validation based on the data rules above.
o	CORS – configure CORS in Program.cs to allow communication from your frontend.
4.	Frontend (build from scratch):
•	Build a new frontend application using React or Angular.
•	Implement all required screens.
•	Connect frontend services to backend APIs.
•	Implement form submission and data fetching logic.
•	Implement filtering & sorting for scenarios and entities.
•	Implement a global search feature (search across scenarios and entities).
•	Implement a map view for entities using their coordinates.
•	Implement error handling and loading states.
•	Implement empty states for lists with no data.
•	Implement toast notifications for user feedback (success and error).
•	Ensure validation feedback is displayed to users (both client-side and server-side errors).
5.	Docker:
•	After implementing the core functionality, containerize the solution using Docker:
i.	Create Dockerfiles for Backend and Frontend
ii.	Create a docker-compose.yml.
iii.	Ensure the solution runs with: docker-compose up
iv.	Update the main README with Docker instructions
•	Notes:
i.	Use multi-stage builds for frontend (build + serve)
ii.	Configure CORS in backend to allow frontend origin
iii.	Use environment variables for API base URL in frontend
iv.	Document any required environment variables
6.	Data persistency:
•	Bonus: If you have time, replace in-memory repositories with a database of your choice
7.	Optional bonus:
•	Add tests for validation (backend) or key UI flows (frontend).
•	Implement Update and Delete capabilities end-to-end for Scenarios and Entities.
•	Improve UX polish.

Acceptance criteria:
•	User can create a scenario and see it in the scenario list.
•	User can open scenario details and view entities belonging to that scenario only.
•	User can select TaskForce (Friendly/Enemy) when creating entities, and TaskForce is displayed in entity lists.
•	User can filter and sort entities by Type, TaskForce, and other fields.
•	User can search for scenarios and entities.
•	User can view entities on a map and toggle between table and map views.
•	Toast notifications appear for successful operations and errors.
•	Enhanced loading and empty states are displayed appropriately.
•	Frontend validates all input fields (coordinates, types, required fields).
•	Backend validates all input fields and returns appropriate error responses.
•	Backend returns proper HTTP status codes for all operations.
•	Data persistence works correctly (in-memory or database).
•	Solution runs locally.
•	Solution runs in Docker using docker-compose up with all services properly configured and communicating.
•	Using clean architecture.
•	Code quality and maintainability.
•	Validation and error handling are properly implemented.
•	Reasonable scope management within the timebox.

Please provide:
•	A link to your branch.
•	Short notes (bullet points) describing what you completed and any trade-offs.

Good Luck!
