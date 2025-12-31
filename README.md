## Online Examination System
An Online Examination System built using ASP.NET Core MVC and SQL Server, designed to manage exams, students, instructors, and results in a secure and scalable way.
________________________________________
### Features
#### 1. Student Module
•	Select student profile
•	View enrolled courses
•	Start exams with timer
•	Auto-submit when time ends
•	Prevent cheating (disable refresh / tab switch)
•	View exam results
•	Review exam answers (correct / wrong)
•	See score per question
•	Track progress with charts

#### 2. Admin Module
•	Admin dashboard with statistics
•	Manage students (Create / Update / Delete)
•	Manage instructors
•	Manage courses & tracks
•	Assign courses to students by track
•	Create exams
•	Add questions (MCQ / True-False)
•	Assign exams to courses
•	Control exam duration
•	Pass / Fail grading policy

#### 3. Security & Exam Control
•	Session-based authentication
•	Server-side exam timer
•	Auto-submit on timeout
•	Disable page refresh & back navigation
•	Prevent exam access after submission

#### 4. Database Design
•	SQL Server database
•	Normalized relational schema
•	Stored Procedures for:
o	CRUD operations
o	Exam attempts
o	Student answers
•	Strong FK constraints & composite keys

### Technologies Used
Technology              	Description
ASP.NET Core MVC	        Backend framework
Entity Framework Core   	ORM
SQL Server              	Database
Stored Procedures	       Data access
C#	                      Programming language
Bootstrap 5	             UI styling
JavaScript	Client-side   behavior
________________________________________
## Project Structure
#### ExamOnline.MVC
│
├── Controllers
├── Models
├── DTOs
├── ViewModels
├── Repositories
├── Views
├── wwwroot
   ├── css
   └── js
 
________________________________________ 

### How to Run the Project
1.	Clone the repository
git clone https://github.com/your-username/Online-Examination-System.git
2.	Open the project in Visual Studio
3.	Restore NuGet packages
4.	Update the connection string in appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ITI_Project;Trusted_Connection=True;"
}
5.	Run the project 
________________________________________

### Learning Outcomes
•	MVC Architecture
•	Repository Pattern
•	Stored Procedures with EF Core
•	Session management
•	Real-world exam logic
•	Clean & scalable code structure
________________________________________

### Future Enhancements
•	Authentication with Identity
•	Role-based authorization
•	Question randomization
•	Online proctoring
•	Export results (PDF / Excel)
•	Notifications system
________________________________________

### Author
Abdullah Ali Elkhoyl
•	LinkedIn: https://www.linkedin.com/in/abdullah-ali-elkholy/
________________________________________

### Support
If you like this project, give it a ⭐ on GitHub!

