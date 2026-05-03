# CoreFitness Gym Booking Sysem *

A comprehensive gym management and booking application built with ASP.NET in Visual Studio.

# Features

User Experience

Authentication: Secure Sign-In/Sign-Out with ASP.NET Core Identity.

Authentication : Secure Sign-In/Sign-Out  With Google OAuth.

The "Sign In" button automatically changes to "MyAccount" upon successful login.

The "Get Started" button on the Home page directs users straight to the login/registration flow.

Users can join by clicking "Become A Member" and choose between Standard or Premium .

In "Our Classes" a member can see available sessions and choose alternative for Book or UnBook sessions. 

Gym Store: A dedicated store page to browse products offered by the gym.

FAQ Accordion: An interactive accordion component for reading gym rules and regulations.

# Customer Service page

Include Validation form and send information to Database in dbo.ContactRequestEntity.

Include messege for sent messege "Message was sent!"


# MyAccount & Profile page About Me

About Me: Users can update their personal information and upload a profile picture.

Option to permanently delete an account, which removes all associated data from the database.

Option to Sign Out from About Me page.

Validation: Client-side validation on form, including  About Me pages.

# Admin Portal
A restricted administrative area is available for staff to manage gym operations.

Admin Access: Visible only when logged in with admin credentials.

# Credentials: Admin

Email: g@com

Password: Admin123!

Management: Admin can create , add and delete gym sessions directly from the interface.


# This project utilizes ASP.NET Core MVC to maintain a strict separation of concerns.  

Domain Layer: Core entities like GymSession, User, and Booking.  

Application Layer: Contains communication between the Domain and Infrastructure layers using services and interfaces.

Infrastructure Layer: Data persistence using Entity Framework Core with the Repository Pattern.  

Frontend: A mix of Tailwind CSS for modern utility-first styling and custom Vanilla CSS.

# Testing
The solution features 7  tests to ensure stability:

Unit Tests: Located in UserTests.cs, testing domain logic such as user age and email validation in isolation .

Integration Tests: Using an EF Core InMemory Database to verify real database operations like booking, unbooking, and filtering.  

# Installation & Setup

  Node.js (for Tailwind CSS)

  SQL Server (LocalDB)


Database: Run Update-Database in the Package Manager Console to apply EF Core migrations.  

# Tailwind CSS:

Open Developer PowerShell in the Terminal.

Navigate to the project folder: cd CoreFitness.Web

Start the Tailwind compiler: npm run dev


# Future Improvements 

Due to time constraints, the following features are planned for future releases:

Responsiveness: The UI is currently optimized for desktop, mobile responsiveness is a priority for the next sprint.

Expanded OAuth: Integration of Microsoft, Facebook, and GitHub third-party logins.

Production Deployment: Resolving configuration errors encountered during production environment setup.

Data Isolation: Further separation of booking data into specialized database schemas (.dbo.Bookings) 

Booking Security: Implementing stricter authorization to ensure users can only see and manage their own historical bookings.





